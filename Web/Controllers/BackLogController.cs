using Microsoft.AspNetCore.Mvc;
using Application.Wrapper.RolesMenu;
using Core.Dto.PMDb;
using Application.Wrapper.TaskLog;
using Application.Wrapper.Task;
using Application.Wrapper.Attachment;
using Application.Wrapper.NewsFeed;


namespace Web.Controllers
{
    public class BackLogController : SidebarMenuController
    {
        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly ILogger<BackLogController> _logger;
        private readonly GlobalController _globalController;


        private readonly ITaskLogWrapper _taskLogWrapper;
        private readonly ITaskWrapper _taskWrapper;
        private readonly IAttachmentWrapper _atttachmentWrapper;
        private readonly INewsFeedWrapper _newsFeedWrapper;




        public BackLogController(ILogger<BackLogController> logger,
         
            Web.Utils.CookieManager cookieManager,

            IRolesMenuWrapper rolesMenuWrapper,

            ITaskLogWrapper taskLogWrapper,
            ITaskWrapper taskWrapper,
            IAttachmentWrapper atttachmentWrapper,
            INewsFeedWrapper newsFeedWrapper
,
            GlobalController globalController


            ) : base(rolesMenuWrapper, cookieManager, globalController)
        {

            _logger = logger;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;


            #endregion

            _taskLogWrapper = taskLogWrapper;
            _taskWrapper = taskWrapper;
            _atttachmentWrapper = atttachmentWrapper;
            _newsFeedWrapper = newsFeedWrapper;
            _globalController = globalController;
        }





        public ActionResult Index()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;
            var data = _taskWrapper.GetListBackLogByProjectId(ProjectId).ToList();
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
            Tasklog formlog = new Tasklog();



            formdata.Taskgroupid = model.TaskData.Taskgroupid;
            formdata.Taskname = model.TaskData.Taskname;
            formdata.Projectid = model.TaskData.Projectid;
            formdata.Startdate = model.TaskData.Startdate;
            formdata.Duedate = model.TaskData.Duedate;
            formdata.Severityid = model.TaskData.Severityid;
            formdata.Assignto = model.TaskData.Assignto;
            formdata.Categoryid = model.TaskData.Categoryid;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Createdby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Createddate = DateTime.Now;
            formdata.Statusid = 3;
            formdata.Type = model.TaskData.Type;
            formdata.Reviewby = model.TaskData.Reviewby;
            formdata.Result = model.TaskData.Result;
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
                _atttachmentWrapper.CreateData(AttachmentFilemodel);
            }

            _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.CreatedNew, (int)formdata.Id, (int)formdata.Type);




            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(FormTaskModel model)
        {
            Core.Dto.PMDb.Task formdata = new Core.Dto.PMDb.Task();

           

            formdata.Id = model.TaskData.Id;
            formdata.Taskgroupid = model.TaskData.Taskgroupid;
            formdata.Taskname = model.TaskData.Taskname;
            formdata.Projectid = model.TaskData.Projectid;
            formdata.Startdate = model.TaskData.Startdate;
            formdata.Duedate = model.TaskData.Duedate;
            formdata.Severityid = model.TaskData.Severityid;
            formdata.Descripition = model.TaskData.Descripition;
            formdata.Assignto = model.TaskData.Assignto;
            formdata.Updateby = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            formdata.Updatedate = DateTime.Now;
            formdata.Createdby = model.TaskData.Createdby;
            formdata.Createddate = model.TaskData.Createddate;
            formdata.Type = model.TaskData.Type;
            formdata.Categoryid = model.TaskData.Categoryid;
            formdata.Statusid = model.TaskData.Statusid;
            formdata.Reviewby = model.TaskData.Reviewby;
            formdata.Result = model.TaskData.Result;
            _taskWrapper.Update(formdata);





            var getguid = _cookieManager.GetCookie("EditTask") != null ? _cookieManager.GetCookie("EditTask") : "";
            var listdataattach = GlobalController.ListEditTask.Where(x => x.GUID == getguid && x.delflag == false).ToList();

            var getolddataAttachment = _atttachmentWrapper.GetDataByTaskId(formdata.Id).ToList();

            foreach (var k in getolddataAttachment)
            {
                _atttachmentWrapper.Delete(k);
            }

            //var data = AttachmentLogic.

            foreach (var i in listdataattach)
            {
                Attachmentfile AttachmentFilemodel = new Attachmentfile();
                AttachmentFilemodel.Filetype = i.filetype;
                AttachmentFilemodel.Istaskdocument = true;
                AttachmentFilemodel.Taskid = formdata.Id; ;
                AttachmentFilemodel.Url = i.Path;
                _atttachmentWrapper.CreateData(AttachmentFilemodel);
            }

            _newsFeedWrapper.AddNewsFeed(Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")), EnumAction.Edited, (int)formdata.Id, (int)formdata.Type);


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
            var data = _taskWrapper.GetDataByIdForActivityMonitoring(id);
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var employeeid = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId").ToString()) : 0;
            var data = _taskWrapper.GetDataById(id, employeeid);
            return View(data);
        }
    }
}
