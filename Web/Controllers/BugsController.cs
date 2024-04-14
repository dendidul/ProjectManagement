using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.TaskLog;
using Application.Wrapper.Bugs;
using Application.Wrapper.Task;
using Application.Wrapper.Attachment;
using Application.Wrapper.NewsFeed;

namespace Web.Controllers
{
    public class BugsController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;


        #endregion

        private readonly ILogger<BugsController> _logger;

        private readonly ITaskLogWrapper _taskLogWrapper;
        private readonly IBugsWrapper _bugsWrapper;
        private readonly ITaskWrapper _taskWrapper;
        private readonly IAttachmentWrapper _attachmentWrapper;
        private readonly INewsFeedWrapper _newsFeedWrapper;


        public BugsController(ILogger<BugsController> logger,
            ITaskLogWrapper taskLogWrapper,
            IBugsWrapper bugsWrapper,
            ITaskWrapper taskWrapper,
            IAttachmentWrapper attachmentWrapper,
            INewsFeedWrapper newsFeedWrapper,  



            Web.Utils.CookieManager cookieManager,

            IRolesMenuWrapper rolesMenuWrapper



           ) : base(rolesMenuWrapper, cookieManager)
        {

            _logger = logger;
            _taskLogWrapper = taskLogWrapper;
            _bugsWrapper = bugsWrapper;
            _taskWrapper = taskWrapper;
            _attachmentWrapper = attachmentWrapper;
            _newsFeedWrapper = newsFeedWrapper;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion


        }


        public ActionResult Index()
        {
            int ProjectId =  _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            int EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;

            if (ProjectId == 100)
            {
                var data = _bugsWrapper.GetListBugsForAdmin().ToList();
                return View(data);
            }
            else
            {
                var data = _bugsWrapper.GetListBugsByAssignedEmployeeAndProjects(EmployeeId, ProjectId).ToList();
                return View(data);
            }

        }

        public ActionResult IndexForAdminProject()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            int EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _bugsWrapper.GetListBugsByProjectId(ProjectId).ToList();
            return View(data);
        }


        public ActionResult Create()
        {
            _cookieManager.SetCookie("CreateBug",Guid.NewGuid().ToString());
            return View();
        }

        public ActionResult Edit(int id)
        {
            _cookieManager.SetCookie("EditBug",Guid.NewGuid().ToString());
            //var data = BugsLogic.GetDataById(id);
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _bugsWrapper.GetDataById(id, employeeid);
            foreach (var j in data.AttachmentList)
            {
                var getguid = _cookieManager.GetCookie("EditBug") != null ? _cookieManager.GetCookie("EditBug") : "";
                FormAttachmentFilesModel Attachfilelist = new FormAttachmentFilesModel();
                Attachfilelist.id = j.Id;
                Attachfilelist.GUID = getguid;
                Attachfilelist.Path = j.Url;
                Attachfilelist.TransactionID = id;
                Attachfilelist.filetype = j.Filetype;
                Attachfilelist.delflag = false;
                var checkdata = GlobalController.ListEditBug.Where(x => x.id == j.Id && x.GUID == getguid).Any();
                if (checkdata == false)
                {
                    GlobalController.ListEditBug.Add(Attachfilelist);
                }
            }

            return View(data);
        }
        public ActionResult Details(int id)
        {
            //var data = BugsLogic.GetDataById(id);
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _bugsWrapper.GetDataById(id, employeeid);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _bugsWrapper.GetDataById(id, employeeid);
            return View(data);
        }

        [HttpPost]
        public ActionResult Create(FormTaskModel model)
        {
            Core.Dto.PMDb.Task formdata = new Core.Dto.PMDb.Task();
            Tasklog formlog = new Tasklog();

            formdata.Taskgroupid = model.TaskData.Taskgroupid;
            formdata.Taskname = model.TaskData.Taskname;
            formdata.Projectid = model.TaskData.Projectid;
            formdata.Startdate = model.TaskData.Startdate;
            formdata.Duedate = model.TaskData.Duedate;
            formdata.Severityid = model.TaskData.Severityid;
            formdata.Assignto = model.TaskData.Severityid;
            formdata.Categoryid = model.TaskData.Categoryid;
            formdata.Createdby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Createddate = DateTime.Now;
            formdata.Statusid = 3;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Type = 2;
            formdata.Reviewby = model.TaskData.Reviewby;
            formdata.Result = model.TaskData.Result;
            _taskWrapper.CreateData(formdata);


           


            var getguid = _cookieManager.GetCookie("CreateBug") != null ? _cookieManager.GetCookie("CreateBug").ToString() : "";
            var getattachmentfiles = GlobalController.ListCreateBug.Where(x => x.GUID == getguid).ToList();

            foreach (var i in getattachmentfiles)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                //AttachmentFilemodel.DocumentId = formdata.id;
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Isbugdocument = true;
                AttachmentFilemodel.Taskid = formdata.Id; ;
                AttachmentFilemodel.Url = i.Path;
                _attachmentWrapper.CreateData(AttachmentFilemodel);
            }

            _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.CreatedNew, (int)formdata.Id, (int)formdata.Type);


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
            formdata.Categoryid = model.TaskData.Categoryid;
            formdata.Updateby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Updatedate = DateTime.Now;
            formdata.Type = model.TaskData.Type;
            formdata.Createdby = model.TaskData.Createdby;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Createddate = model.TaskData.Createddate;
            //formdata.StatusId = model.TaskData.StatusId;

            if (Command == "Mark As In Progress")
            {
                formdata.Statusid = 4;

                _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.InProgress, (int)formdata.Id, (int)formdata.Type);

            }

            else if (Command == "Mark As In Review")
            {
                formdata.Statusid = 7;
                _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.InReview, (int)formdata.Id, (int)formdata.Type);
            }

            else
            {
                formdata.Statusid = model.TaskData.Statusid;
                _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Edited, (int)formdata.Id, (int)formdata.Type);
            }


            formdata.Reviewby = model.TaskData.Reviewby;
            formdata.Result = model.TaskData.Result;
            _taskWrapper.Update(formdata);         




            var getguid = _cookieManager.GetCookie("EditBug") != null ? _cookieManager.GetCookie("EditBug") : "";
            var listdataattach = GlobalController.ListEditBug.Where(x => x.GUID == getguid && x.delflag == false).ToList();

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
                AttachmentFilemodel.Isbugdocument = true;
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


            _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Deleted, (int)form.Id, (int)olddata.Type);

            return RedirectToAction("Index");

        }
    }
}
