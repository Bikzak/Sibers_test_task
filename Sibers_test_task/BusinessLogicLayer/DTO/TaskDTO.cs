using DomainModels.Enum;

namespace BusinessLogicLayer.DTO
{
    public class TaskDTO
    {
        public int TaskID { get; set; }
        public int WorkerID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string WorkerName { get; set; }
        public TasksStatus TasksStatus { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
    }
}
