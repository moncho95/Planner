using System.ComponentModel.DataAnnotations;

namespace Planner.Data.Entities
{
    using static Planner.Data.DataConstants.Board;
    public class Board
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(BoardMaxName)]
        public string Name { get; init; } = null!;

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
