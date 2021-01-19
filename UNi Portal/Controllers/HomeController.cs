using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using UNi_Portal.Models;

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
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";
            ViewData["AlertData"] = ReturnValue;
            return View();
        }

        [Route( "admin/login" )]
        [HttpPost]
        public ActionResult AdminLogin( LoginModel LM )
        {

            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";
            LM.txtUserID = LM.txtUserID.ToLower();
            if ( ViewData.ModelState.IsValid )
            {
                Query = "SELECT ADMIN_ID, ADMIN_Password, ADMIN_FirstName, ADMIN_LastName FROM UPS_Admin WHERE (ADMIN_ID = '" + LM.txtUserID + "') AND (ADMIN_Password = '" + LM.txtPassword + "')";

                ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

                if ( ReturnValue == "Y" )
                {
                    return Redirect( Url.Action( "UserProfile", "User" ) );
                }
                else
                {
                    ViewData["AlertData"] = ReturnValue;
                }
            }
            else
                ViewData["AlertData"] = "Email or Password is incorrect.";
            return Redirect( "admin/login" );
        }

        [Route( "admin/dashboard" )]
        public ActionResult Dashboard()
        {
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";


            //Schools 
            Query = "SELECT * FROM UPS_Admin";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData["SchoolsCount"] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData["AlertData"] = ReturnValue;
            }


            //Programs
            Query = "SELECT * FROM UPS_Programs";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData["ProgramsCount"] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData["AlertData"] = ReturnValue;
            }


            //Teachers
            Query = "SELECT * FROM UPS_Teacher";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData["TeachersCount"] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData["AlertData"] = ReturnValue;
            }


            //Students
            Query = "SELECT * FROM UPS_Students";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData["StudentsCount"] = FormDataTable.Rows.Count;
            }
            else
            {
                ViewData["AlertData"] = ReturnValue;
            }
            return View();
        }
    }
}