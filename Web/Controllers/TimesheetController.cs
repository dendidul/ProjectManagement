using Application.Wrapper.RolesMenu;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.PMDb;
using Application.Wrapper.NewsFeed;
using Application.Wrapper.Timesheet;
using System.Drawing;

namespace Web.Controllers
{
    public class TimesheetController : SidebarMenuController
    {

        #region

        private readonly Web.Utils.CookieManager _cookieManager;
        private readonly IRolesMenuWrapper _rolesMenuWrapper;
        private readonly GlobalController _globalController;



        #endregion


        private readonly ILogger<HomeController> _logger;
        private readonly INewsFeedWrapper _newsFeedWrapper;
        private readonly ITimesheetWrapper _timesheetWrapper;

        public TimesheetController(ILogger<HomeController> logger,


           Web.Utils.CookieManager cookieManager,

           IRolesMenuWrapper rolesMenuWrapper,
           INewsFeedWrapper newsFeedWrapper,
             ITimesheetWrapper timesheetWrapper,
             GlobalController globalController



           ) : base(rolesMenuWrapper, cookieManager, globalController)
        {

            _logger = logger;



            #region

            _cookieManager = cookieManager;
            _rolesMenuWrapper = rolesMenuWrapper;
            _globalController = globalController;

            #endregion

            _newsFeedWrapper = newsFeedWrapper;
            _timesheetWrapper = timesheetWrapper;





        }

        public ActionResult GetEvents(string start, string end)
        {
            //var fromDate = ConvertFromUnixTimestamp(start);
            //var toDate = ConvertFromUnixTimestamp(end);
            var fromDate = Convert.ToDateTime(start);
            var toDate = Convert.ToDateTime(end);
            //Get the events
            //You may get from the repository also
            var eventList = GetEvents();

            var rows = eventList.ToArray();
            return Json(rows);
        }

        public IActionResult Index()
        {
            return View();
        }


        private List<CalendarViewModel> GetEvents()
        {
            List<CalendarViewModel> eventList = new List<CalendarViewModel>();
            int employeeid = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
            var events = _newsFeedWrapper.GetNewsFeedByEmployeeIdForCalendar(employeeid);
            var ceklist = events.ToList();
            foreach (var a in events)
            {
                var Temp = new DateTime(1990, 1, 1);
                CalendarViewModel newEvent = new CalendarViewModel();
                newEvent.id = a.id;
                newEvent.title = a.title;

                newEvent.start = a.start.HasValue ? a.start.Value.ToString("MM/dd/yyyy") : Temp.ToString("MM/dd/yyyy");

                // dikarenakan untuk tampil data di viewnya loopingnya bertipe lebih kecil (<) bukan lebih kecil atau sama dengan  
                newEvent.end = a.end.HasValue ? a.end.Value.AddDays(1).ToString("MM/dd/yyyy") : Temp.ToString("MM/dd/yyyy");
                //
                newEvent.allDay = false;
                newEvent.Description = a.Description;
                newEvent.ProjectName = a.ProjectName;
                newEvent.EmployeeName = a.EmployeeName;

                eventList.Add(newEvent);

            }


            return eventList;
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        //public ActionResult DownloadTimesheet(string Projectid, string Startdate, string Enddate)
        //{
           
        //    int employeeid = Convert.ToInt32(_cookieManager.GetCookie("EmployeeId"));
        //    var startdate = Convert.ToDateTime(Startdate);
        //    var enddate = Convert.ToDateTime(Enddate);
        //    var projectid = Convert.ToInt32(Projectid != "" ? Projectid : "0");


        //    var data = _timesheetWrapper.GetTimesheet(projectid, startdate, enddate, employeeid);

        //    var row_awal = 3;

        //    using (var excelPackage = new ExcelPackage())
        //    {
        //        excelPackage.Workbook.Properties.Author = "ALBAPRO";
        //        excelPackage.Workbook.Properties.Title = "Timesheet Employee";
        //        var sheet = excelPackage.Workbook.Worksheets.Add("Export Results");
        //        sheet.Name = "Timesheet Employee";

        //        sheet.Column(1).AutoFit(2);
        //        sheet.Column(2).AutoFit(10);
        //        sheet.Column(3).AutoFit(15);
        //        sheet.Column(4).AutoFit(15);
        //        sheet.Column(5).AutoFit(10);
        //        sheet.Column(6).AutoFit(10);
        //        sheet.Column(7).AutoFit(10);

        //        var allCells = sheet.Cells[3, 1, 3, 9];
        //        var cellFont = allCells.Style.Font;
        //        allCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //        allCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlueViolet);
        //        cellFont.SetFromFont(new Font("Times New Roman", 12));
        //        cellFont.Bold = true;
            


        //        var judul = sheet.Cells[1, 1, 1, 2];
        //        judul.Merge = true;
        //        judul.Value = "Timesheet Employee";
        //        var Fontjudul = judul.Style.Font;
        //        Fontjudul.SetFromFont(new Font("Times New Roman", 14));
        //        Fontjudul.Bold = true;


        //        sheet.Cells[3, 1].Value = "No";
        //        sheet.Cells[3, 2].Value = "Date";
        //        sheet.Cells[3, 3].Value = "Employee Name";
        //        sheet.Cells[3, 4].Value = "Task Name";
              
        //        sheet.Cells[3, 5].Value = "Project";
        //        sheet.Cells[3, 6].Value = "Severity";
        //        sheet.Cells[3, 7].Value = "Task Type";
        //        sheet.Cells[3, 8].Value = "Work Hours";
        //        sheet.Cells[3, 9].Value = "Description";

        //        List<TimesheetModel> TimesheetList = new List<TimesheetModel>();


        //        var rowIndex = 4;
        //        var no = 1;

        //        var getlist = data.OrderBy(x => x.StartDate).ToList();

        //        foreach (var row in getlist.OrderBy(x => x.StartDate))
        //        {
        //            sheet.Cells[rowIndex, 1].Value = no;
        //            sheet.Cells[rowIndex, 2].Value = Convert.ToDateTime(row.StartDate).ToString("dd-MMM-yyyy");
        //            sheet.Cells[rowIndex, 3].Value = row.EmployeeName;
        //            sheet.Cells[rowIndex, 4].Value = row.TaskName;
                  
        //            sheet.Cells[rowIndex, 5].Value = row.ProjectsName;
        //            sheet.Cells[rowIndex, 6].Value = row.SeverityName;
        //            sheet.Cells[rowIndex, 7].Value = row.TypeName;
        //            sheet.Cells[rowIndex, 8].Value = row.Estimate;
        //            sheet.Cells[rowIndex, 9].Value = row.Descripition;
        //            rowIndex++;
        //            no++;
        //        }



        //        var endofrow = getlist != null ? getlist.Count() + 3 : 3;
        //        sheet.Tables.Add(sheet.Cells[3, 1, endofrow, 9], "wadaw");
        //        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

        //        var memoryStream = new MemoryStream();
        //        excelPackage.SaveAs(memoryStream);

        //        string fileName = "Timesheet Employee.xlsx";
        //        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //        memoryStream.Position = 0;
        //        return File(memoryStream, contentType, fileName);
        //    }
        //}
    }
}
