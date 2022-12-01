using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sibers_test_task.ViewModels
{
    public class ProjectViewModel
    {

        public int ProjectID { get; set; }
        [Display(Name = "Project name")]
        public string ProjectName { get; set; }
        [Display(Name = "Executer company")]
        public string Executor { get; set; }
        [Display(Name = "Сlient company")]
        public string Customer { get; set; }
        [Display(Name = "Deadline of project")]
        public DateTime Deadline { get; set; }
        [Display(Name = "Creation Date of project")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Priority")]
        [Required(ErrorMessage = "Error! Range only from 1 to 5")]
        public int Priority { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
    }
}