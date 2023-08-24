using System.ComponentModel.DataAnnotations;

namespace Planner.Models
{
    using static Planner.Data.DataConstants.Task;
    public class TaskFormModel
    {
        [Required]
        [StringLength(TaskMaxTitle, MinimumLength = TaskMinTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(TaskMaxDescription, MinimumLength = TaskMinDescription, ErrorMessage =
            "Description should be at least {2} characters long.")]
        public string Description { get; set; } = null!;
        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<TaskBoardModel> Boards { get; set; } = null!;
    }
}
