using BusinessLogicLayer.DTO;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectService
    {
        void Create(ProjectDTO projectDTO);
        IEnumerable<ProjectDTO> GetProjects();
        ProjectDTO GetProject(int id);
        void Edit(ProjectDTO projectDTO);
        void Delete(ProjectDTO projectDTO);
        List<WorkerDTO> GetFreeWorker(int id);
        void AddToProjectBLL(int ProjectID, int WorkerID);
        List<WorkerDTO> GetWorkersInProject_BLL(int id);
        void DeleteFromProjectBLL(int ProjectID, int WorkerID);
        List<TaskDTO> GetMissionsInProject_BLL(int id);
        void CreateTask(TaskDTO taskDTO);
    }
}
