using Microsoft.EntityFrameworkCore;
using InterviewPrep.Domain.Entities;

namespace InterviewPrep.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Question> Questions => Set<Question>();

    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();

    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();

    public DbSet<UserSkill> UserSkills => Set<UserSkill>();

    public DbSet<UserProgress> UserProgresses => Set<UserProgress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");

        modelBuilder.Entity<Category>().ToTable("categories");

        modelBuilder.Entity<Question>().ToTable("questions");

        modelBuilder.Entity<UserAnswer>().ToTable("user_answers");

        modelBuilder.Entity<Bookmark>().ToTable("bookmarks");

        modelBuilder.Entity<UserSkill>().ToTable("user_skills");

        modelBuilder.Entity<UserProgress>().ToTable("user_progress");
    }
}