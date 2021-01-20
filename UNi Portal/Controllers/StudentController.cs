using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNi_Portal.Models;

namespace UNi_Portal.Controllers
{
    public class StudentController : Controller
    {
        string ReturnValue = "";
        string Query = "";
        DataTable FormDataTable = new DataTable();

        [Route( "student" )]
        public ActionResult Index()
        {
            ViewData["AlertData"] = "";
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";
            Query = @"SELECT STD_RollNo, STD_FirstName, STD_LastName, 
                    (SELECT DISTINCT PRG_ProgramName FROM UPS_Programs WHERE (PRG_PCode = STD_PRGPCode)) AS PRG_ProgramName, 
                    (SELECT DISTINCT PRG_SectionName FROM UPS_Programs WHERE (PRG_PCode = STD_PRGPCode) AND (PRG_SCode = STD_PRGSCode)) AS PRG_SectionName, STD_CrntSemester,
                    (SELECT DISTINCT SCL_SchoolName FROM UPS_Schools WHERE (SCL_SchoolCode = STD_SCLSchoolCode)) AS SCL_SchoolName, 
                    (SELECT DISTINCT SCL_SchoolAbb FROM UPS_Schools WHERE (SCL_SchoolCode = STD_SCLSchoolCode)) AS SCL_SchoolAbb, STD_PKID 
                    FROM UPS_Students ORDER BY STD_RollNo";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData["Students"] = FormDataTable;
            }
            else
            {
                ViewData["AlertData"] = ReturnValue;
            }
            return View();
        }

        [Route( "student/{id}" )]
        public ActionResult Details( int id )
        {
            return View();
        }

        [Route( "students/create" )]
        public ActionResult Create()
        {
            ViewData["AlertData"] = "";
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";

            Query = @"SELECT CC_CntryCode, CC_CntryName FROM UPS_CityCountry ORDER BY CC_CntryName";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                List<SelectListItem> cbCountries = new List<SelectListItem>();
                foreach ( DataRow dtRow in FormDataTable.Rows )
                {
                    cbCountries.Add( new SelectListItem { Value = dtRow["CC_CntryCode"].ToString(), Text = dtRow["CC_CntryName"].ToString() } );
                }
                ViewData["cbCountries"] = cbCountries;
            }
            else
                ViewData["AlertData"] = ReturnValue;


            Query = @"SELECT SCL_SchoolCode, SCL_SchoolName FROM UPS_Schools ORDER BY SCL_SchoolName";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                List<SelectListItem> cbSchools = new List<SelectListItem>();
                foreach ( DataRow dtRow in FormDataTable.Rows )
                {
                    cbSchools.Add( new SelectListItem { Value = dtRow["SCL_SchoolCode"].ToString(), Text = dtRow["SCL_SchoolName"].ToString() } );
                }
                ViewData["cbSchools"] = cbSchools;
            }
            else
                ViewData["AlertData"] = ReturnValue;

            return View();
        }

        //
        // POST: /Students/Create
        [HttpPost]
        public ActionResult Create( StudentModel Model )
        {
            try
            {
                return RedirectToAction( "Index" );
            }
            catch
            {
                return View();
            }
        }

        [Route( "student/{id}/edit" )]
        public ActionResult Edit( int id )
        {
            ViewData["AlertData"] = "";
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";
            
            Query = @"SELECT * FROM UPS_Students WHERE (STD_PKID = '" + id + "')";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                ViewData["StudentData"] = FormDataTable;
            }
            else
                ViewData["AlertData"] = ReturnValue;
            
            return View();
        }

        //
        // POST: /Students/Edit/5
        [HttpPatch]
        public ActionResult Edit( int id, FormCollection collection )
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction( "Index" );
            }
            catch
            {
                return View();
            }
        }

        [Route( "student/{id}" )]
        [HttpDelete]
        public ActionResult Delete( FormCollection collection )
        {
            Query = @"DELETE FROM UPS_Students WHERE (STD_PKID = '" + collection["PKID"] + "')";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                ViewData["AlertType"] = "success";
                ViewData["AlertHeading"] = "Info";
                ViewData["AlertData"] = ReturnValue;
                ViewData["AlertData"] = "Data successfully Deleted.";
            }
            else
            {
                ViewData["AlertType"] = "danger";
                ViewData["AlertHeading"] = "Error";
                ViewData["AlertData"] = ReturnValue;
            }

            return RedirectToAction( "Index" );
        }
    }
}
