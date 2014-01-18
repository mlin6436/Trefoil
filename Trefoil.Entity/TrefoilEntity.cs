using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Helper;

namespace Trefoil.Entity
{
    public class TrefoilEntity : DbContext, ITrefoilEntity, IDisposable
    {
        public DbSet<BusinessFunction> BusinessFunction { get; set; }
        public DbSet<BusinessStatus> BusinessStatus { get; set; }
        public DbSet<BusinessType> BusinessType { get; set; }
        public DbSet<BusinessValue> BusinessValue { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<PriorityType> PriorityType { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<StateType> StateType { get; set; }
        public DbSet<StatusType> StatusType { get; set; }
        public DbSet<RoleType> RoleType { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<User> User { get; set; }

        public TrefoilEntity()
            : base()
        { }

        public TrefoilEntity(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Log>().HasOptional(l => l.CreatedbySystemuser).WithMany().HasForeignKey(m => m.createdby);
            modelBuilder.Entity<Log>().HasOptional(l => l.ModifiedbySystemuser).WithMany().HasForeignKey(m => m.modifiedby);

            modelBuilder.Entity<Note>().HasOptional(n => n.CreatedbySystemuser).WithMany().HasForeignKey(m => m.createdby);
            modelBuilder.Entity<Note>().HasOptional(n => n.ModifiedbySystemuser).WithMany().HasForeignKey(m => m.modifiedby);

            modelBuilder.Entity<Organisation>().HasRequired(o => o.BusinessStatus).WithMany().HasForeignKey(m => m.businessstatusid);
            modelBuilder.Entity<Organisation>().HasRequired(o => o.BusinessType).WithMany().HasForeignKey(m => m.businesstypeid);
            modelBuilder.Entity<Organisation>().HasOptional(o => o.CreatedByUser).WithMany().HasForeignKey(m => m.createdby);
            modelBuilder.Entity<Organisation>().HasOptional(o => o.ModifiedByUser).WithMany().HasForeignKey(m => m.modifiedby);
            modelBuilder.Entity<Organisation>().HasMany(o => o.Notes).WithMany(n => n.Organisations).Map(m => { m.MapLeftKey("organisationid"); m.MapRightKey("noteid"); m.ToTable("OrganisationNote"); });

            modelBuilder.Entity<Project>().HasMany(p => p.AssignToUsers).WithMany(u => u.Projects).Map(m => { m.MapLeftKey("projectid"); m.MapRightKey("systemuserid"); m.ToTable("ProjectUser"); });
            modelBuilder.Entity<Project>().HasRequired(p => p.BusinessValue).WithMany().HasForeignKey(m => m.businessvalueid);
            modelBuilder.Entity<Project>().HasOptional(p => p.Contact).WithMany().HasForeignKey(m => m.contactid);
            modelBuilder.Entity<Project>().HasOptional(p => p.CreatedByUser).WithMany().HasForeignKey(m => m.createdby);
            modelBuilder.Entity<Project>().HasOptional(p => p.ModifiedByUser).WithMany().HasForeignKey(m => m.modifiedby);
            modelBuilder.Entity<Project>().HasMany(p => p.Notes).WithMany(n => n.Projects).Map(m => { m.MapLeftKey("projectid"); m.MapRightKey("noteid"); m.ToTable("ProjectNote"); });
            modelBuilder.Entity<Project>().HasOptional(p => p.Owner).WithMany().HasForeignKey(m => m.ownerid);
            modelBuilder.Entity<Project>().HasRequired(p => p.Organisation).WithMany().HasForeignKey(m => m.organisationid);
            modelBuilder.Entity<Project>().HasRequired(p => p.PriorityType).WithMany().HasForeignKey(m => m.prioritytypeid);
            modelBuilder.Entity<Project>().HasRequired(p => p.StateType).WithMany().HasForeignKey(m => m.statetypeid);
            modelBuilder.Entity<Project>().HasRequired(p => p.StatusType).WithMany().HasForeignKey(m => m.statustypeid);

            modelBuilder.Entity<Task>().HasMany(t => t.Users).WithMany(u => u.Tasks).Map(m => { m.MapLeftKey("taskid"); m.MapRightKey("systemuserid"); m.ToTable("TaskUser"); });
            modelBuilder.Entity<Task>().HasOptional(t => t.CreatedByUser).WithMany().HasForeignKey(m => m.createdby);
            modelBuilder.Entity<Task>().HasOptional(t => t.ModifiedByUser).WithMany().HasForeignKey(m => m.modifiedby);
            modelBuilder.Entity<Task>().HasMany(t => t.Notes).WithMany(n => n.Tasks).Map(m => { m.MapLeftKey("taskid"); m.MapRightKey("noteid"); m.ToTable("TaskNote"); });
            modelBuilder.Entity<Task>().HasOptional(t => t.Organisation).WithMany().HasForeignKey(m => m.organisationid);
            modelBuilder.Entity<Task>().HasOptional(t => t.Owner).WithMany().HasForeignKey(m => m.ownerid);
            modelBuilder.Entity<Task>().HasRequired(t => t.PriorityType).WithMany().HasForeignKey(m => m.prioritytypeid);
            modelBuilder.Entity<Task>().HasOptional(t => t.Project).WithMany().HasForeignKey(m => m.projectid);
            modelBuilder.Entity<Task>().HasRequired(t => t.StateType).WithMany().HasForeignKey(m => m.statetypeid);
            modelBuilder.Entity<Task>().HasRequired(t => t.StatusType).WithMany().HasForeignKey(m => m.statustypeid);

            modelBuilder.Entity<User>().HasOptional(u => u.BusinessFunction).WithMany().HasForeignKey(m => m.businessfunctionid);
            modelBuilder.Entity<User>().HasOptional(u => u.CreatedByUser).WithMany().HasForeignKey(m => m.createdby);
            modelBuilder.Entity<User>().HasOptional(u => u.ModifiedByUser).WithMany().HasForeignKey(m => m.modifiedby);
            modelBuilder.Entity<User>().HasOptional(u => u.ParentUser).WithMany().HasForeignKey(m => m.parentsystemuserid);
            modelBuilder.Entity<User>().HasOptional(u => u.Organisation).WithMany().HasForeignKey(m => m.organisationid);
            modelBuilder.Entity<User>().HasRequired(u => u.RoleType).WithMany().HasForeignKey(m => m.roletypeid);
        }

        public void DropAndCreate(string serverName, string databaseName, string userId, string password)
        {
            string nameOrConnectionString = String.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};Integrated Security=SSPI;", serverName, databaseName, userId, password);

            using (TrefoilEntity entity = new TrefoilEntity(nameOrConnectionString))
            {
                try
                {
                    Database.SetInitializer<TrefoilEntity>(null);

                    if (entity.Database.Exists())
                    {
                        entity.Database.ExecuteSqlCommand(String.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", databaseName));
                        entity.Database.Delete();
                    }

                    entity.Database.Create();
                    InitialiseAll(entity);
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    var exceptions = ex.GetOriginalException();
                    entity.Database.ExecuteSqlCommand(string.Format("ALTER DATABASE {0} SET MULTI_USER WITH ROLLBACK IMMEDIATE", databaseName));

                    // Log
                    throw exceptions;
                }
            }
        }

        public void Migrate(string serverName, string databaseName, string userId, string password)
        {
            string nameOrConnectionString = String.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};Integrated Security=SSPI;", serverName, databaseName, userId, password);
            string connectionString = String.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI;", serverName, databaseName);
            string providerInvariantName = "System.Data.SqlClient";

            try
            {
                var configuration = new DbMigrationsConfiguration()
                {
                    ContextType = typeof(TrefoilEntity),
                    MigrationsAssembly = typeof(TrefoilEntity).Assembly,
                    AutomaticMigrationsEnabled = false,
                    TargetDatabase = new DbConnectionInfo(connectionString, providerInvariantName), // TODO: nameOrConnectionString
                };

                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }
            catch (Exception ex)
            {
                var exception = ex.GetOriginalException();
            }
        }

        #region Data Initialisation

        public void InitialiseAll(TrefoilEntity trefoilEntity)
        {
            InitialiseBusinessFunction(trefoilEntity);
            InitialiseBusinessStatus(trefoilEntity);
            InitialiseBusinessType(trefoilEntity);
            InitialiseBusinessValue(trefoilEntity);
            InitialiseLog(trefoilEntity);
            InitialiseOrganisation(trefoilEntity);
            InitialisePriorityType(trefoilEntity);
            InitialiseProject(trefoilEntity);
            InitialiseStateType(trefoilEntity);
            InitialiseStatusType(trefoilEntity);
            InitialiseRoleType(trefoilEntity);
            InitialiseTask(trefoilEntity);
            InitialiseUser(trefoilEntity);
        }

        private void InitialiseBusinessFunction(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Marketing",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 2,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Sales",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 3,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Information Technology",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 4,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Executive Board",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 31,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Customer Services",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 32,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Accounts",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 33,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Creative / Design",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 34,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Security",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 35,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "M.P.",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 36,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Commerical Property",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 41,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Reception",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 42,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Owner",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 45,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Administration",
                });

                trefoilEntity.BusinessFunction.Add(new BusinessFunction()
                {
                    businessfunctionid = 46,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Legal",
                });
            }
        }

        private void InitialiseBusinessStatus(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.BusinessStatus.Add(new BusinessStatus()
                {
                    businessstatusid = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Prospective",
                });

                trefoilEntity.BusinessStatus.Add(new BusinessStatus()
                {
                    businessstatusid = 2,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Live",
                });

                trefoilEntity.BusinessStatus.Add(new BusinessStatus()
                {
                    businessstatusid = 3,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Historic",
                });

                trefoilEntity.BusinessStatus.Add(new BusinessStatus()
                {
                    businessstatusid = 4,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Rejected",
                });
            }
        }

        private void InitialiseBusinessType(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Architects",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 2,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Information Technology",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 3,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Legal",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 4,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Events Management",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 5,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Property",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 6,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Retail",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 52,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Recruitment",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 53,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Telecommunications",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 54,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Engineering",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 55,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Agriculture, Forestry and Fishing",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 56,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Arts, Sports and Recreation",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 57,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Construction",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 58,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Education",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 59,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Manufacturing",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 60,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Marketing",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 61,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Wholesale",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 62,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Commercial Property",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 63,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Government",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 65,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Hard House Promoter",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 66,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Sales",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 67,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Chartered Surveyors",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 68,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Industrial Property",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 69,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Banking, Investment, Financial",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 70,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "House Promoter",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 71,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Agents",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 72,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Promotion Data",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 73,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Contractors",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 74,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Leisure",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 75,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Adult Entertainment",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 76,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "GambDickg",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 77,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Hospitality",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 78,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Waste Disposal",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 79,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Music",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 80,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Media",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 81,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Medical",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 83,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Charity",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 84,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Photography",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 85,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Financial",
                });

                trefoilEntity.BusinessType.Add(new BusinessType()
                {
                    businesstypeid = 86,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Investments",
                });
            }
        }

        private void InitialiseBusinessValue(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.BusinessValue.Add(new BusinessValue()
                {
                    businessvalueid = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Must Have",
                });

                trefoilEntity.BusinessValue.Add(new BusinessValue()
                {
                    businessvalueid = 2,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Great",
                });

                trefoilEntity.BusinessValue.Add(new BusinessValue()
                {
                    businessvalueid = 3,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Good",
                });

                trefoilEntity.BusinessValue.Add(new BusinessValue()
                {
                    businessvalueid = 4,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Average",
                });

                trefoilEntity.BusinessValue.Add(new BusinessValue()
                {
                    businessvalueid = 5,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Nice To Have",
                });
            }
        }

        private void InitialiseLog(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.Log.Add(new Log()
                {
                    applicationname = "Orange",
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "Test log",
                    isresolved = false,
                    levelcode = LevelCode.Debug,
                    logid = 1,
                    modifiedby = null,
                    modifiedon = null,
                    subject = "First log",
                });

                trefoilEntity.Note.Add(new Note()
                {
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    noteid = 2,
                });
            }
        }

        private void InitialiseOrganisation(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.Organisation.Add(new Organisation()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = null,
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessstatusid = 2,
                    businesstypeid = 2,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    name = "Orange",
                    organisationid = 1,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    website = "",
                });

                trefoilEntity.Organisation.Add(new Organisation()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = null,
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessstatusid = 2,
                    businesstypeid = 2,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    name = "Banana",
                    organisationid = 1,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    website = "",
                });

                trefoilEntity.Organisation.Add(new Organisation()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = null,
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessstatusid = 2,
                    businesstypeid = 2,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    name = "Apple",
                    organisationid = 1,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    website = "",
                });

                trefoilEntity.Organisation.Add(new Organisation()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = null,
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessstatusid = 2,
                    businesstypeid = 2,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    name = "Pear",
                    organisationid = 1,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    website = "",
                });
            }
        }

        private void InitialisePriorityType(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.PriorityType.Add(new PriorityType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "High",
                    prioritytypeid = 1,
                });

                trefoilEntity.PriorityType.Add(new PriorityType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Normal",
                    prioritytypeid = 2,
                });

                trefoilEntity.PriorityType.Add(new PriorityType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Low",
                    prioritytypeid = 3,
                });
            }
        }
        
        private void InitialiseProject(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.Project.Add(new Project()
                {
                    actualdurationminutes = 100,
                    actualend = DateTime.UtcNow,
                    actualstart = DateTime.UtcNow.AddDays(-1),
                    budget = 1000.00,
                    businessvalueid = 1,
                    cancelledreason = null,
                    contactid = 1,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    isbilled = false,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 1,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 1,
                    statetypeid = 5,
                    statustypeid = 3,
                    rejectedreason = null,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow,
                    scheduledstart = DateTime.UtcNow.AddDays(-1),
                    subject = "Play a gig",
                    totalcharge = 1000.00,
                    totalcost = 500.00,
                });

                trefoilEntity.Project.Add(new Project()
                {
                    actualdurationminutes = 100,
                    actualend = DateTime.UtcNow,
                    actualstart = DateTime.UtcNow.AddDays(-1),
                    budget = 1000.00,
                    businessvalueid = 1,
                    cancelledreason = null,
                    contactid = 1,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    isbilled = false,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 2,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 2,
                    statetypeid = 5,
                    statustypeid = 3,
                    rejectedreason = null,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow.AddDays(2),
                    scheduledstart = DateTime.UtcNow.AddDays(3),
                    subject = "Roll a fag",
                    totalcharge = 1000.00,
                    totalcost = 500.00,
                });

                trefoilEntity.Project.Add(new Project()
                {
                    actualdurationminutes = 100,
                    actualend = DateTime.UtcNow,
                    actualstart = DateTime.UtcNow.AddDays(-1),
                    budget = 1000.00,
                    businessvalueid = 1,
                    cancelledreason = null,
                    contactid = 1,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    isbilled = false,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 2,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 3,
                    statetypeid = 5,
                    statustypeid = 3,
                    rejectedreason = null,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow.AddDays(3),
                    scheduledstart = DateTime.UtcNow.AddDays(4),
                    subject = "Have a nap",
                    totalcharge = 1000.00,
                    totalcost = 500.00,
                });

                trefoilEntity.Project.Add(new Project()
                {
                    actualdurationminutes = null,
                    actualend = null,
                    actualstart = null,
                    budget = 1000.00,
                    businessvalueid = 1,
                    cancelledreason = null,
                    contactid = 1,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    isbilled = false,
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 2,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 4,
                    statetypeid = 5,
                    statustypeid = 3,
                    rejectedreason = null,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow.AddDays(5),
                    scheduledstart = DateTime.UtcNow.AddDays(6),
                    subject = "Sun bathing",
                    totalcharge = 1000.00,
                    totalcost = 500.00,
                });
            }
        }
        
        private void InitialiseStateType(TrefoilEntity trefoilEntity)
        {                    
            if (trefoilEntity != null)
            {
                trefoilEntity.StateType.Add(new StateType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Not Started",
                    statetypeid = 1,
                });

                trefoilEntity.StateType.Add(new StateType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "In Design",
                    statetypeid = 2,
                });

                trefoilEntity.StateType.Add(new StateType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "In Development",
                    statetypeid = 3,
                });

                trefoilEntity.StateType.Add(new StateType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "In Testing",
                    statetypeid = 4,
                });

                trefoilEntity.StateType.Add(new StateType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Done",
                    statetypeid = 5,
                });
            }
        }
        
        private void InitialiseStatusType(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.StatusType.Add(new StatusType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Proposal",
                    statustypeid = 1,
                });

                trefoilEntity.StatusType.Add(new StatusType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Live",
                    statustypeid = 2,
                });

                trefoilEntity.StatusType.Add(new StatusType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Complete",
                    statustypeid = 3,
                });

                trefoilEntity.StatusType.Add(new StatusType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Cancelled",
                    statustypeid = 4,
                });

                trefoilEntity.StatusType.Add(new StatusType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    modifiedon = null,
                    name = "Rejected",
                    statustypeid = 5,
                });
            }
        }

        private void InitialiseRoleType(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.RoleType.Add(new RoleType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    name = "Administrator",
                    roletypeid = 1,
                });

                trefoilEntity.RoleType.Add(new RoleType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    name = "Account",
                    roletypeid = 2,
                });

                trefoilEntity.RoleType.Add(new RoleType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    name = "Internal",
                    roletypeid = 3,
                });

                trefoilEntity.RoleType.Add(new RoleType()
                {
                    createdon = DateTime.UtcNow,
                    disabledreason = null,
                    isdisabled = false,
                    name = "Client",
                    roletypeid = 4,
                });
            }
        }

        private void InitialiseTask(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.Task.Add(new Task()
                {
                    actualdurationminutes = null,
                    actualend = null,
                    actualstart = null,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "The first task in the list",
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 1,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 1,
                    statetypeid = 1,
                    statustypeid = 1,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow.AddDays(1),
                    scheduledstart = DateTime.UtcNow,
                    subject = "The first task subject",
                    taskid = 1,
                });

                trefoilEntity.Task.Add(new Task()
                {
                    actualdurationminutes = null,
                    actualend = null,
                    actualstart = null,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "The 2nd task in the list",
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 1,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 1,
                    statetypeid = 1,
                    statustypeid = 1,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow.AddDays(1),
                    scheduledstart = DateTime.UtcNow,
                    subject = "The 2nd task subject",
                    taskid = 2,
                });

                trefoilEntity.Task.Add(new Task()
                {
                    actualdurationminutes = null,
                    actualend = null,
                    actualstart = null,
                    createdby = 1,
                    createdon = DateTime.UtcNow,
                    description = "The 3rd task in the list",
                    isdisabled = false,
                    modifiedby = null,
                    modifiedon = null,
                    organisationid = 1,
                    ownerid = 1,
                    prioritytypeid = 1,
                    projectid = 2,
                    statetypeid = 1,
                    statustypeid = 1,
                    scheduleddurationminutes = 100,
                    scheduledend = DateTime.UtcNow.AddDays(1),
                    scheduledstart = DateTime.UtcNow,
                    subject = "The 3rd task subject",
                    taskid = 3,
                });
            }
        }
        
        private void InitialiseUser(TrefoilEntity trefoilEntity)
        {
            if (trefoilEntity != null)
            {
                trefoilEntity.User.Add(new User()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = "",
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessfunctionid = 42,
                    createdby = null,
                    createdon = null,
                    disabledreason = null,
                    emailaddress = @"admin@orange.com",
                    firstname = "Admin",
                    fullname = "Admin",
                    homephone = null,
                    interest = null,
                    isdisabled = false,
                    isinternal = true,
                    jobtitle = "Admin",
                    lastname = null,
                    middlename = null,
                    mobilephone = null,
                    modifiedby = null,
                    modifiedon = null,
                    nickname = null,
                    organisationid = null,
                    parentsystemuserid = null,
                    password = "Carr1er",
                    roletypeid = 1,
                    systemuserid = 1,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    title = null,
                    website = "",
                });

                trefoilEntity.User.Add(new User()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = "",
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessfunctionid = 42,
                    createdby = null,
                    createdon = null,
                    disabledreason = null,
                    emailaddress = @"lee.Smith@orange.com",
                    firstname = "Lee",
                    fullname = "Lee Smith",
                    homephone = null,
                    interest = null,
                    isdisabled = false,
                    isinternal = true,
                    jobtitle = "Director",
                    lastname = "Smith",
                    middlename = null,
                    mobilephone = "01234567890",
                    modifiedby = null,
                    modifiedon = null,
                    nickname = null,
                    organisationid = 1,
                    parentsystemuserid = 1,
                    password = "starlight",
                    roletypeid = 1,
                    systemuserid = 2,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    title = null,
                    website = "",
                });

                trefoilEntity.User.Add(new User()
                {
                    address1_city = "London",
                    address1_country = "United Kingdom",
                    address1_county = "London",
                    address1_fax = null,
                    address1_line1 = "1 Piccadilly",
                    address1_line2 = null,
                    address1_line3 = null,
                    address1_name = "Orange",
                    address1_postalcode = "W1J 1XX",
                    address1_postofficebox = null,
                    address1_telephone1 = "",
                    address1_telephone2 = null,
                    address1_telephone3 = null,
                    businessfunctionid = 3,
                    createdby = null,
                    createdon = null,
                    disabledreason = null,
                    emailaddress = @"Merry.Dick@orange.com",
                    firstname = "Merry",
                    fullname = "Merry Dick",
                    homephone = null,
                    interest = null,
                    isdisabled = false,
                    isinternal = true,
                    jobtitle = "Developer",
                    lastname = "Dick",
                    middlename = null,
                    mobilephone = null,
                    modifiedby = null,
                    modifiedon = null,
                    nickname = null,
                    organisationid = 1,
                    parentsystemuserid = 1,
                    password = "spaceman",
                    roletypeid = 1,
                    systemuserid = 3,
                    social_facebook = "",
                    social_linkedin = "",
                    social_twitter = "",
                    social_vimeo = "",
                    social_youtube = "",
                    title = null,
                    website = "",
                });
            }
        }

        #endregion
    }
}
