using System.ComponentModel.DataAnnotations;

namespace DomainModels.Enum
{
    public enum TasksStatus
    {
        [Display(Name = "Создан")]
        ToDo = 1,
        [Display(Name = "В процессе")]
        InProgress = 2,
        [Display(Name = "Завершен")]
        Done = 3
    }
}
