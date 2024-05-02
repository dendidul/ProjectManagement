using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Core.Dto.PMDb;
using Infrastructure.Helper.Config;

namespace Infrastructure.Context
{
    public partial class ProjectManagementEntities : DbContext
    {
        public ProjectManagementEntities()
        {

        }

        private readonly IConfigCreatorHelper _configCreator;


        public ProjectManagementEntities(DbContextOptions<ProjectManagementEntities> options,
            IConfigCreatorHelper configCreator)
            : base(options)
        {
            _configCreator = configCreator;
        }

        public virtual DbSet<Attachmentfile> Attachmentfiles { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Newsfeed> Newsfeeds { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Projectgroup> Projectgroups { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Rolesmenu> Rolesmenus { get; set; } = null!;
        public virtual DbSet<Rolesprojectemployee> Rolesprojectemployees { get; set; } = null!;
        public virtual DbSet<Severity> Severities { get; set; } = null!;

        public virtual DbSet<UserProjectModel> UserProjectModels { get; set; } = null!;

        public virtual DbSet<ViewRolesProjectEmployee> ViewRolesProjectEmployees { get; set; } = null!;

        public virtual DbSet<ViewAllEmployee> ViewAllEmployees { get; set; } = null!;
        public virtual DbSet<ViewAllProject> ViewAllProjects { get; set; } = null!;
        public virtual DbSet<ViewDocumentByProject> ViewDocumentByProjects { get; set; } = null!;



        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Core.Dto.PMDb.Task> Tasks { get; set; } = null!;
        public virtual DbSet<Taskdayactivity> Taskdayactivities { get; set; } = null!;
        public virtual DbSet<Taskgroup> Taskgroups { get; set; } = null!;
        public virtual DbSet<Tasklog> Tasklogs { get; set; } = null!;
        public virtual DbSet<Core.Dto.PMDb.Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<EventViewModel> EventViewModels { get; set; } = null!;

        public virtual DbSet<CalendarViewModel> CalendarViewModels { get; set; } = null!;

        public virtual DbSet<NewsFeedModels> NewsFeedModels { get; set; } = null!;

        public virtual DbSet<ViewProjectGroupEmployeeModels> ViewProjectGroupEmployeeModels { get; set; } = null!;

        public virtual DbSet<ViewModelProjectGroup> ViewModelProjectGroup { get; set; } = null!;

        public virtual DbSet<TaskDayActivities> TaskDayActivitieses { get; set; } = null!;

        public virtual DbSet<TaskModels> TaskModels { get; set; } = null!;

        public virtual DbSet<TimesheetModel> TimesheetModels { get; set; } = null!;

        public virtual DbSet<TaskGroupModel> TaskGroupModels { get; set; } = null!;

        







        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

                  optionsBuilder.UseNpgsql("User ID=oryhuliu;Password=dg3hjPij_qj9Jn5pQ2nPUVab5baJG0kH;Host=kiouni.db.elephantsql.com;Port=5432;Database=oryhuliu;");

               // optionsBuilder.UseNpgsql(_configCreator.Get("ConnectionDB:ProjectManagement:Constring"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                 modelBuilder.Entity<UserProjectModel>().HasNoKey();
            modelBuilder.Entity<EventViewModel>().HasNoKey();
            modelBuilder.Entity<CalendarViewModel>().HasNoKey();
            modelBuilder.Entity<NewsFeedModels>().HasNoKey();
            modelBuilder.Entity<ViewModelProjectGroup>().HasNoKey();

            modelBuilder.Entity<TaskDayActivities>().HasNoKey();

            modelBuilder.Entity<TaskModels>().HasNoKey();

            modelBuilder.Entity<TimesheetModel>().HasNoKey();

            modelBuilder.Entity<ViewProjectGroupEmployeeModels>().HasNoKey();

            modelBuilder.Entity<ViewAllEmployee>().HasNoKey();
            modelBuilder.Entity<ViewAllProject>().HasNoKey();
            modelBuilder.Entity<ViewDocumentByProject>().HasNoKey();
            modelBuilder.Entity<ViewRolesProjectEmployee>().HasNoKey();
            modelBuilder.Entity<TaskGroupModel>().HasNoKey();
            

            modelBuilder.Entity<Attachmentfile>(entity =>
            {
                entity.ToTable("attachmentfiles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attachmenttype).HasColumnName("attachmenttype");

                entity.Property(e => e.Bugid)
                    .HasMaxLength(10)
                    .HasColumnName("bugid");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Filetype)
                    .HasMaxLength(50)
                    .HasColumnName("filetype");

                entity.Property(e => e.Isbugdocument).HasColumnName("isbugdocument");

                entity.Property(e => e.Isdocumentid).HasColumnName("isdocumentid");

                entity.Property(e => e.Istaskdocument).HasColumnName("istaskdocument");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(50)
                    .HasColumnName("categoryname");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description)
                    .HasMaxLength(10)
                    .HasColumnName("description");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Newsfeedid).HasColumnName("newsfeedid");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Departmentname)
                    .HasMaxLength(50)
                    .HasColumnName("departmentname");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Documentname)
                    .HasMaxLength(50)
                    .HasColumnName("documentname");

                entity.Property(e => e.IsPublic)
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Projectid).HasColumnName("projectid");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Departmentid).HasColumnName("departmentid");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Photourl).HasColumnName("photourl");

                entity.Property(e => e.Positionid).HasColumnName("positionid");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Controllername)
                    .HasMaxLength(50)
                    .HasColumnName("controllername");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Menuname)
                    .HasMaxLength(50)
                    .HasColumnName("menuname");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Sequence).HasColumnName("sequence");
            });

            modelBuilder.Entity<Newsfeed>(entity =>
            {
                entity.ToTable("newsfeed");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createddate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("position");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Positionname)
                    .HasMaxLength(50)
                    .HasColumnName("positionname");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("projects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby)
                    .HasMaxLength(50)
                    .HasColumnName("createdby");

                entity.Property(e => e.Datetimecreated)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("datetimecreated");

                entity.Property(e => e.Datetimeupdated)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("datetimeupdated");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Ispublic).HasColumnName("ispublic");

                entity.Property(e => e.Projectgroupid).HasColumnName("projectgroupid");

                entity.Property(e => e.Projectsname)
                    .HasMaxLength(50)
                    .HasColumnName("projectsname");

                entity.Property(e => e.Updateby)
                    .HasMaxLength(50)
                    .HasColumnName("updateby");
            });

            modelBuilder.Entity<Projectgroup>(entity =>
            {
                entity.ToTable("projectgroup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Ispublic)
                    .HasColumnName("ispublic")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Projectgroupname)
                    .HasMaxLength(50)
                    .HasColumnName("projectgroupname");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Rolesname)
                    .HasMaxLength(50)
                    .HasColumnName("rolesname");
            });

            modelBuilder.Entity<Rolesmenu>(entity =>
            {
                entity.ToTable("rolesmenu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.Property(e => e.Rolesid).HasColumnName("rolesid");
            });

            modelBuilder.Entity<Rolesprojectemployee>(entity =>
            {
                entity.ToTable("rolesprojectemployee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Empid).HasColumnName("empid");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Roleid).HasColumnName("roleid");
            });

            modelBuilder.Entity<Severity>(entity =>
            {
                entity.ToTable("severity");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Descripiton).HasColumnName("descripiton");

                entity.Property(e => e.Severityname)
                    .HasMaxLength(50)
                    .HasColumnName("severityname");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Statusname)
                    .HasMaxLength(50)
                    .HasColumnName("statusname");
            });

            modelBuilder.Entity<Core.Dto.PMDb.Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Assignto).HasColumnName("assignto");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Descripition).HasColumnName("descripition");

                entity.Property(e => e.Duedate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("duedate");

                entity.Property(e => e.Estimatetime)
                    .HasPrecision(20, 1)
                    .HasColumnName("estimatetime");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Result).HasColumnName("result");

                entity.Property(e => e.Reviewby).HasColumnName("reviewby");

                entity.Property(e => e.Severityid).HasColumnName("severityid");

                entity.Property(e => e.Startdate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("startdate");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Taskgroupid).HasColumnName("taskgroupid");

                entity.Property(e => e.Taskname).HasColumnName("taskname");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Updateby).HasColumnName("updateby");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("updatedate");
            });

            modelBuilder.Entity<Taskdayactivity>(entity =>
            {
                entity.ToTable("taskdayactivity");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Estimate)
                    .HasPrecision(20, 1)
                    .HasColumnName("estimate");

                entity.Property(e => e.Startdate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("startdate");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Taskgroup>(entity =>
            {
                entity.ToTable("taskgroup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Enddate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("enddate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Startdate)
                    .HasColumnType("timestamp(0) without time zone")
                    .HasColumnName("startdate");

                entity.Property(e => e.Taskgroupname)
                    .HasMaxLength(50)
                    .HasColumnName("taskgroupname");
            });

            modelBuilder.Entity<Tasklog>(entity =>
            {
                entity.ToTable("tasklog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Assignto).HasColumnName("assignto");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Descripition).HasColumnName("descripition");

                entity.Property(e => e.Duedate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("duedate");

                entity.Property(e => e.Estimatetime).HasColumnName("estimatetime");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Result).HasColumnName("result");

                entity.Property(e => e.Reviewby).HasColumnName("reviewby");

                entity.Property(e => e.Severityid).HasColumnName("severityid");

                entity.Property(e => e.Startdate)
                    .HasColumnType("timestamp(3) without time zone")
                    .HasColumnName("startdate");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Taskgroupid).HasColumnName("taskgroupid");

                entity.Property(e => e.Taskid).HasColumnName("taskid");

                entity.Property(e => e.Taskname).HasColumnName("taskname");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Core.Dto.PMDb.Type>(entity =>
            {
                entity.ToTable("type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag)
                    .HasColumnName("del_flag")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Typename)
                    .HasMaxLength(50)
                    .HasColumnName("typename");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlag).HasColumnName("del_flag");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Rolesid).HasColumnName("rolesid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
