using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;
using Planner.Data.Entities;
using Task = Planner.Data.Entities.Task;
using System.Security.Claims;

namespace Planner.Controllers
{
    public class BoardController : Controller
    {
        private readonly PlannerDbContext _data;
        public BoardController(PlannerDbContext context)
        {
            _data = context;
        }
        public async Task<IActionResult> All()
        {
            var boards = await _data
                .Boards
                .Select(b => new BoardViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b
                    .Tasks
                    .Select(t=> new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.User.UserName
                    })
                })
                .ToListAsync();

            return View(boards);

        }

        public async Task<IActionResult> Create()
        {
            TaskFormModel taskModel = new TaskFormModel() { Boards = GetBoards() };
            return View(taskModel);


        }

        private IEnumerable<TaskBoardModel> GetBoards() =>
            _data
            .Boards
            .Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name
            });

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            if (!GetBoards().Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist");
            }
            string currentUserId = GetUserId();

            if (!ModelState.IsValid)
            {
                taskModel.Boards = GetBoards();
                return View(taskModel);
            }

            Task task = new Task() {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId,

            };

            await _data.Tasks.AddAsync(task);
            await _data.SaveChangesAsync();

            var boards = _data.Boards;
            return RedirectToAction("All", "Boards");

        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
