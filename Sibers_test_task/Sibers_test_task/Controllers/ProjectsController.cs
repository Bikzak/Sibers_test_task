using System.Web.Mvc;
using Sibers_test_task.ViewModels;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using System.Linq;
using System.Net;
using DomainModels.Enum;

namespace Sibers_test_task.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private IProjectService projectService = new ProjectService();
        public ActionResult Index()
        {
            var projects = projectService.GetProjects()
                .Select(p => new ProjectViewModel
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName,
                    CreationDate = p.CreationDate,
                    Deadline = p.Deadline,
                    Executor = p.Executor,
                    Customer = p.Customer,
                    Priority = p.Priority
                });
            return View(projects.ToList());
        }
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            if (project.Priority < 0 || project.Priority >= 6)
            {
                ModelState.AddModelError("Priority", "Error, Priority range from 1 to 5");
            }
            if (project.CreationDate >= project.Deadline)
            {
                ModelState.AddModelError("Deadline", "Error in dates,  Check the deadline");
            }

            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            var project_v2 = new ProjectDTO
            {
                ProjectName = project.ProjectName,
                CreationDate = project.CreationDate,
                Deadline = project.Deadline,
                Customer = project.Customer,
                Executor = project.Executor,
                Priority = project.Priority
            };
            projectService.Create(project_v2);
            return RedirectToAction("Index");


        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var res = new ProjectViewModel
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                CreationDate = project.CreationDate,
                Deadline = project.Deadline,
                Executor = project.Executor,
                Customer = project.Customer,
                Priority = project.Priority
            };


            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel project)
        {
            if (project.Priority < 0 || project.Priority >= 6)
            {
                ModelState.AddModelError("Priority", "Error, Priority range from 1 to 5");
            }
            if (project.CreationDate >= project.Deadline)
            {
                ModelState.AddModelError("Deadline", "Error in dates,  Check the deadline");
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", project);
            }
            var project_v2 = new ProjectDTO
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                CreationDate = project.CreationDate,
                Deadline = project.Deadline,
                Customer = project.Customer,
                Executor = project.Executor,
                Priority = project.Priority
            };
            projectService.Edit(project_v2);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            var project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var res = new ProjectViewModel
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                CreationDate = project.CreationDate,
                Deadline = project.Deadline,
                Executor = project.Executor,
                Customer = project.Customer,
                Priority = project.Priority
            };
            return View(res);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            projectService.Delete(project);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var res = new ProjectViewModel
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                CreationDate = project.CreationDate,
                Deadline = project.Deadline,
                Executor = project.Executor,
                Customer = project.Customer,
                Priority = project.Priority
            };
            return View(res);
        }

        [HttpGet]
        public ActionResult AddToProject(int ProjectID)
        {
            // модель которая определена во вью
            var workerView = new WorkerViewModel();

            // создается модель WorkerViewModel из модели workerDTO
            var mapWorker = projectService.GetFreeWorker(ProjectID).AsEnumerable().Select(w => new WorkerViewModel
            {
                WorkerID = w.WorkerID,
                FirstName = w.FirstName,
                LastName = w.LastName,
                PatronymicName = w.PatronymicName,
                Email = w.Email
            });
            workerView.Workers = mapWorker.AsEnumerable().ToList();
            return View(workerView);
        }
        [HttpPost]
        public ActionResult AddToProject(int ProjectID, WorkerViewModel workerViewModel)
        {
            if (workerViewModel.Workers == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var worker in workerViewModel.Workers)
            {
                if (worker.isChecked == true)
                {
                    projectService.AddToProjectBLL(ProjectID, worker.WorkerID);
                }
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult DeleteFromProject(int ProjectID)
        {

            var workerView = new WorkerViewModel();

            // создается модель WorkerViewModel из модели workerDTO
            var mapWorker = projectService.GetWorkersInProject_BLL(ProjectID).AsEnumerable().Select(w => new WorkerViewModel
            {
                WorkerID = w.WorkerID,
                FirstName = w.FirstName,
                LastName = w.LastName,
                PatronymicName = w.PatronymicName,
                Email = w.Email
            });
            workerView.Workers = mapWorker.AsEnumerable().ToList();
            return View(workerView);
        }
        [HttpPost]
        public ActionResult DeleteFromProject(int ProjectID, WorkerViewModel workerViewModel)
        {
            if (workerViewModel.Workers == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var worker in workerViewModel.Workers)
            {

                if (worker.isChecked == true)
                {
                    projectService.DeleteFromProjectBLL(ProjectID, worker.WorkerID);
                }
            }
            return RedirectToAction("Index");
        }
        //Выдает список рабочих в проекте 
        public ActionResult ShowWorkersInProject(int ProjectID)
        {
            // модель которая определена во вью
            var workerView = new WorkerViewModel();

            // создается модель WorkerViewModel из модели workers
            var mapWorker = projectService.GetWorkersInProject_BLL(ProjectID).AsEnumerable().Select(w => new WorkerViewModel
            {
                WorkerID = w.WorkerID,
                FirstName = w.FirstName,
                LastName = w.LastName,
                PatronymicName = w.PatronymicName,
                Email = w.Email
            });
            workerView.Workers = mapWorker.AsEnumerable().ToList();
            return View(workerView);
        }
        //Выдает список задач
        public ActionResult ShowProjectsMissions(int ProjectId)
        {
            var missionView = new TaskViewModel();

            var mapMission = projectService.GetMissionsInProject_BLL(ProjectId).AsEnumerable().Select(m => new TaskViewModel
            {
                TaskID = m.TaskID,
                WorkerID = m.WorkerID,
                ProjectID = m.ProjectID,
                Name = m.Name,
                WorkerName = m.WorkerName,
                TasksStatus = m.TasksStatus,
                Comment = m.Comment,
                Priority = m.Priority
            });
            missionView.Tasks = mapMission.AsEnumerable().ToList();
            return View(missionView);
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(TaskViewModel task)
        {
            if (task.Priority < 0 || task.Priority >= 6)
            {
                ModelState.AddModelError("Priority", "Error, Priority range from 1 to 5");
            }
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            TaskDTO task_v2 = new TaskDTO
            {
                TaskID = task.TaskID,
                WorkerID = task.WorkerID,
                ProjectID = task.ProjectID,
                Name = task.Name,
                WorkerName = task.WorkerName,
                TasksStatus = task.TasksStatus,
                Comment = task.Comment,
                Priority = task.Priority
            };
            projectService.CreateTask(task_v2);
            return RedirectToAction("Index");
            
        }

    }
}
