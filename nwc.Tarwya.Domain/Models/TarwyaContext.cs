using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class TarwyaContext : DbContext
    {
        public TarwyaContext()
        {
        }

        public TarwyaContext(DbContextOptions<TarwyaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryItem> CategoryItems { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<ComplaintImage> ComplaintImages { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public virtual DbSet<FeedbackQuestionAnswer> FeedbackQuestionAnswers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<Toilet> Toilets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<ZamZamLocation> ZamZamLocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("Name_En");

                entity.Property(e => e.NameFa).HasColumnName("Name_Fa");

                entity.Property(e => e.NameFr).HasColumnName("Name_Fr");

                entity.Property(e => e.NameId).HasColumnName("Name_id");

                entity.Property(e => e.NameTr).HasColumnName("Name_Tr");

                entity.Property(e => e.NameUr).HasColumnName("Name_Ur");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("Name_En");

                entity.Property(e => e.NameFa).HasColumnName("Name_Fa");

                entity.Property(e => e.NameFr).HasColumnName("Name_Fr");

                entity.Property(e => e.NameId).HasColumnName("Name_id");

                entity.Property(e => e.NameTr).HasColumnName("Name_Tr");

                entity.Property(e => e.NameUr).HasColumnName("Name_Ur");
            });

            modelBuilder.Entity<CategoryItem>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("IX_SubCategories")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("Name_En");

                entity.Property(e => e.NameFa).HasColumnName("Name_Fa");

                entity.Property(e => e.NameFr).HasColumnName("Name_Fr");

                entity.Property(e => e.NameId).HasColumnName("Name_id");

                entity.Property(e => e.NameTr).HasColumnName("Name_Tr");

                entity.Property(e => e.NameUr).HasColumnName("Name_Ur");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryItems)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategories_Categories");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.Property(e => e.AgentLocation).IsRequired();

                entity.Property(e => e.AgetLanguage).IsRequired();

                entity.Property(e => e.AgetOs)
                    .IsRequired()
                    .HasColumnName("AgetOS");

                entity.Property(e => e.AssetId).IsRequired();

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.IsSyncedToCcb).HasColumnName("IsSyncedToCCB");

                entity.Property(e => e.IssuerMobileNumber)
                    .IsRequired()
                    .HasColumnName("Issuer_MobileNumber");

                entity.Property(e => e.IssuerName)
                    .IsRequired()
                    .HasColumnName("Issuer_Name");

                entity.Property(e => e.MantinanceArea).IsRequired();

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaints_SubCategories");
            });

            modelBuilder.Entity<ComplaintImage>(entity =>
            {
                entity.Property(e => e.EamPath).HasColumnName("EAM_Path");

                entity.Property(e => e.LocalName).IsRequired();

                entity.HasOne(d => d.Complaint)
                    .WithMany(p => p.ComplaintImages)
                    .HasForeignKey(d => d.ComplaintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplaintImages_Complaints");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<FeedbackQuestion>(entity =>
            {
                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("Name_En");

                entity.Property(e => e.NameFa).HasColumnName("Name_Fa");

                entity.Property(e => e.NameFr).HasColumnName("Name_Fr");

                entity.Property(e => e.NameId).HasColumnName("Name_id");

                entity.Property(e => e.NameTr).HasColumnName("Name_Tr");

                entity.Property(e => e.NameUr).HasColumnName("Name_Ur");
            });

            modelBuilder.Entity<FeedbackQuestionAnswer>(entity =>
            {
                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackQuestionAnswers)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedbackQuestionAnswers_Feedbacks");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.FeedbackQuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedbackQuestionAnswers_FeedbackQuestions");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "Security");

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims", "Security");

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Toilet>(entity =>
            {
                entity.Property(e => e.Code).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Security");

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaims", "Security");

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.ProviderKey, e.LoginProvider });

                entity.ToTable("UserLogins", "Security");

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("AK_UserLogins_LoginProvider_ProviderKey")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("UserRoles", "Security");

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("UserTokens", "Security");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<ZamZamLocation>(entity =>
            {
                entity.Property(e => e.NameAr)
                    .IsRequired()
                    .HasColumnName("Name_Ar");

                entity.Property(e => e.NameEn)
                    .IsRequired()
                    .HasColumnName("Name_En");

                entity.Property(e => e.NameFa)
                    .IsRequired()
                    .HasColumnName("Name_Fa");

                entity.Property(e => e.NameFr)
                    .IsRequired()
                    .HasColumnName("Name_Fr");

                entity.Property(e => e.NameId)
                    .IsRequired()
                    .HasColumnName("Name_id");

                entity.Property(e => e.NameTr)
                    .IsRequired()
                    .HasColumnName("Name_Tr");

                entity.Property(e => e.NameUr)
                    .IsRequired()
                    .HasColumnName("Name_Ur");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}