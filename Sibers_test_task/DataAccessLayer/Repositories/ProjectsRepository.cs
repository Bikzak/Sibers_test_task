using DataAccessLayer.EF;
using DomainModels.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class ProjectsRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Create(Projects project)
        {
            db.Project.Add(project);
            db.SaveChanges();
        }
        public IEnumerable<Projects> GetAll()
        {
            return db.Project;
        }
        public Projects GetProject(int id)
        {
            return db.Project.Find(id);
        }
        public void EditProject(Projects project)
        {
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteProject(Projects project)
        {
            var workerProject = db.WorkerProjects.Where(x => x.ProjectID == project.ProjectID);
            db.WorkerProjects.RemoveRange(workerProject);
            var tasks = db.Task.Where(x => x.ProjectID == project.ProjectID);
            db.Task.RemoveRange(tasks);
            Projects res = db.Project.FirstOrDefault(p => p.ProjectID == project.ProjectID);
                db.Entry(res).State = EntityState.Deleted;
                db.SaveChanges();
            
        }
        public IQueryable<int> GetWorkerProjects(int id)
        {
            var workersInProject = from wp in db.WorkerProjects
                                   where wp.ProjectID == id
                                   select wp.WorkerID;
            return workersInProject;
        }
        public List<Workers> GetAllFreeWorkers(List<int> list)
        {
            var worker = from w in db.Worker.AsEnumerable()
                         where !(list.Contains(w.WorkerID))
                         select new Workers()
                         {
                             WorkerID = w.WorkerID,
                             FirstName = w.FirstName,
                             LastName = w.LastName,
                             PatronymicName = w.PatronymicName,
                             Email = w.Email
                         };
            return worker.ToList();
        }
        public void AddToProjectDAL(WorkerProjects workerProject)
        {
            db.WorkerProjects.Add(workerProject);
            db.SaveChanges();
        }
        public List<Workers> GetWorkersInProject_DAL(List<int> list)
        {
            var worker = from w in db.Worker.AsEnumerable()
                         where (list.Contains(w.WorkerID))
                         select new Workers()
                         {
                             WorkerID = w.WorkerID,
                             FirstName = w.FirstName,
                             LastName = w.LastName,
                             PatronymicName = w.PatronymicName,
                             Email = w.Email
                         };
            return worker.ToList();
        }
        public void DeleteFromProjectDAL(WorkerProjects workerProject)
        {
            WorkerProjects res = db.WorkerProjects.FirstOrDefault(uc =>
               uc.WorkerID == workerProject.WorkerID && uc.ProjectID == workerProject.ProjectID);
            db.WorkerProjects.Remove(res);
            db.SaveChanges();
        }

        public List<Tasks> GetMissions_DAL(int projectId)
        {
            var missions = from m in db.Task.AsEnumerable()
                           where m.ProjectID == projectId
                           select new Tasks()
                           {
                               TaskID = m.TaskID,
                               WorkerID = m.WorkerID,
                               ProjectID = m.ProjectID,
                               Name = m.Name,
                               WorkerName = m.WorkerName,
                               TasksStatus = m.TasksStatus,
                               Comment = m.Comment,
                               Priority = m.Priority
                           };
            return missions.ToList();
        }

        public void CreateTask(Tasks task)
        {
            db.Task.Add(task);
            db.SaveChanges();
        }
    }
}
