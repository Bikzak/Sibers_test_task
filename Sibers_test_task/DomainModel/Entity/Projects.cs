using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Entity
{
    [Table("Projects")]
    public class Projects
    {
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        [Key]
        public int ProjectID { get; set; }
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        [Display(Name = "Название проекта")]
        public string ProjectName { get; set; }
        [Display(Name = "Компания-Исполнитель")]
        public string Executor { get; set; }
        [Display(Name = "Компания-Заказчик")]
        public string Customer { get; set; }
        [Display(Name = "Дата окончания проекта")]
        public DateTime Deadline { get; set; }
        [Display(Name = "Дата начала проекта")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Приоритет")]
        [Required(ErrorMessage = "Только от 1 до 5")]
        public int Priority { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
