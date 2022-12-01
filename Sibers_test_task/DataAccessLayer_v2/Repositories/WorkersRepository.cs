using DataAccessLayer.EF;
using DomainModels.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class WorkersRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Create(Workers worker)
        {
            db.Worker.Add(worker);
            db.SaveChanges();
        }
        public IEnumerable<Workers> GetAll()
        {
            return db.Worker;
        }
        public Workers GetWorker(int id)
        {
            return db.Worker.Find(id);
        }
        public void EditWorker(Workers worker)
        {
            db.Entry(worker).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteWorker(Workers worker)
        {
            db.Entry(worker).State = EntityState.Deleted;
            db.SaveChanges();
        }
        public IQueryable<int> GetWorkerProjects(int id)
        {
            var workersInProject = from wp in db.WorkerProjects
                                   where wp.WorkerID == id
                                   select wp.ProjectID;
            return workersInProject;
        }
        public List<Projects> GetAllProject(List<int> list)
        {
            var worker = from p in db.Project.AsEnumerable()
                         where (list.Contains(p.ProjectID))
                         select new Projects()
                         {
                             ProjectID = p.ProjectID,
                             ProjectName = p.ProjectName,
                             Executor = p.Executor,
                             Customer = p.Customer,
                             Deadline = p.Deadline,
                             CreationDate = p.CreationDate,
                             Priority = p.Priority
                         };
            return worker.ToList();
        }
    }
}
