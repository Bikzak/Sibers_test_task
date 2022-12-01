using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Repositories;
using DomainModels.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class ProjectService : IProjectService
    {
        private ProjectsRepository service = new ProjectsRepository();

        public void Create(ProjectDTO projectDTO)
        {
            var project = new Projects
            {
                ProjectName = projectDTO.ProjectName,
                Executor = projectDTO.Executor,
                CreationDate = projectDTO.CreationDate,
                Deadline = projectDTO.Deadline,
                Customer = projectDTO.Customer,
                Priority = projectDTO.Priority
            };
            service.Create(project);
        }

        public IEnumerable<ProjectDTO> GetProjects()
        {
            var projects = service.GetAll().ToList()
                .Select(p => new ProjectDTO
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName,
                    CreationDate = p.CreationDate,
                    Deadline = p.Deadline,
                    Executor = p.Executor,
                    Customer = p.Customer,
                    Priority = p.Priority
                });
            return projects;
        }
        public ProjectDTO GetProject(int id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id проекта", "");
            var project = service.GetProject(id);
            if (project == null)
                throw new ValidationException("Проект не найден", "");
            var result = new ProjectDTO
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                CreationDate = project.CreationDate,
                Deadline = project.Deadline,
                Executor = project.Executor,
                Customer = project.Customer,
                Priority = project.Priority
            };
            return result;

        }
        public void Edit(ProjectDTO projectDTO)
        {
            var project = new Projects
            {
                ProjectID = projectDTO.ProjectID,
                ProjectName = projectDTO.ProjectName,
                CreationDate = projectDTO.CreationDate,
                Deadline = projectDTO.Deadline,
                Executor = projectDTO.Executor,
                Customer = projectDTO.Customer,
                Priority = projectDTO.Priority
            };
            service.EditProject(project);
        }
        
        public void Delete(ProjectDTO projectDTO)
        {
            var project = new Projects
            {
                ProjectID = projectDTO.ProjectID,
                ProjectName = projectDTO.ProjectName,
                CreationDate = projectDTO.CreationDate,
                Deadline = projectDTO.Deadline,
                Executor = projectDTO.Executor,
                Customer = projectDTO.Customer,
                Priority = projectDTO.Priority
            };
            service.DeleteProject(project);
        }

        //Выдает список работников, вне этого проекта (свободных)
        public List<WorkerDTO> GetFreeWorker(int id)
        {
            var workerInProject = service.GetWorkerProjects(id).ToList();
            var worker = service.GetAllFreeWorkers(workerInProject).Select(w => new WorkerDTO
            {
                WorkerID = w.WorkerID,
                FirstName = w.FirstName,
                LastName = w.LastName,
                PatronymicName = w.PatronymicName,
                Email = w.Email
            });
            return worker.ToList();
        }

        public void AddToProjectBLL(int ProjectID, int WorkerID)
        {
            var workerProject = new WorkerProjects
            {
                ProjectID = ProjectID,
                WorkerID = WorkerID
            };
            service.AddToProjectDAL(workerProject);
        }

        public List<WorkerDTO> GetWorkersInProject_BLL(int id)
        {
            var workerInProject = service.GetWorkerProjects(id).ToList();
            var worker = service.GetWorkersInProject_DAL(workerInProject).Select(w => new WorkerDTO
            {
                WorkerID = w.WorkerID,
                FirstName = w.FirstName,
                LastName = w.LastName,
                PatronymicName = w.PatronymicName,
                Email = w.Email
            });
            return worker.ToList();
        }

        public void DeleteFromProjectBLL(int ProjectID, int WorkerID)
        {
            var workerProject = new WorkerProjects
            {
                ProjectID = ProjectID,
                WorkerID = WorkerID
            };
            service.DeleteFromProjectDAL(workerProject);
        }

        public List<TaskDTO> GetMissionsInProject_BLL(int id)
        {
            var missionsInProject = service.GetMissions_DAL(id).ToList();
            var mapMissions = missionsInProject.Select(m => new TaskDTO
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
            return mapMissions.ToList();
        }

        public void CreateTask(TaskDTO taskDTO)
        {
            var task = new Tasks
            {
                TaskID = taskDTO.TaskID,
                WorkerID = taskDTO.WorkerID,
                ProjectID = taskDTO.ProjectID,
                Name = taskDTO.Name,
                WorkerName = taskDTO.WorkerName,
                TasksStatus = taskDTO.TasksStatus,
                Comment = taskDTO.Comment,
                Priority = taskDTO.Priority
            };
            service.CreateTask(task);
        }
    }
}
