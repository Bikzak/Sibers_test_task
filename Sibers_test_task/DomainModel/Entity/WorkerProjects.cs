using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Entity
{
    public class WorkerProjects
    {
        [Key, Column(Order = 0)]
        public int WorkerID { get; set; }
        [Key, Column(Order = 1)]
        public int ProjectID { get; set; }
    }
}
