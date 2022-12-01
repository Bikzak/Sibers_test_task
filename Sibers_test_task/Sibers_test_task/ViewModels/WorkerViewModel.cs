using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sibers_test_task.ViewModels
{
    public class WorkerViewModel
    {
        public int WorkerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public string Email { get; set; }
        public List<WorkerViewModel> Workers { get; set; }
        [Display(Name = "Please tick ")]
        public bool isChecked { get; set; }
    }
}