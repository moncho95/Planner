using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Planner.Data.Entities;
using Task = Planner.Data.Entities.Task;

namespace Planner.Data
{
    public class PlannerDbContext : IdentityDbContext<IdentityUser>
    {
        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Task> Tasks { get; set; }

        private IdentityUser TestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgress { get; set; }
        private Board DoneBoard { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

                

            SeedUsers();
            modelBuilder
                .Entity<IdentityUser>()
                .HasData(TestUser);
            SeedBoards();
            modelBuilder
                .Entity<Board>()
                .HasData(OpenBoard, InProgress, DoneBoard);
            
            modelBuilder
                .Entity<Task>()
                .HasData(new Task()
                {
                    Id = 1,
                    Title = "Prepare for the technical interview",
                    Description = "Solve problems, go over past solutions, see most popular interview questions.",
                    CreatedOn = DateTime.Now,
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id

                },
                new Task()
                {
                    Id = 2,
                    Title = "Draw a painting for uncle's birthday",
                    Description = "My dad wants a painting A3 format of their dog or something else.",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id

                },
                new Task()
                {
                    Id = 3,
                    Title = "Begin reading new book",
                    Description = "The last book I bought sits on my sheelf for too long.",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = InProgress.Id

                }, new Task()
                {
                    Id = 4,
                    Title = "Buy I new notebook for programming",
                    Description = "I filled all of my notebooks and need to buy a new one.",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = DoneBoard.Id

                });
            base.OnModelCreating(modelBuilder);
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            TestUser = new IdentityUser()
            {
                UserName = "test@softuni.bg",
                NormalizedUserName = "TEST@SOFTUNI.BG"
            };

            TestUser.PasswordHash = hasher.HashPassword(TestUser, "softuni");
        }
        private void SeedBoards()
        {
            OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };

            InProgress = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };

            DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };

        }

    }
}