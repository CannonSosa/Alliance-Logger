using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Logging.API
{
    public partial class RemoteLoggerContext : DbContext
    {
        public RemoteLoggerContext()
        {
        }

        public RemoteLoggerContext(DbContextOptions<RemoteLoggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DatabaseLogs> DatabaseLogs { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
#if DEBUG
                optionsBuilder.UseSqlServer(@"server=DESKTOP-L7N9NMJ\SQLEXPRESS;database=RemoteLogger;user id=devlogin;pwd=Benton$42025;");
#else
                optionsBuilder.UseSqlServer("server=USSDEV220;database=RemoteLogger;user id=sa;pwd=Alliance&*****;");
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey("CustomerID");

                entity.Property(e => e.CustomerID)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DatabaseLogs>(entity =>
            {
                entity.HasKey("LogID");

                entity.Property(e => e.Application)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LogID)
                    .HasColumnName("LogID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LogContent).HasMaxLength(100);

                entity.Property(e => e.LogDateTime).HasColumnType("datetime");

                entity.Property(e => e.NoteContent).HasMaxLength(100);

    });

            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.HasKey("BookmarkID");

                entity.Property(e => e.CustomerID).HasColumnName("CustomerID");

                entity.Property(e => e.LogID).HasColumnName("LogID");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
