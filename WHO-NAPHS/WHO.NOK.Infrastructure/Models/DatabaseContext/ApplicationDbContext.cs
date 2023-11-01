// <copyright file="ApplicationDbContext.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress constructor error.
#pragma warning disable CS8618

namespace WHO.NOK.Infrastructure.Models.DatabaseContext
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using WHO.NOK.Infrastructure.Models.Assessments;
    using WHO.NOK.Infrastructure.Models.Configurations;
    using WHO.NOK.Infrastructure.Models.Countries;
    using WHO.NOK.Infrastructure.Models.ErrorLog;
    using WHO.NOK.Infrastructure.Models.Excel;
    using WHO.NOK.Infrastructure.Models.Languages;
    using WHO.NOK.Infrastructure.Models.Permissions;
    using WHO.NOK.Infrastructure.Models.Plans;
    using WHO.NOK.Infrastructure.Models.Users;
    using Common = WHO.NOK.Core.Common;

    /// <summary>
    /// Application DB Context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options"> Database option.</param>
        /// <param name="httpContextAccessor"> Http accessor.</param>
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets Roles.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets mapping table of user and roles.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets Countries.
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Gets or sets Currencies.
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets Languages.
        /// </summary>
        public DbSet<Language> Languages { get; set; }

        /// <summary>
        /// Gets or sets country plan.
        /// </summary>
        public DbSet<CountryPlan> CountryPlans { get; set; }

        /// <summary>
        /// Gets or sets sources.
        /// </summary>
        public DbSet<Source> Sources { get; set; }

        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public DbSet<TechnicalArea> TechnicalAreas { get; set; }

        /// <summary>
        /// Gets or sets technical area indicators.
        /// </summary>
        public DbSet<TechnicalAreaIndicator> TechnicalAreaIndicators { get; set; }

        /// <summary>
        /// Gets or sets plan indicators.
        /// </summary>
        public DbSet<CountryPlanIndicator> CountryPlanIndicators { get; set; }

        /// <summary>
        /// Gets or sets excel mapping.
        /// </summary>
        public DbSet<ExcelMapping> ExcelMappings { get; set; }

        /// <summary>
        /// Gets or sets common technical areas.
        /// </summary>
        public DbSet<CommonTechnicalArea> CommonTechnicalAreas { get; set; }

        /// <summary>
        /// Gets or sets SQL Error logs.
        /// </summary>
        public DbSet<SQLErrorLog> SQLErrorLogs { get; set; }

        /// <summary>
        /// Gets or sets plan types.
        /// </summary>
        public DbSet<PlanType> PlanTypes { get; set; }

        /// <summary>
        /// Gets or sets Plan status.
        /// </summary>
        public DbSet<PlanStatus> PlanStatuses { get; set; }

        /// <summary>
        /// Gets or sets Plan stage.
        /// </summary>
        public DbSet<PlanStage> PlanStages { get; set; }

        /// <summary>
        /// Gets or sets assessment type.
        /// </summary>
        public DbSet<AssessmentType> AssessmentTypes { get; set; }

        /// <summary>
        /// Gets or sets planning tools.
        /// </summary>
        public DbSet<PlanningTools> PlanningTools { get; set; }

        /// <summary>
        /// Gets or sets IHR recommendations.
        /// </summary>
        public DbSet<IHRRecommendations> IHRRecommendations { get; set; }

        /// <summary>
        /// Gets or sets NBW recommendations.
        /// </summary>
        public DbSet<NBWRecommendations> NBWRecommendations { get; set; }

        /// <summary>
        /// Gets or sets strategic action priorities.
        /// </summary>
        public DbSet<StrategicActionPriority> StrategicActionPriorities { get; set; }

        /// <summary>
        /// Gets or sets strategic action impact.
        /// </summary>
        public DbSet<StrategicActionImpact> StrategicActionImpacts { get; set; }

        /// <summary>
        /// Gets or sets strategic action Feasibility.
        /// </summary>
        public DbSet<StrategicActionFeasibility> StrategicActionFeasibility { get; set; }

        /// <summary>
        /// Gets or sets strategic actions for strategic plan.
        /// </summary>
        public DbSet<StrategicAction> StrategicActions { get; set; }

        /// <summary>
        /// Gets or sets collaborating institutions.
        /// </summary>
        public DbSet<CollaboratingInstitution> CollaboratingInstitutions { get; set; }

        /// <summary>
        /// Gets or sets detailed activity type.
        /// </summary>
        public DbSet<DetailedActivityType> DetailedActivityTypes { get; set; }

        /// <summary>
        /// Gets or sets detailed activities.
        /// </summary>
        public DbSet<DetailedActivity> DetailedActivities { get; set; }

        /// <summary>
        /// Gets or sets application settings configurations.
        /// </summary>
        public DbSet<Configuration> Configurations { get; set; }

        /// <summary>
        /// Gets or sets Common Indicator mappings.
        /// </summary>
        public DbSet<CommonIndicatorsMapping> CommonIndicatorsMapping { get; set; }

        /// <summary>
        /// Gets or sets country plan reviews.
        /// </summary>
        public DbSet<CountryPlanReview> CountryPlanReviews { get; set; }

        /// <summary>
        /// Gets or sets permission.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add code to get only non deleted rows from the table.
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<CountryPlan>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<CountryPlanIndicator>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StrategicAction>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<DetailedActivity>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RoleId");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserRole)
                    .HasForeignKey<UserRole>(d => d.UserId)
                    .HasConstraintName("FK_UserId");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<TechnicalArea>(entity =>
            {
                entity.HasOne(d => d.Source)
                    .WithMany(p => p.TechnicalAreas)
                    .HasForeignKey(d => d.SourceId)
                    .HasConstraintName("FK_Source_TechnicalArea");
            });

            modelBuilder.Entity<TechnicalAreaIndicator>(entity =>
            {
                entity.Property(e => e.IndicatorCode).HasMaxLength(10);

                entity.HasOne(d => d.TechnicalArea)
                    .WithMany(p => p.TechnicalAreaIndicator)
                    .HasForeignKey(d => d.TechnicalAreaId)
                    .HasConstraintName("FK_TechnicalArea_Indicator");
            });

            modelBuilder.Entity<CountryPlan>(entity =>
            {
                entity.Property(e => e.CountryISOCode)
                    .HasMaxLength(10)
                    .HasColumnName("CountryISOCode");

                entity.Property(e => e.PlanCode).HasMaxLength(50);

                entity.Property(e => e.SendReviewReminder)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AssessmentType)
                    .WithMany(p => p.CountryPlans)
                    .HasForeignKey(d => d.AssessmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryPlans_AssessmentTypes");

                entity.HasOne(d => d.PlanStage)
                    .WithMany(p => p.CountryPlans)
                    .HasForeignKey(d => d.PlanStageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryPlans_PlanStages");

                entity.HasOne(d => d.PlanStatus)
                    .WithMany(p => p.CountryPlans)
                    .HasForeignKey(d => d.PlanStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryPlans_PlanStatus");

                entity.HasOne(d => d.PlanType)
                    .WithMany(p => p.CountryPlans)
                    .HasForeignKey(d => d.PlanTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryPlans_PlanTypes");
            });

            modelBuilder.Entity<CommonTechnicalArea>(entity =>
             {
                 entity.Property(e => e.DisplayName).HasMaxLength(1000);
             });

            modelBuilder.Entity<StrategicAction>(entity =>
           {
               entity.Property(e => e.ResponsibleAuthority).HasMaxLength(1000);

               entity.HasOne(d => d.StrategicActionFeasibility)
                   .WithMany(p => p.StrategicActions)
                   .HasForeignKey(d => d.Feasibility)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_StrategicActions_StrategicPlanFeasibility");

               entity.HasOne(d => d.StrategicActionImpact)
                   .WithMany(p => p.StrategicActions)
                   .HasForeignKey(d => d.Impact)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_StrategicActions_StrategicPlanImpacts");

               entity.HasOne(d => d.CountryPlanIndicator)
                   .WithMany(p => p.StrategicActions)
                   .HasForeignKey(d => d.PlanIndicatorId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_StrategicActions_CountryPlanIndicators");

               entity.HasOne(d => d.StrategicActionPriority)
                   .WithMany(p => p.StrategicActions)
                   .HasForeignKey(d => d.Priority)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_StrategicActions_StrategicPlanPriorities");
           });

            modelBuilder.Entity<StrategicActionFeasibility>(entity =>
            {
                entity.Property(e => e.Feasibility).HasMaxLength(50);
            });

            modelBuilder.Entity<StrategicActionImpact>(entity =>
            {
                entity.Property(e => e.Impact).HasMaxLength(50);
            });

            modelBuilder.Entity<StrategicActionPriority>(entity =>
            {
                entity.Property(e => e.Priority).HasMaxLength(50);
            });

            modelBuilder.Entity<DetailedActivity>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(1000);

                entity.Property(e => e.Deadline).HasMaxLength(1000);

                entity.Property(e => e.Donor).HasMaxLength(1000);

                entity.Property(e => e.EstimatedBudgetSource).HasMaxLength(1000);

                entity.Property(e => e.Responsible).HasMaxLength(1000);

                entity.Property(e => e.RiskName).HasMaxLength(1000);

                entity.HasOne(d => d.StrategicActionFeasibility)
                    .WithMany()
                    .HasForeignKey(d => d.Feasibility)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailedActivities_StrategicPlanFeasibility");

                entity.HasOne(d => d.StrategicActionImpact)
                    .WithMany()
                    .HasForeignKey(d => d.Impact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailedActivities_StrategicPlanImpacts");

                entity.HasOne(d => d.CollaboratingInstitution)
                    .WithMany()
                    .HasForeignKey(d => d.InstituteId)
                    .HasConstraintName("FK_DetailedActivities_CollaboratingInstitutions");

                entity.HasOne(d => d.StrategicActionPriority)
                    .WithMany()
                    .HasForeignKey(d => d.Priority)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailedActivities_StrategicPlanPriorities");

                entity.HasOne(d => d.StrategicAction)
                    .WithMany()
                    .HasForeignKey(d => d.StrategicActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailedActivities_StrategicActions");
            });

            modelBuilder.Entity<DetailedActivityType>(entity =>
            {
                entity.Property(e => e.Activity).HasMaxLength(1000);
            });

            modelBuilder.Entity<CollaboratingInstitution>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(1000);
            });

            modelBuilder.Entity<CountryPlanReview>(entity =>
            {
                entity.HasOne(d => d.CountryPlan)
                    .WithMany(p => p.CountryPlanReviews)
                    .HasForeignKey(d => d.CountryPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryPlanReviews_CountryPlans");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_CurrencyId");
            });
        }

        /// <summary>
        /// Fill the Audit Properties.
        /// </summary>
        private void OnBeforeSaving()
        {
            var currentUsername = !string.IsNullOrEmpty(this.httpContextAccessor?.HttpContext?.User?.FindFirst("user_id")?.Value)
                ? this.httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value
                : null;

            _ = int.TryParse(currentUsername, out int currentLoginId);

            // This is to prevent overriding the value with 0 when no login was present.
            if (currentLoginId != 0)
            {
                var entries = this.ChangeTracker.Entries();
                foreach (var entry in entries)
                {
                    if (entry.Entity is Common.ITrackable trackable)
                    {
                        var now = DateTime.UtcNow;
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                trackable.LastUpdatedAt = now;
                                trackable.LastUpdatedBy = currentLoginId;
                                break;

                            case EntityState.Added:
                                trackable.CreatedAt = now;
                                trackable.CreatedBy = currentLoginId;
                                trackable.LastUpdatedAt = now;
                                trackable.LastUpdatedBy = currentLoginId;
                                break;
                        }
                    }
                }
            }
        }
    }
}