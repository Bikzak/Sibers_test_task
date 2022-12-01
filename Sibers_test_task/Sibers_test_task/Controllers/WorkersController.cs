using System.Web.Mvc;
using Sibers_test_task.ViewModels;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using System.Linq;
using System.Net;

namespace Sibers_test_task.Controllers
{
    [Authorize]
    public class WorkersController : Controller
    {
        private IWorkerService workerService = new WorkersService();
        public ActionResult Index()
        {
            var workers = workerService.GetWorkers()
                .Select(p => new WorkerViewModel
                {
                    WorkerID = p.WorkerID,
                    FirstName = p.FirstName,
                    PatronymicName = p.PatronymicName,
                    LastName = p.LastName,
                    Email = p.Email
                });
            return View(workers.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkerViewModel worker)
        {
            var worker_v2 = new WorkerDTO
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                PatronymicName = worker.PatronymicName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            workerService.Create(worker_v2);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var worker = workerService.GetWorker(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            var res = new WorkerViewModel
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                PatronymicName = worker.PatronymicName,
                LastName = worker.LastName,
                Email = worker.Email
            };


            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkerViewModel worker)
        {
            var worker_v2 = new WorkerDTO
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                PatronymicName = worker.PatronymicName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            workerService.Edit(worker_v2);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            var worker = workerService.GetWorker(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            var res = new WorkerViewModel
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                PatronymicName = worker.PatronymicName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            return View(res);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var worker = workerService.GetWorker(id);
            if (worker == null)
            {
                return HttpNotFound();
            }

            workerService.Delete(worker);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {

            var worker = workerService.GetWorker(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            var res = new WorkerViewModel
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                PatronymicName = worker.PatronymicName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            return View(res);
        }
        [HttpGet]
        public ActionResult ShowProjects(int WorkerID)
        {
            var projectView = new ProjectViewModel();
            var mapProjects = workerService.GetWorkersProjects(WorkerID).AsEnumerable().Select(p => new ProjectViewModel
            {
                ProjectID = p.ProjectID,
                ProjectName = p.ProjectName,
                Executor = p.Executor,
                Customer = p.Customer,
                Deadline = p.Deadline,
                CreationDate = p.CreationDate,
                Priority = p.Priority
            });
            projectView.Projects = mapProjects.ToList();
            return View(projectView);
        }

    }
}