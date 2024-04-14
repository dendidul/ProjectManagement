using Infrastructure.Helper.Config;
using Infrastructure.Helper.Encryption;
using Infrastructure.Helper.Auth;
using Application.Wrapper.Auth;
using Application.Repositories.UserApi;

using Application.Wrapper.Utility;

using Infrastructure.Repository;

using Infrastructure.Helper.Upload;
using Application.ExternalAPI.NewKalbeConnect;
using Infrastructure.WebServices;
using Infrastructure.Cache;

using Infrastructure.Helper.Notif;
using Infrastructure.Helper.Common;
using Application.ExternalAPI.LoyaltyAPI;
using Application.Repositories.Attachment;
using Application.Repositories.Users;
using Application.Wrapper.Users;
using Application.Repositories.Bugs;
using Application.Repositories.Comment;
using Application.Repositories.Category;
using Application.Repositories.CustomerLink;
using Application.Repositories.Department;
using Application.Repositories.Document;
using Application.Repositories.Employee;
using Application.Repositories.Global;
using Application.Repositories.Menu;
using Application.Repositories.NewsFeed;
using Application.Repositories.Position;
using Application.Repositories.Profile;
using Application.Repositories.Project;
using Application.Repositories.ProjectGroup;
using Application.Repositories.Roles;
using Application.Repositories.RolesMenu;
using Application.Repositories.RolesProjectEmployee;
using Application.Repositories.Severity;
using Application.Repositories.Status;
using Application.Repositories.Task;
using Application.Repositories.TaskDayActivity;
using Application.Repositories.TaskGroup;
using Application.Repositories.TaskLog;
using Application.Repositories.Timesheet;
using Application.Repositories.Types;
using Application.Wrapper.Bugs;
using Application.Wrapper.Category;
using Application.Wrapper.Comment;
using Application.Wrapper.Department;
using Application.Wrapper.Document;
using Application.Wrapper.Employee;
using Application.Wrapper.Global;
using Application.Wrapper.Menu;
using Application.Wrapper.NewsFeed;
using Application.Wrapper.Types;
using Application.Wrapper.Timesheet;
using Application.Wrapper.TaskLog;
using Application.Wrapper.TaskGroup;
using Application.Wrapper.TaskDayActivity;
using Application.Wrapper.Task;
using Application.Wrapper.Status;
using Application.Wrapper.Severity;
using Application.Wrapper.RolesProjectEmployee;
using Application.Wrapper.RolesMenu;
using Application.Wrapper.Roles;
using Application.Wrapper.ProjectGroup;
using Application.Wrapper.Project;
using Application.Wrapper.Profile;
using Application.Wrapper.Position;
using Application.Wrapper.Attachment;
using Web.Controllers;

namespace Web.AppConfig
{
    public static class AppDependency
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddSingleton<IConfigCreatorHelper, ConfigCreatorHelper>();
            services.AddSingleton<ISignature, Signature>();
            services.AddSingleton(typeof(IDataAccessClientRepository), typeof(DataAccessClientRepository));

            services.AddScoped<IAuthAccessToken, AuthAccessToken>();
            services.AddScoped<IStringEncryption, StringEncryption>();
            services.AddScoped<IAccessTokenHelper, AccessTokenHelper>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<IValidateAccessToken, ValidateAccessToken>();
            services.AddScoped<IUserAPIDA, UserAPIDA>();
            // services.AddScoped<AuthorizedAccessTokenAttribute>();
            services.AddScoped<IUtilityWrapper, UtilityWrapper>();

            services.AddScoped<IRedisCache, RedisCache>();
            services.AddTransient<INewKalbeConnectAPI, NewKalbeConnectAPI>();
            services.AddTransient<IKalbeConnectAPIService, KalbeConnectAPIService>();
            services.AddScoped<Web.Utils.CookieManager>();
            services.AddScoped<Web.Utils.CMSAuthorize>();

            services.AddScoped<IUploadFilesHelper, UploadFilesHelper>();

            services.AddScoped<INotifEmail, NotifEmail>();
            services.AddScoped<ICommonHelper, CommonHelper>();
            services.AddScoped<ILoyaltyAPIService, LoyaltyAPIService>();
            services.AddScoped<ILoyaltyAPI, LoyaltyAPI>();

            services.AddScoped<IAttachmentDA, AttachmentDA>();
            services.AddScoped<IUserDA, UserDA>();
            services.AddScoped<IBugsDA, BugsDA>();
            services.AddScoped<ICategoryDA, CategoryDA>();
            services.AddScoped<ICommentDA, CommentDA>();           
            services.AddScoped<IDepartmentDA, DepartmentDA>();
            services.AddScoped<IDocumentDA, DocumentDA>();
            services.AddScoped<IEmployeeDA, EmployeeDA>();
            services.AddScoped<IGlobalDA, GlobalDA>();
            services.AddScoped<IMenuDA, MenuDA>();
            services.AddScoped<INewsFeedDA, NewsFeedDA>();
            services.AddScoped<IPositionDA, PositionDA>();
            services.AddScoped<IProfileDA, ProfileDA>();
            services.AddScoped<IProjectDA, ProjectDA>();
            services.AddScoped<IProjectGroupDA, ProjectGroupDA>();
            services.AddScoped<IRolesDA, RolesDA>();
            services.AddScoped<IRolesMenuDA, RolesMenuDA>();
            services.AddScoped<IRolesProjectEmployeeDA, RolesProjectEmployeeDA>();
            services.AddScoped<ISeverityDA, SeverityDA>();
            services.AddScoped<IStatusDA, StatusDA>();
            services.AddScoped<ITaskDA, TaskDA>();
            services.AddScoped<ITaskDayActivityDA, TaskDayActivityDA>();
            services.AddScoped<ITaskGroupDA, TaskGroupDA>();
            services.AddScoped<ITaskLogDA, TaskLogDA>();
            services.AddScoped<ITimesheetDA, TimesheetDA>();
            services.AddScoped<ITypesDA, TypesDA>();
            services.AddScoped<IUserDA, UserDA>();
            services.AddScoped<GlobalController>();

            services.AddScoped<IProjectWrapper, ProjectWrapper>();

            services.AddScoped<IAttachmentWrapper, AttachmentWrapper>();
            services.AddScoped<IBugsWrapper, BugsWrapper>();
            services.AddScoped<ICategoryWrapper, CategoryWrapper>();
            services.AddScoped<ICommentWrapper, CommentWrapper>();           
            services.AddScoped<IDepartmentWrapper, DepartmentWrapper>();
            services.AddScoped<IDocumentWrapper, DocumentWrapper>();
            services.AddScoped<IEmployeeWrapper, EmployeeWrapper>();
            services.AddScoped<IGlobalWrapper, GlobalWrapper>();
            services.AddScoped<IMenuWrapper, MenuWrapper>();
            services.AddScoped<INewsFeedWrapper, NewsFeedWrapper>();
            services.AddScoped<IPositionWrapper, PositionWrapper>();
            services.AddScoped<IProfileWrapper, ProfileWrapper>();
           
            services.AddScoped<IProjectGroupWrapper, ProjectGroupWrapper>();
            services.AddScoped<IRolesWrapper, RolesWrapper>();
            services.AddScoped<IRolesMenuWrapper, RolesMenuWrapper>();
            services.AddScoped<IRolesProjectEmployeeWrapper, RolesProjectEmployeeWrapper>();
            services.AddScoped<ISeverityWrapper, SeverityWrapper>();
            services.AddScoped<IStatusWrapper, StatusWrapper>();
            services.AddScoped<ITaskWrapper, TaskWrapper>();
            services.AddScoped<ITaskDayActivityWrapper, TaskDayActivityWrapper>();
            services.AddScoped<ITaskGroupWrapper, TaskGroupWrapper>();
            services.AddScoped<ITaskLogWrapper, TaskLogWrapper>();
            services.AddScoped<ITimesheetWrapper, TimesheetWrapper>();
            services.AddScoped<ITypesWrapper, TypesWrapper>();
            services.AddScoped<IUsersWrapper, UsersWrapper>();


            return services;
        }
    }
}
