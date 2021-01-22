﻿using System;
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
        public ActionResult TCHRMarkSheet( )
        {
            return View();
        }

        [Route( "teacher/marks" )]
        public ActionResult TCHRMarkSheet( FilterModel Model )
        {
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