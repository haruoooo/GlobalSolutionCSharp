using Microsoft.EntityFrameworkCore;
using FutureWork.Domain.Entities;

namespace FutureWork.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Professional> Professionals => Set<Professional>();
        public DbSet<LearningPath> LearningPaths => Set<LearningPath>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<Progress> Progresses => Set<Progress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professional>(e =>
            {
                e.ToTable("PROFESSIONAL");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.Email).IsRequired().HasMaxLength(120);
            });

            modelBuilder.Entity<LearningPath>(e =>
            {
                e.ToTable("LEARNING_PATH");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Title).IsRequired().HasMaxLength(150);
                e.Property(x => x.Description).HasMaxLength(400);
                e.Property(x => x.Area).HasMaxLength(100);
            });

            modelBuilder.Entity<Module>(e =>
            {
                e.ToTable("MODULE");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();

                e.HasOne(x => x.LearningPath)
                 .WithMany(p => p.Modules)
                 .HasForeignKey(x => x.LearningPathId)
                 .HasConstraintName("FK_MODULE_LEARNINGPATH");

                e.Property(x => x.Title).IsRequired().HasMaxLength(150);
                e.Property(x => x.WorkloadHours).IsRequired();
            });

            modelBuilder.Entity<Progress>(e =>
            {
                e.ToTable("PROGRESS");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();

                e.HasOne(x => x.Professional)
                 .WithMany(p => p.Progresses)
                 .HasForeignKey(x => x.ProfessionalId)
                 .HasConstraintName("FK_PROGRESS_PROFESSIONAL");

                e.HasOne(x => x.Module)
                 .WithMany(m => m.Progresses)
                 .HasForeignKey(x => x.ModuleId)
                 .HasConstraintName("FK_PROGRESS_MODULE");

                e.Property(x => x.Status).IsRequired();
                e.Property(x => x.Percentage).HasDefaultValue(0);
                e.Property(x => x.UpdatedAt).HasColumnType("TIMESTAMP");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
