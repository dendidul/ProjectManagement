using Application.Wrapper.Category;
using Application.Wrapper.Department;
using Application.Wrapper.Employee;
using Application.Wrapper.Global;
using Application.Wrapper.Menu;
using Application.Wrapper.NewsFeed;
using Application.Wrapper.Position;
using Application.Wrapper.Project;
using Application.Wrapper.ProjectGroup;
using Application.Wrapper.Roles;
using Application.Wrapper.RolesProjectEmployee;
using Application.Wrapper.Severity;
using Application.Wrapper.Status;
using Application.Wrapper.Task;
using Application.Wrapper.TaskDayActivity;
using Application.Wrapper.TaskGroup;
using Application.Wrapper.TaskLog;
using Application.Wrapper.Types;
using Core.Dto.PMDb;
using Infrastructure.Helper.Config;
using Infrastructure.Helper.Upload;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;

namespace Web.Controllers
{
    public class GlobalController : Controller
    {


        private readonly ITypesWrapper _typeWrapper;
        private readonly ITaskLogWrapper _taskLogWrapper;
        private readonly IProjectGroupWrapper _projectGroupWrapper;
        private readonly IRolesProjectEmployeeWrapper _rolesProjectEmployeeWrapper;
        private readonly IRolesWrapper _rolesWrapper;
        private readonly IEmployeeWrapper _employeeWrapper;
        private readonly IDepartmentWrapper _departmentWrapper;
        private readonly IPositionWrapper _positionWrapper;
        private readonly IProjectWrapper _projectWrapper;
        private readonly ITaskGroupWrapper _taskGroupWrapper;
        private readonly ISeverityWrapper _severityWrapper;
        private readonly IStatusWrapper _statusWrapper;
        private readonly ICategoryWrapper _categoryWrapper;
        private readonly IMenuWrapper _menuWrapper;
        private readonly ITaskWrapper _taskWrapper;
        private readonly INewsFeedWrapper _newsFeedWrapper;
        private readonly ITaskDayActivityWrapper _taskDayActivityWrapper;
        private readonly IGlobalWrapper _globalWrapper;
        private readonly IUploadFilesHelper _uploadFilesHelper;
        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IConfigCreatorHelper _configCreatorHelper;


        public static List<FormAttachmentFilesModel> ListDocumentCreatePublic = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListDocumentEditPublic = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListDocumentCreateDocumentProject = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListDocumentEditDocumentProject = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListCreateTask = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListEditTask = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListCreateBug = new List<FormAttachmentFilesModel>();
        public static List<FormAttachmentFilesModel> ListEditBug = new List<FormAttachmentFilesModel>();

        public static List<FormAttachmentFilesModel> ListEditActivityReview = new List<FormAttachmentFilesModel>();


        public GlobalController(
ITypesWrapper typeWrapper, ITaskLogWrapper taskLogWrapper, IProjectGroupWrapper projectGroupWrapper, IRolesProjectEmployeeWrapper rolesProjectEmployeeWrapper, IRolesWrapper rolesWrapper, IEmployeeWrapper employeeWrapper, IDepartmentWrapper departmentWrapper, IPositionWrapper positionWrapper, IProjectWrapper projectWrapper, ITaskGroupWrapper taskGroupWrapper, ISeverityWrapper severityWrapper, IStatusWrapper statusWrapper, ICategoryWrapper categoryWrapper, IMenuWrapper menuWrapper, ITaskWrapper taskWrapper, INewsFeedWrapper newsFeedWrapper, ITaskDayActivityWrapper taskDayActivityWrapper, Utils.CookieManager cookieManager, IGlobalWrapper globalWrapper
            ,IUploadFilesHelper uploadFilesHelper, IConfigCreatorHelper configCreatorHelper
            )
        {
            _typeWrapper = typeWrapper;
            _taskLogWrapper = taskLogWrapper;
            _projectGroupWrapper = projectGroupWrapper;
            _rolesProjectEmployeeWrapper = rolesProjectEmployeeWrapper;
            _rolesWrapper = rolesWrapper;
            _employeeWrapper = employeeWrapper;
            _departmentWrapper = departmentWrapper;
            _positionWrapper = positionWrapper;
            _projectWrapper = projectWrapper;
            _taskGroupWrapper = taskGroupWrapper;
            _severityWrapper = severityWrapper;
            _statusWrapper = statusWrapper;
            _categoryWrapper = categoryWrapper;
            _menuWrapper = menuWrapper;
            _taskWrapper = taskWrapper;
            _newsFeedWrapper = newsFeedWrapper;
            _taskDayActivityWrapper = taskDayActivityWrapper;
            _cookieManager = cookieManager;
            _globalWrapper = globalWrapper;
            _uploadFilesHelper = uploadFilesHelper;
            _configCreatorHelper = configCreatorHelper;


        }



        [HttpPost]
        public IActionResult ChangeProject(int id, string ProjectName)
        {
            var sessionEmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var RolesId = _rolesProjectEmployeeWrapper.GetRolesByProjectAndEmployeeID(id, sessionEmployeeId) != null ? _rolesProjectEmployeeWrapper.GetRolesByProjectAndEmployeeID(id, sessionEmployeeId).Roleid : 1007;

            var session_role_id = _cookieManager.GetCookie("Role_ID") != null ? Convert.ToInt32(_cookieManager.GetCookie("Role_ID")) : 0; ;
            if (session_role_id != 1)
            {

                _cookieManager.SetCookie("Role_ID", RolesId.Value.ToString());
            }



            _cookieManager.SetCookie("ProjectID", id.ToString());
            _cookieManager.SetCookie("ProjectName", ProjectName);
            return Json(id.ToString());
        }


        public List<Menu> GetParentMenu()
        {
            List<Menu> MenuList = new List<Menu>();
            Menu Menu = new Menu();

            Menu.Id = 0;
            Menu.Menuname = "No Parent Menu";
            MenuList.Add(Menu);

            var rows = _menuWrapper.GetParentMenu().ToList();
            foreach (var row in rows)
            {
                MenuList.Add(row);
                //Menu MenuModel = new Menu();
                //MenuModel.id = row.id;
                //MenuModel.MenuName 
            }
            return rows;
        }

        public ActionResult GetProgressProject()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;

            if (ProjectId == 0 || ProjectId == 100)
            {
                var data = 0;
                return Json(data);
            }
            else
            {
                var data = _globalWrapper.GetProjectProgress(ProjectId);
                return Json(data);
            }

        }

        [HttpPost]
        public ActionResult GetBugsProgressBar()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;

            if (ProjectId == 0 || ProjectId == 100)
            {
                var data = new ProgressBarModel()
                {
                    Completed = 0,
                    NotCompleted = 0
                };
                return Json(data);
            }
            else
            {
                var data = _globalWrapper.ProgressBugsByProject(ProjectId);
                return Json(data);
            }
        }


        [HttpPost]
        public IActionResult GetTaskProgressBar()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID")) : 0;

            if (ProjectId == 0 || ProjectId == 100)
            {
                var data = new ProgressBarModel()
                {
                    Completed = 0,
                    NotCompleted = 0
                };
                return Json(data);
            }
            else
            {
                var data = _globalWrapper.ProgressTaskByProject(ProjectId);
                return Json(data);
            }
        }

        [HttpPost]
        public List<Projectgroup> GetAllProjectGroup()
        {
            var data = _projectGroupWrapper.GetAllData().ToList();
            return data;
        }

        public List<Role> GetAllRoles()
        {
            var data = _rolesWrapper.GetAllData().ToList();
            return data;
        }

        public List<ViewAllEmployee> GetAllEmployee()
        {
            var data = _employeeWrapper.GetAllData().ToList();
            return data;
        }

        public List<Department> GetAllDepartment()
        {
            var data = _departmentWrapper.GetAllData().ToList();
            return data;
        }

        public List<Position> GetAllPosition()
        {
            var data = _positionWrapper.GetAllData().ToList();
            return data;
        }

        public List<ViewAllProject> GetAllProject()
        {
            var data = _projectWrapper.GetAllData().ToList();
            return data;
        }

        public List<Taskgroup> GetActiveTaskGroup()
        {
            int ProjectId = _cookieManager.GetCookie("ProjectID") != null ? Convert.ToInt32(_cookieManager.GetCookie("ProjectID").ToString()) : 0;
            var data = _taskGroupWrapper.GetActiveTaskGroup(ProjectId).ToList();
            return data;
        }

        public List<Taskgroup> GetAllTaskGroup()
        {
            var data = _taskGroupWrapper.GetAllData().ToList();
            return data;
        }

        public List<Status> GetStatus()
        {

            var data = _statusWrapper.GetStatus().ToList();
            return data;
        }

        public List<Status> GetAllStatus()
        {

            var data = _statusWrapper.GetAllData().ToList();
            return data;
        }

        public List<Severity> GetAllSeverity()
        {
            var data = _severityWrapper.GetAllData().ToList();
            return data;
        }

        public List<Core.Dto.PMDb.Type> GetAllType()
        {
            var data = _typeWrapper.GetAllData().ToList();
            return data;
        }

        public List<Category> GetAllCategory()
        {
            var data = _categoryWrapper.GetAllData().ToList();
            return data;
        }

        public List<ViewRolesProjectEmployee> GetAllEmployeeByProject(int id)
        {

            var data = _rolesProjectEmployeeWrapper.GetAllData(id).ToList();
            return data;
        }

        public List<ViewProjectGroupEmployeeModels> GetAllProjectEmployeeID()
        {
            var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId").ToString()) : 0;
            var data = _projectWrapper.GetAllProjectsByEmployeeId(EmployeeId).ToList();
            return data;
        }


        [HttpPost]
        public ActionResult SubmitActivityBugsLog(FormTaskModel data)
        {
            try
            {
                var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
                var getlistdata = _taskDayActivityWrapper.GetDataByTaskId(data.TaskData.Id, EmployeeId).ToList();

                //var data = BugsLogic.GetDataById(id, employeeid);
                foreach (var j in getlistdata)
                {
                    _taskDayActivityWrapper.Delete(j);
                }

                foreach (var i in data.TaskDayActivity)
                {
                    Taskdayactivity model = new Taskdayactivity();
                    model.Startdate = i.Startdate;
                    model.Type = 2;
                    model.Employeeid = EmployeeId;
                    model.Taskid = data.TaskData.Id;
                    model.Estimate = i.Estimate;
                    model.Description = i.Description;
                    model.DelFlag = false;
                    _taskDayActivityWrapper.CreateData(model);
                }
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json("false");
                throw;
            }

        }

        [HttpPost]
        public ActionResult SubmitReviewLog(FormTaskModel data)
        {
            try
            {
                var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
                var getlistdata = _taskDayActivityWrapper.GetDataByTaskId(data.TaskData.Id, EmployeeId).ToList();

                foreach (var j in getlistdata)
                {
                    _taskDayActivityWrapper.Delete(j);
                }

                foreach (var i in data.TaskDayActivity)
                {
                    Taskdayactivity model = new Taskdayactivity();
                    model.Startdate = i.Startdate;
                    model.Type = 3;
                    model.Taskid = data.TaskData.Id;
                    model.Employeeid = EmployeeId;
                    model.Estimate = i.Estimate;
                    model.Description = i.Description;
                    model.DelFlag = false;
                    _taskDayActivityWrapper.CreateData(model);
                }
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json("false");
                throw;
            }

        }


        [HttpPost]
        public ActionResult SubmitActivityLog(FormTaskModel data)
        {
            try
            {
                var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
                var getlistdata = _taskDayActivityWrapper.GetDataByTaskId(data.TaskData.Id, EmployeeId).ToList();

                foreach (var j in getlistdata)
                {
                    _taskDayActivityWrapper.Delete(j);
                }

                foreach (var i in data.TaskDayActivity)
                {
                    Taskdayactivity model = new Taskdayactivity();
                    model.Startdate = i.Startdate;
                    model.Type = 1;
                    model.Employeeid = EmployeeId;
                    model.Taskid = data.TaskData.Id;
                    model.Estimate = i.Estimate;
                    model.Description = i.Description;
                    model.DelFlag = false;
                    _taskDayActivityWrapper.CreateData(model);
                }
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json("false");
                throw;
            }

        }

        public ActionResult GetProjectByEmployeeId()
        {
            var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _projectWrapper.GetAllProjectByEmployeeId(EmployeeId) != null ? _projectWrapper.GetAllProjectByEmployeeId(EmployeeId).ViewProjectGroupEmployeeModelList : null;

            if (data != null)
            {
                List<ViewProjectGroupEmployeeModels> List = new List<ViewProjectGroupEmployeeModels>();

                foreach (var row in data)
                {
                    ViewProjectGroupEmployeeModels form = new ViewProjectGroupEmployeeModels();
                    form.ProjectId = row.ProjectId;
                    form.ProjectName = row.ProjectName;
                    List.Add(form);
                }

                return Json(List);

            }
            else
            {
                return Json(data);
            }



        }


        public ActionResult GetProjectEmployeeID()
        {
            var EmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
            var data = _projectWrapper.GetAllProjectByEmployeeId(EmployeeId);
            return Json(data);
        }


        public static string RemoveSpecialCharacters(string str)
        {
            string output = Regex.Replace(str, @"[^\w\s]", "", RegexOptions.Compiled);
            output = output.Replace("  ", " ");

            return output;
        }

        private static string ProcessContentNameUrl(string p)
        {

            string strFormName = RemoveSpecialCharacters(p);
            strFormName = strFormName.Replace(' ', '-');

            return strFormName.ToLower();
        }


        [HttpPost]
        public ActionResult UploadFileCreatePublicDocument(IFormFile Upload_File)
        {

            //var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

                //var pathfolder = Path.Combine(Server.MapPath("~/Content/UploadFile/"));
                var pathfolder = Directory.GetCurrentDirectory() + "/Content/UploadFile/";

                //upload receipt to google storage
                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if(resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {
                    var getguid = _cookieManager.GetCookie("CreatePublicDocument") != null ? _cookieManager.GetCookie("CreatePublicDocument") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = resultImagesUrl;
                    formdata.filetype = Path.GetExtension(resultImagesUrl);
                    ListDocumentCreatePublic.Add(formdata);
                }


               

                
            }
            return Json(path);

        }



        [HttpPost]
        public ActionResult UploadFileEditPublicDocument(IFormFile Upload_File, int project)
        {

           // var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                ////fileName = "";
                //pathOriginal = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                //path = "/Content/UploadFile/" + fileName;
                //file.SaveAs(pathOriginal);




                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if (resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {
                    var getfiletype = Path.GetExtension(pathOriginal);

                    var getguid = _cookieManager.GetCookie("EditPublicDocument") != null ? _cookieManager.GetCookie("EditPublicDocument") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = path;
                    formdata.filetype = getfiletype;
                    ListDocumentEditPublic.Add(formdata);
                }
            }



            return Json(path);

        }


        //[HttpPost]
        //public ActionResult UploadBacklogFromExcel(HttpPostedFileBase Upload_File, string project)
        //{
        //    if (Request.Files["Upload_File"].ContentLength > 0)
        //    {
        //        DataSet ds = new DataSet();
        //        string fileExtension = System.IO.Path.GetExtension(Request.Files["Upload_File"].FileName);

        //        if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //        {
        //            var sessionEmployeeId = _cookieManager.GetCookie("EmployeeId") != null ? Convert.ToInt32(_cookieManager.GetCookie("EmployeeId")) : 0;
        //            var sessionEmployeeName = _cookieManager.GetCookie("Employee") != null ? _cookieManager.GetCookie("Employee") : "";
        //            var fileName = Path.GetFileName(Upload_File.FileName.Replace(fileExtension, "")) + "_" + sessionEmployeeName + "_" + DateTime.Now.ToString("dd-MMM-yyyy_hhmmss") + fileExtension;
        //            // TODO: need to define destination
        //            var filepath = Path.Combine(Server.MapPath("~/Content/UploadFile"), fileName);
        //            if (System.IO.File.Exists(filepath))
        //            {
        //                System.IO.File.Delete(filepath);
        //                Upload_File.SaveAs(filepath);
        //            }
        //            else
        //            {
        //                Upload_File.SaveAs(filepath);
        //            }
        //            string excelConnectionString = string.Empty;
        //            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
        //            filepath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

        //            //connection String for xls file format.
        //            if (fileExtension == ".xls")
        //            {
        //                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        //                filepath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //            }

        //            //connection String for xlsx file format.
        //            else if (fileExtension == ".xlsx")
        //            {
        //                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
        //                filepath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //            }

        //            //Create Connection to Excel work book and add oledb namespace
        //            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //            excelConnection.Open();
        //            DataTable dt = new DataTable();

        //            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            if (dt == null)
        //            {
        //                return Json("Fail getting data!!");
        //            }
        //            else
        //            {

        //                String[] excelSheets = new String[dt.Rows.Count];
        //                int t = 0;
        //                //excel data saves in temp file here.
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    excelSheets[t] = row["TABLE_NAME"].ToString();
        //                    t++;
        //                }
        //                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

        //                string query = string.Format("Select * from [{0}]", excelSheets[0]);
        //                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
        //                {
        //                    dataAdapter.Fill(ds);
        //                }


        //                //var timesheet = new List<TimesheetUploadModels>();
        //                //TimesheetsViewModel timesheets = new TimesheetsViewModel();


        //                for (int i = 4; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    if (ds.Tables[0].Rows[i][0].ToString() == null || ds.Tables[0].Rows[i][0].ToString() == "")
        //                    {
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            var Taskname = ds.Tables[0].Rows[i][0] != null ? ds.Tables[0].Rows[i][0].ToString() : "";
        //                            var Description = ds.Tables[0].Rows[i][1] != null ? ds.Tables[0].Rows[i][1].ToString() : "";

        //                            Core.Dto.PMDb.Task form = new Core.Dto.PMDb.Task();
        //                            //Tasklog log = new Tasklog();


        //                            form.Taskname = Taskname;
        //                            form.Descripition = Description;
        //                            form.Projectid = Convert.ToInt32(project);
        //                            form.Createddate = DateTime.Now;
        //                            form.Createdby = sessionEmployeeId;
        //                            form.Updatedate = DateTime.Now;
        //                            form.Updateby = sessionEmployeeId;
        //                            form.Type = 3;
        //                            _taskWrapper.CreateData(form);

        //                            //log.TaskName = Taskname;
        //                            //log.Descripition = Description;
        //                            //log.ProjectId = Convert.ToInt32(project);
        //                            //log.CreatedDate = DateTime.Now;
        //                            //log.CreatedBy = sessionEmployeeId;
        //                            //log.Type = 3;
        //                            //TaskLogLogic.CreateData(log);




        //                            _newsFeedWrapper.AddNewsFeed(sessionEmployeeId, EnumAction.CreatedNew, (int)form.id, (int)form.Type);


        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            //timesheet.Add(new TimesheetUploadModels
        //                            //{
        //                            //    No = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString()),
        //                            //    NIK = ds.Tables[0].Rows[i][1].ToString(),
        //                            //    Name = ds.Tables[0].Rows[i][2].ToString(),
        //                            //    StatusUpload = "Error : There is problem with data in this row"
        //                            //});
        //                        }
        //                    }


        //                }

        //                //timesheets.TimesheetUploadModels = timesheet;
        //                //excelConnection.Close();
        //                return Json("Success");
        //            }
        //        }
        //        else
        //        {
        //            return Json("File is not excel");
        //        }
        //    }
        //    else
        //    {
        //        return Json("File is not excel");
        //    }
        //}



        [HttpPost]
        public ActionResult UploadFileEditDocumentProject(IFormFile Upload_File)
        {

          //  var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {
                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                //fileName = "";
                //pathOriginal = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                //path = "/Content/UploadFile/" + fileName;
                //file.SaveAs(pathOriginal);

                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if (resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {
                    var getfiletype = Path.GetExtension(pathOriginal);

                    var getguid = _cookieManager.GetCookie("EditDocumentProject") != null ? _cookieManager.GetCookie("EditDocumentProject") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = path;
                    formdata.filetype = getfiletype;

                    ListDocumentEditDocumentProject.Add(formdata);
                }

                

               
            }
            return Json(path);

        }


        public ActionResult UploadFileCreateDocumentProject(IFormFile Upload_File)
        {

           // var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

                //var pathfolder = Path.Combine(Server.MapPath("~/Content/UploadFile/"));

                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                //fileName = "";
                //pathOriginal = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                //path = "/Content/UploadFile/" + fileName;
                //file.SaveAs(pathOriginal);


                var getfiletype = Path.GetExtension(pathOriginal);

                var getguid = _cookieManager.GetCookie("CreateDocumentProject") != null ? _cookieManager.GetCookie("CreateDocumentProject") : "";
                FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                formdata.GUID = getguid;
                formdata.Path = path;
                formdata.filetype = getfiletype;
                ListDocumentCreateDocumentProject.Add(formdata);

            }
            return Json(path);

        }




        public ActionResult UploadFileCreateTask(IFormFile Upload_File)
        {

           // var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                //fileName = "";
                //pathOriginal = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                //path = "/Content/UploadFile/" + fileName;
                //file.SaveAs(pathOriginal);
                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if (resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {
                    var getfiletype = Path.GetExtension(pathOriginal);

                    var getguid = _cookieManager.GetCookie("CreateTask") != null ? _cookieManager.GetCookie("CreateTask") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = path;
                    formdata.filetype = getfiletype;
                    ListCreateTask.Add(formdata);
                }
               

            }
            return Json(path);

        }


        public ActionResult UploadFileEditTask(IFormFile Upload_File)
        {

           // var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

               
                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                //fileName = "";
                //pathOriginal = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                //path = "/Content/UploadFile/" + fileName;
                //file.SaveAs(pathOriginal);
                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if (resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {
                    var getfiletype = Path.GetExtension(pathOriginal);

                    var getguid = _cookieManager.GetCookie("EditTask") != null ? _cookieManager.GetCookie("EditTask") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = path;
                    formdata.filetype = getfiletype;
                    ListEditTask.Add(formdata);

                }
                
            }
            return Json(path);

        }


        public ActionResult UploadFileCreateBug(IFormFile Upload_File)
        {

           // var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                //fileName = "";
                //pathOriginal = Path.Combine(Server.MapPath("~/Content/UploadFile/"), fileName);
                //path = "/Content/UploadFile/" + fileName;
                //file.SaveAs(pathOriginal);
                //upload receipt to google storage
                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if (resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {

                    var getfiletype = Path.GetExtension(pathOriginal);

                    var getguid = _cookieManager.GetCookie("CreateBug") != null ? _cookieManager.GetCookie("CreateBug") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = path;
                    formdata.filetype = getfiletype;
                    ListCreateBug.Add(formdata);
                }


            }
            return Json(path);

        }


        public ActionResult UploadFileEditBug(IFormFile Upload_File)
        {

           // var file = Request.Files[0];
            //var lala = Request.Files[1];
            var path = "";
            string pathOriginal = "";
            string fileName = "";

            if (Upload_File != null)
            {

                fileName = Guid.NewGuid().ToString();
                var splitname = Upload_File.FileName.Split('.');
                splitname[0] = fileName;

                fileName = splitname[0] + "." + splitname[1];
                //fileName = "";
                var resultImagesUrl = _uploadFilesHelper.Upload(Upload_File, string.Format("{0}{1}", _configCreatorHelper.Get("GoogleStorage:GoogleCloudStorageUrl"), "")).Result;

                if (resultImagesUrl == "400")
                {
                    return Json("Gagal Upload");
                }
                else
                {
                    var getfiletype = Path.GetExtension(pathOriginal);

                    var getguid = _cookieManager.GetCookie("EditBug") != null ? _cookieManager.GetCookie("EditBug") : "";
                    FormAttachmentFilesModel formdata = new FormAttachmentFilesModel();

                    formdata.GUID = getguid;
                    formdata.Path = path;
                    formdata.filetype = getfiletype;
                    ListEditBug.Add(formdata);
                }
            }
            return Json(path);

        }

        [HttpPost]
        public ActionResult DeleteAttachmentDataEditPublicDocument(string id)
        {
            var getid = Convert.ToInt32(id);
            var getsession = _cookieManager.GetCookie("EditPublicDocument") != null ? _cookieManager.GetCookie("EditPublicDocument") : "";
            var data = ListDocumentEditPublic.Where(x => x.id == getid && x.GUID == getsession).FirstOrDefault();
            ListDocumentEditPublic.Remove(data);

            if(data!= null)
            {
                data.delflag = true;
                ListDocumentEditPublic.Add(data);
            }
           
            return Json(new { status = "success", text = "Delete Successfuly" });

        }

        [HttpPost]
        public ActionResult DeleteAttachmentDataEditDocumentProject(string id)
        {
            var getid = Convert.ToInt32(id);
            var getsession = _cookieManager.GetCookie("EditDocumentProject") != null ? _cookieManager.GetCookie("EditDocumentProject") : "";
            var data = ListDocumentEditDocumentProject.Where(x => x.id == getid && x.GUID == getsession).FirstOrDefault();
            ListDocumentEditDocumentProject.Remove(data);
            data.delflag = true;
            ListDocumentEditDocumentProject.Add(data);
            return Json(new { status = "success", text = "Delete Successfuly" });

        }


        [HttpPost]
        public ActionResult DeleteAttachmentDataEditTask(string id)
        {
            var getid = Convert.ToInt32(id);
            var getsession = _cookieManager.GetCookie("EditTask") != null ? _cookieManager.GetCookie("EditTask") : "";
            var data = ListEditTask.Where(x => x.id == getid && x.GUID == getsession).FirstOrDefault();
            ListEditTask.Remove(data);
            data.delflag = true;
            ListEditTask.Add(data);
            return Json(new { status = "success", text = "Delete Successfuly" });

        }

        [HttpPost]
        public ActionResult DeleteAttachmentDataEditBug(string id)
        {
            var getid = Convert.ToInt32(id);
            var getsession = _cookieManager.GetCookie("EditBug") != null ? _cookieManager.GetCookie("EditBug") : "";
            var data = ListEditBug.Where(x => x.id == getid && x.GUID == getsession).FirstOrDefault();
            ListEditBug.Remove(data);
            data.delflag = true;
            ListEditBug.Add(data);
            return Json(new { status = "success", text = "Delete Successfuly" });

        }




    }
}
