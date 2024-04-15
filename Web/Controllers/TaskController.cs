using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.Task;
using Application.Wrapper.NewsFeed;
using Application.Wrapper.Attachment;

namespace Web.Controllers
{
    public class TaskController : SidebarMenuController
    {


        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion

        private readonly ILogger<TaskController> _logger;
        private readonly ITaskWrapper _taskWrapper;
        private readonly INewsFeedWrapper _newsWrapper;
        private readonly IAttachmentWrapper _attachmentWrapper;



        public TaskController(ILogger<TaskController> logger, Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
            ITaskWrapper taskWrapper,
             INewsFeedWrapper newsWrapper,
             IAttachmentWrapper attachmentWrapper,
             GlobalController globalController


           ) : base(rolesMenuWrapper, cookieManager,globalController)
        {

            _logger = logger;
            _taskWrapper = taskWrapper;
            _newsWrapper = newsWrapper;
            _attachmentWrapper = attachmentWrapper;
            _globalController = globalController;

            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion

        }


        public ActionResult Index()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            int EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;

            if (ProjectId == 100)
            {
                var data = _taskWrapper.GetListTaskForAdmin().ToList();

                return View(data);
            }
            else
            {
                var data = _taskWrapper.GetListTaskByAssignedEmployeeAndProjects(EmployeeId, ProjectId).ToList();

                return View(data);
            }

        }

        public ActionResult PlanningBoard()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            int EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;

            if (ProjectId == 100)
            {
                var data = _taskWrapper.GetListTaskForAdmin().ToList();

                return View(data);
            }
            else
            {
                var data = _taskWrapper.GetListTaskByAssignedEmployeeAndProjects(EmployeeId, ProjectId).ToList();

                return View(data);
            }
        }


        public ActionResult IndexForAdminProject()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            int EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskWrapper.GetListTaskByProjectId(ProjectId).ToList();
            return View(data);
        }

        public ActionResult Create()
        {

            _cookieManager.SetCookie("CreateTask",Guid.NewGuid().ToString());
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormTaskModel model)
        {
            Core.Dto.PMDb.Task formdata = new Core.Dto.PMDb.Task();

            formdata.Taskgroupid = model.TaskData.Taskgroupid;
            formdata.Taskname = model.TaskData.Taskname;
            formdata.Projectid = model.TaskData.Projectid;
            formdata.Startdate = model.TaskData.Startdate;
            formdata.Duedate = model.TaskData.Duedate;
            formdata.Severityid = model.TaskData.Severityid;
            formdata.Assignto = model.TaskData.Assignto;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Createdby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Createddate = DateTime.Now;
            formdata.Statusid = 3;
            formdata.Type = 1;
            formdata.Reviewby = model.TaskData.Reviewby;
            _taskWrapper.CreateData(formdata);

            var getguid = _cookieManager.GetCookie("CreateTask") != null ? _cookieManager.GetCookie("CreateTask") : "";
            var getattachmentfiles = GlobalController.ListCreateTask.Where(x => x.GUID == getguid).ToList();

            foreach (var i in getattachmentfiles)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                //AttachmentFilemodel.DocumentId = formdata.id;
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Istaskdocument = true;
                AttachmentFilemodel.Taskid = formdata.Id; ;
                AttachmentFilemodel.Url = i.Path;
                _attachmentWrapper.CreateData(AttachmentFilemodel);
            }

            _newsWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.CreatedNew, (int)formdata.Id, (int)formdata.Type);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(FormTaskModel model, string Command)
        {
            Core.Dto.PMDb.Task formdata = new Core.Dto.PMDb.Task();
            formdata.Id = model.TaskData.Id;
            formdata.Taskgroupid = model.TaskData.Taskgroupid;
            formdata.Taskname = model.TaskData.Taskname;
            formdata.Projectid = model.TaskData.Projectid;
            formdata.Startdate = model.TaskData.Startdate;
            formdata.Duedate = model.TaskData.Duedate;
            formdata.Severityid = model.TaskData.Severityid;
            formdata.Assignto = model.TaskData.Assignto;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Updateby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Updatedate = DateTime.Now;
            formdata.Createdby = model.TaskData.Createdby;
            formdata.Createddate = model.TaskData.Createddate;
            formdata.Type = model.TaskData.Type;
            //formdata.StatusId = model.TaskData.StatusId;


            formdata.Descripition = model.TaskData.Descripition;
            formdata.Reviewby = model.TaskData.Reviewby;


            if (Command == "Mark As In Progress")
            {
                formdata.Statusid = 4;

                _newsWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.InProgress, (int)formdata.Id, (int)formdata.Type);

            }

            else if (Command == "Mark As In Review")
            {
                formdata.Statusid = 7;
                _newsWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.InReview, (int)formdata.Id, (int)formdata.Type);
            }

            else
            {
                formdata.Statusid = model.TaskData.Statusid;
                _newsWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Edited, (int)formdata.Id, (int)formdata.Type);
            }


            _taskWrapper.Update(formdata);

            var getguid = _cookieManager.GetCookie("EditTask") != null ? _cookieManager.GetCookie("EditTask") : "";
            var listdataattach = GlobalController.ListEditTask.Where(x => x.GUID == getguid && x.delflag == false).ToList();

            var getolddataAttachment = _attachmentWrapper.GetDataByTaskId(formdata.Id).ToList();

            foreach (var k in getolddataAttachment)
            {
                _attachmentWrapper.Delete(k);
            }

            //var data = AttachmentLogic.

            foreach (var i in listdataattach)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Istaskdocument = true;
                AttachmentFilemodel.Taskid = formdata.Id; ;
                AttachmentFilemodel.Url = i.Path;
                _attachmentWrapper.CreateData(AttachmentFilemodel);
            }

            //NewsFeedLogic.AddNewsFeed(Convert.ToInt32(Session["EmployeeId"].ToString()), EnumAction.Edited, (int)formdata.id, (int)formdata.Type);


            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Delete(FormTaskModel model)
        {
            Core.Dto.PMDb.Task form = new Core.Dto.PMDb.Task();
            form.Id = model.TaskData.Id;
            _taskWrapper.Delete(form);
            var olddata = _taskWrapper.GetData(form.Id);

            _newsWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Deleted, (int)form.Id, (int)olddata.Type);
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            _cookieManager.SetCookie("EditTask",Guid.NewGuid().ToString());
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskWrapper.GetDataById(id, employeeid);

            foreach (var j in data.AttachmentList)
            {
                var getguid = _cookieManager.GetCookie("EditTask") != null ? _cookieManager.GetCookie("EditTask") : "";
                FormAttachmentFilesModel Attachfilelist = new FormAttachmentFilesModel();
                Attachfilelist.id = j.Id;
                Attachfilelist.GUID = getguid;
                Attachfilelist.Path = j.Url;
                Attachfilelist.TransactionID = id;
                Attachfilelist.filetype = j.Filetype;
                Attachfilelist.delflag = false;
                var checkdata = GlobalController.ListEditTask.Where(x => x.id == j.Id && x.GUID == getguid).Any();
                if (checkdata == false)
                {
                    GlobalController.ListEditTask.Add(Attachfilelist);
                }
            }



            return View(data);
            //return View(data);
        }



        public ActionResult Details(int id)
        {
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskWrapper.GetDataById(id, employeeid);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _taskWrapper.GetDataById(id, employeeid);
            return View(data);
        }
    }
}
