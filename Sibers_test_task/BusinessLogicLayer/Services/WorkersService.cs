using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Repositories;
using DomainModels.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class WorkersService : IWorkerService
    {
        private WorkersRepository service = new WorkersRepository();

        public void Create(WorkerDTO workerDTO)
        {
            var worker = new Workers
            {
                FirstName = workerDTO.FirstName,
                PatronymicName = workerDTO.PatronymicName,
                LastName = workerDTO.LastName,
                Email = workerDTO.Email
            };
            service.Create(worker);
        }

        public void Delete(WorkerDTO workerDTO)
        {
            var worker = new Workers
            {
                WorkerID = workerDTO.WorkerID,
                FirstName = workerDTO.FirstName,
                PatronymicName = workerDTO.PatronymicName,
                LastName = workerDTO.LastName,
                Email = workerDTO.Email
            };
            service.DeleteWorker(worker);
        }

        public void Edit(WorkerDTO workerDTO)
        {
            var worker = new Workers
            {
                WorkerID = workerDTO.WorkerID,
                FirstName = workerDTO.FirstName,
                PatronymicName = workerDTO.PatronymicName,
                LastName = workerDTO.LastName,
                Email = workerDTO.Email
            };
            service.EditWorker(worker);
        }

        public WorkerDTO GetWorker(int id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id проекта", "");
            var worker = service.GetWorker(id);
            if (worker == null)
                throw new ValidationException("Проект не найден", "");
            var result = new WorkerDTO
            {
                WorkerID = worker.WorkerID,
                FirstName = worker.FirstName,
                PatronymicName = worker.PatronymicName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            return result;
        }

        public IEnumerable<WorkerDTO> GetWorkers()
        {
            var workers = service.GetAll().ToList()
                .Select(p => new WorkerDTO
                {
                    WorkerID = p.WorkerID,
                    FirstName = p.FirstName,
                    PatronymicName = p.PatronymicName,
                    LastName = p.LastName,
                    Email = p.Email
                });
            return workers;
        }

        public List<ProjectDTO> GetWorkersProjects(int id)
        {
            var workerProject = service.GetWorkerProjects(id).ToList();
            var projects = service.GetAllProject(workerProject).Select(p => new ProjectDTO
            {
                ProjectID = p.ProjectID,
                ProjectName = p.ProjectName,
                Executor = p.Executor,
                Customer = p.Customer,
                Deadline = p.Deadline,
                CreationDate = p.CreationDate,
                Priority = p.Priority
            });
            return projects.ToList();
        }
    }
}
