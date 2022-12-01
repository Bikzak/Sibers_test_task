using BusinessLogicLayer.DTO;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWorkerService
    {
        void Create(WorkerDTO workerDTO);
        IEnumerable<WorkerDTO> GetWorkers();
        WorkerDTO GetWorker(int id);
        void Edit(WorkerDTO workerDTO);
        void Delete(WorkerDTO workerDTO);
        List<ProjectDTO> GetWorkersProjects(int id);
    }
}