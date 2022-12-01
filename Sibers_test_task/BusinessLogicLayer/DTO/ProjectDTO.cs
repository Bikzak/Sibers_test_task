using System;

namespace BusinessLogicLayer.DTO
{
    public class ProjectDTO
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Executor { get; set; }
        public string Customer { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreationDate { get; set; }
        public int Priority { get; set; }
    }
}
