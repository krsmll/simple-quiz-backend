using System;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Option> Options { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<SelectedOption> SelectedOptions { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}