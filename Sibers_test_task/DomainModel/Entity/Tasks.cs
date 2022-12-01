using DomainModels.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Entity
{
    public class Tasks
    {
        /// <summary>
        /// Идентификатор Задач
        /// </summary>
        [Key, Column(Order = 1)]
        public int TaskID { get; set; }
        /// <summary>
        /// Идентификатор Работника
        /// </summary>
        public int WorkerID { get; set; }
        [ForeignKey("WorkerID")]
        public Workers Worker { get; set; }
        /// <summary>
        /// Идентификатор Проекта
        /// </summary>
        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Projects Project { get; set; }
        [Display(Name = "Название задачи")]
        public string Name { get; set; }
        [Display(Name = "Исполнитель")]
        public string WorkerName { get; set; }
        [Display(Name = "Статус задачи")]
        public TasksStatus TasksStatus { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        [Display(Name = "Приоритет")]
        public int Priority { get; set; }
    }
}
