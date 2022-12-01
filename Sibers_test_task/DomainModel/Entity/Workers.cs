using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Entity
{
    [Table("Workers")]
    public class Workers
    {
        /// <summary>
        /// Идентификатор работника
        /// </summary>
        [Key]
        public int WorkerID { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилие")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [Display(Name = "E-mail работника")]
        public string Email { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
