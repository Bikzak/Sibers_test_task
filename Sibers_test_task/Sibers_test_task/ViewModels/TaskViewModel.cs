using DomainModels.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sibers_test_task.ViewModels
{
    public class TaskViewModel
    {
        public int TaskID { get; set; }
        public int WorkerID { get; set; }
        public int ProjectID { get; set; }
        [Display(Name = "Name of Task")]
        public string Name { get; set; }
        [Display(Name = "Executer")]
        public string WorkerName { get; set; }
        [Display(Name = "Task Status")]
        
        public TasksStatus TasksStatus { get; set; }
        [Display(Name = "Comment")]
        public string Comment { get; set; }
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
        [Display(Name = "Please tick")]
        public bool isChecked { get; set; }
    }
}