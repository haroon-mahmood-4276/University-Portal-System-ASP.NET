using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using UNi_Portal.Models;
using Newtonsoft.Json;

namespace UNi_Portal.Controllers
{
    public class HomeController : Controller
    {
        string ReturnValue = "";
        string Query = "";
        DataTable FormDataTable = new DataTable();

        [Route( "" )]
        public ActionResult Index()
        {
            return View();
        }

        [Route( "admin/login" )]
        public ActionResult AdminLogin()
        {
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            ViewData[ "AlertData" ] = ReturnValue;
            return View();
        }

        [Route( "admin/login" )]
        [HttpPost]
        public ActionResult AdminLogin( LoginModel LM )
        {
            if ( HttpContext.Request.Cookies[ "Login" ] != null )
            {
                HttpContext.Response.Cookies.Remove( "Login" );
            }
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            LM.txtUserID = LM.txtUserID.ToLower();
            if ( ViewData.ModelState.IsValid )
            {
                Query = "SELECT ADMIN_ID, ADMIN_Password, ADMIN_FirstName, ADMIN_LastName FROM UPS_Admin WHERE (ADMIN_ID = '" + LM.txtUserID + "') AND (ADMIN_Password = '" + LM.txtPassword + "')";

                ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

                if ( ReturnValue == "Y" )
                {
                    var Login = new Dictionary<string, string>()
                    {
                        {"ID", FormDataTable.Rows[0]["ADMIN_ID"].ToString().ToUpper()},
                        {"FirstName", FormDataTable.Rows[0]["ADMIN_FirstName"].ToString()}, 
                        {"LastName", FormDataTable.Rows[0]["ADMIN_LastName"].ToString()},
                        {"Role", "Admin"}
                    };

                    var json = JsonConvert.SerializeObject( Login );

                    HttpCookie LoginCookie = new HttpCookie( "Login", json );
                    LoginCookie.Expires = DateTime.Now.AddDays( 1 );
                    HttpContext.Response.Cookies.Add( LoginCookie );

                    return Redirect( Url.Action( "Dashboard", "Home" ) );
                }
                else
                {
                    ViewData[ "AlertData" ] = ReturnValue;
                }
            }
            else
                ViewData[ "AlertData" ] = "Email or Password is incorrect.";
            return View();
        }

        [Route( "admin/dashboard" )]
        public ActionResult Dashboard()
        {
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";


            //Schools 
            Query = "SELECT * FROM UPS_Admin";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "SchoolsCount" ] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }


            //Programs
            Query = "SELECT * FROM UPS_Programs";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "ProgramsCount" ] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }


            //Teachers
            Query = "SELECT * FROM UPS_Teachers";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "TeachersCount" ] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }


            //Students
            Query = "SELECT * FROM UPS_Students";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "StudentsCount" ] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }
            return View();
        }



        [Route( "teacher/login" )]
        public ActionResult TeacherLogin()
        {
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            ViewData[ "AlertData" ] = ReturnValue;
            return View();
        }



        [Route( "teacher/login" )]
        [HttpPost]
        public ActionResult TeacherLogin( LoginModel LM )
        {
            if ( HttpContext.Request.Cookies[ "Login" ] != null )
            {
                HttpContext.Response.Cookies.Remove( "Login" );
            }
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            LM.txtUserID = LM.txtUserID.ToLower();
            if ( ViewData.ModelState.IsValid )
            {
                Query = "SELECT TCHR_ID, TCHR_Password, TCHR_FirstName, TCHR_LastName FROM UPS_Teachers WHERE (TCHR_ID = '" + LM.txtUserID + "') AND (TCHR_Password = '" + LM.txtPassword + "')";

                ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

                if ( ReturnValue == "Y" )
                {
                    var Login = new Dictionary<string, string>()
                    {
                        {"ID", FormDataTable.Rows[0]["TCHR_ID"].ToString().ToUpper()},
                        {"FirstName", FormDataTable.Rows[0]["TCHR_FirstName"].ToString()}, 
                        {"LastName", FormDataTable.Rows[0]["TCHR_LastName"].ToString()},
                        {"Role", "Teacher"}
                    };

                    var json = JsonConvert.SerializeObject( Login );

                    HttpCookie LoginCookie = new HttpCookie( "Login", json );
                    LoginCookie.Expires = DateTime.Now.AddDays( 1 );
                    HttpContext.Response.Cookies.Add( LoginCookie );

                    return Redirect( Url.Action( "TCHRMarkSheet", "Home" ) );
                }
                else
                {
                    ViewData[ "AlertData" ] = ReturnValue;
                }
            }
            else
                ViewData[ "AlertData" ] = "Email or Password is incorrect.";
            return View();
        }

        [Route( "teacher/marks" )]
        public ActionResult TCHRMarkSheet()
        {
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            Query = @"SELECT SCL_SchoolCode, SCL_SchoolName FROM UPS_Schools ORDER BY SCL_SchoolName";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                List<SelectListItem> cbSchools = new List<SelectListItem>();
                foreach ( DataRow dtRow in FormDataTable.Rows )
                {
                    cbSchools.Add( new SelectListItem { Value = dtRow[ "SCL_SchoolCode" ].ToString(), Text = dtRow[ "SCL_SchoolName" ].ToString() } );
                }
                ViewData[ "cbSchools" ] = cbSchools;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }
            return View();
        }

        [HttpPost]
        [Route( "teacher/marks" )]
        public ActionResult TCHRMarkSheet( FormCollection Collection, FilterModel Model )
        {
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";

            Query = @"SELECT STD_RollNo FROM UPS_Students WHERE (STD_PRGPCode = '" + Model.PRG_PCode + "') AND (STD_PRGSCode = '" + Model.PRG_SCode + "') AND (STD_SCLSchoolCode = '" + Model.PRG_SCLSchoolCode + "') ORDER BY STD_RollNo";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                for ( int i = 0; i < FormDataTable.Rows.Count; i++ )
                {
                    Query = @"INSERT INTO UPS_STDMarks  (                           SM_STDRollNo,                       SM_ExamName,             SM_SubjectName,            STD_SCLSchoolCode,             SM_PRGPCode,              SM_PRGSCode,                    SM_Date,                       SM_TotalMarks,                             SM_ObtainedMarks)
                        VALUES                  ('" + FormDataTable.Rows[ i ][ "STD_RollNo" ].ToString() + "','" + Model.ExamType + "','" + Model.Subject + "','" + Model.PRG_SCLSchoolCode + "','" + Model.PRG_PCode + "','" + Model.PRG_SCode + "','" + Model.ClassDate.ToString() + "','" + Model.TotalMarks + "','" + Collection[ FormDataTable.Rows[ i ][ "STD_RollNo" ].ToString() + "OM" ] + "')";
                    ReturnValue = DBQueries.DB_ExecuteNonQuery( Query );
                }
                if ( ReturnValue == "Y" )
                {
                    ViewData[ "AlertType" ] = "Success";
                    ViewData[ "AlertHeading" ] = "Info";
                    ViewData[ "AlertData" ] = "Marks Sheet Saved.";
                }
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }
            return RedirectToAction( "TCHRMarkSheet" );
        }


        [Route( "teacher/attendance" )]
        public ActionResult TCHRAttendance()
        {
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            Query = @"SELECT SCL_SchoolCode, SCL_SchoolName FROM UPS_Schools ORDER BY SCL_SchoolName";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                List<SelectListItem> cbSchools = new List<SelectListItem>();
                foreach ( DataRow dtRow in FormDataTable.Rows )
                {
                    cbSchools.Add( new SelectListItem { Value = dtRow[ "SCL_SchoolCode" ].ToString(), Text = dtRow[ "SCL_SchoolName" ].ToString() } );
                }
                ViewData[ "cbSchools" ] = cbSchools;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }
            return View();
        }

        [HttpPost]
        [Route( "teacher/attendance" )]
        public ActionResult TCHRAttendance( FormCollection Collection, FilterModel Model )
        {
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";

            Query = @"SELECT STD_RollNo FROM UPS_Students WHERE (STD_PRGPCode = '" + Model.PRG_PCode + "') AND (STD_PRGSCode = '" + Model.PRG_SCode + "') AND (STD_SCLSchoolCode = '" + Model.PRG_SCLSchoolCode + "') ORDER BY STD_RollNo";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                for ( int i = 0; i < FormDataTable.Rows.Count; i++ )
                {
                    Query = @"INSERT INTO UPS_STDAttendance  (                SA_STDRollNo,                           SA_SubjectName,            SA_SCLSchoolCode,             SA_PRGPCode,              SA_PRGSCode,                    SA_Date,                       SA_StartTime,                             SA_EndTime,                                               SA_Status)
                        VALUES                  ('" + FormDataTable.Rows[ i ][ "STD_RollNo" ].ToString() + "','" + Model.Subject + "','" + Model.PRG_SCLSchoolCode + "','" + Model.PRG_PCode + "','" + Model.PRG_SCode + "','" + Model.ClassDate.ToString() + "','" + Model.StartTime + "',             '" + Model.EndTime + "','" + ( ( Collection[ FormDataTable.Rows[ i ][ "STD_RollNo" ].ToString() + "Chk" ] ) == "on" ? "Y" : "N" ) + "')";
                    ReturnValue = DBQueries.DB_ExecuteNonQuery( Query );
                }
                if ( ReturnValue == "Y" )
                {
                    ViewData[ "AlertType" ] = "Success";
                    ViewData[ "AlertHeading" ] = "Info";
                    ViewData[ "AlertData" ] = "Marks Sheet Saved.";
                }
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }
            return RedirectToAction( "TCHRAttendance" );
        }



        [Route( "student/login" )]
        public ActionResult StudentLogin()
        {
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            ViewData[ "AlertData" ] = ReturnValue;
            return View();
        }


        [Route( "student/login" )]
        [HttpPost]
        public ActionResult StudentLogin( LoginModel LM )
        {
            if ( HttpContext.Request.Cookies[ "Login" ] != null )
            {
                HttpContext.Response.Cookies.Remove( "Login" );
            }
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            LM.txtUserID = LM.txtUserID.ToLower();
            if ( ViewData.ModelState.IsValid )
            {
                Query = "SELECT STD_RollNo, STD_Password, STD_FirstName, STD_LastName FROM UPS_Students WHERE (STD_RollNo = '" + LM.txtUserID + "') AND (STD_Password = '" + LM.txtPassword + "')";

                ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

                if ( ReturnValue == "Y" )
                {
                    var Login = new Dictionary<string, string>()
                    {
                        {"ID", FormDataTable.Rows[0]["STD_RollNo"].ToString().ToUpper()},
                        {"FirstName", FormDataTable.Rows[0]["STD_FirstName"].ToString()}, 
                        {"LastName", FormDataTable.Rows[0]["STD_LastName"].ToString()},
                        {"Role", "Student"}
                    };

                    var json = JsonConvert.SerializeObject( Login );

                    HttpCookie LoginCookie = new HttpCookie( "Login", json );
                    LoginCookie.Expires = DateTime.Now.AddDays( 1 );
                    HttpContext.Response.Cookies.Add( LoginCookie );

                    return Redirect( Url.Action( "TCHRMarkSheet", "Home" ) );
                }
                else
                {
                    ViewData[ "AlertData" ] = ReturnValue;
                }
            }
            else
                ViewData[ "AlertData" ] = "Email or Password is incorrect.";
            return View();
        }


        [Route( "student/dashboard/{id}" )]
        public ActionResult StudentDashboard( string id )
        {
            DataTable Attendance = new DataTable();
            DataTable Marks = new DataTable();
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";

            Query = @"SELECT STD_RollNo, STD_FirstName, STD_LastName, 
                    (SELECT DISTINCT PRG_ProgramName FROM UPS_Programs WHERE (PRG_PCode = STD_PRGPCode)) AS PRG_ProgramName, 
                    (SELECT DISTINCT PRG_SectionName FROM UPS_Programs WHERE (PRG_PCode = STD_PRGPCode) AND (PRG_SCode = STD_PRGSCode)) AS PRG_SectionName, STD_CrntSemester,
                    (SELECT DISTINCT SCL_SchoolName FROM UPS_Schools WHERE (SCL_SchoolCode = STD_SCLSchoolCode)) AS SCL_SchoolName, 
                    (SELECT DISTINCT SCL_SchoolAbb FROM UPS_Schools WHERE (SCL_SchoolCode = STD_SCLSchoolCode)) AS SCL_SchoolAbb, STD_PKID 
                    FROM UPS_Students WHERE STD_RollNo = '" + id + "' ORDER BY STD_RollNo";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "StudentData" ] = FormDataTable;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }


            Query = @"SELECT SA_SubjectName, SA_Date, SA_StartTime, SA_EndTime, SA_Status FROM UPS_STDAttendance
                    WHERE (SA_STDRollNo = '" + id + "') ORDER BY SA_Date";

            ReturnValue = DBQueries.DBFilDTable( ref Attendance, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "Attendance" ] = Attendance;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }


            Query = @"SELECT SM_ExamName, SM_SubjectName, SM_Date, SM_TotalMarks, SM_ObtainedMarks FROM UPS_STDMarks WHERE (SM_STDRollNo = '" + id + "') ORDER BY SM_Date";

            ReturnValue = DBQueries.DBFilDTable( ref Marks, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData[ "Marks" ] = Marks;
            }
            else
            {
                ViewData[ "AlertData" ] = ReturnValue;
            }
            return View();
        }

        [Route( "logout" )]
        public ActionResult Logout()
        {
            Response.Cookies[ "Login" ].Expires = DateTime.Now.AddDays( -1 );
            return Redirect( Url.Action( "Index", "Home" ) );
        }
    }
}