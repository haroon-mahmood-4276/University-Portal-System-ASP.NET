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


        [Route( "student/create" )]
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


        [Route( "student/create" )]
        [HttpPost]
        public ActionResult Create( StudentModel Model )
        {
            try
            {
                Query = @"INSERT INTO UPS_Students (           STD_RollNo,                STD_Password,                STD_FirstName,                STD_LastName,                STD_CNIC,                STD_Address,                STD_SCLSchoolCode,                STD_PRGPCode,                STD_PRGSCode,                STD_CrntSemester,                STD_PhoneNo,                STD_Email,                STD_Gender,                STD_CCCityCode,                STD_CCCntryCode)
                        VALUES                     ('" + Model.STD_RollNo + "','" + Model.STD_Password + "','" + Model.STD_FirstName + "','" + Model.STD_LastName + "','" + Model.STD_CNIC + "','" + Model.STD_Address + "','" + Model.STD_SCLSchoolCode + "','" + Model.STD_PRGPCode + "','" + Model.STD_PRGSCode + "','" + Model.STD_CrntSemester + "','" + Model.STD_PhoneNo + "','" + Model.STD_Email + "','" + Model.STD_Gender + "','" + Model.STD_CCCityCode + "','" + Model.STD_CCCntryCode + "'";
                ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
                if ( ReturnValue == "Y" )
                {
                    ViewData["AlertType"] = "success";
                    ViewData["AlertHeading"] = "Info";
                    ViewData["AlertData"] = "Data successfully Saved.";
                }
                else
                    ViewData["AlertData"] = ReturnValue;

                return RedirectToAction( "Index" );
            }
            catch ( Exception ex )
            {
                ViewData["AlertData"] = ex.Message.ToString();
                return View();
            }
        }


        [Route( "student/{id}/edit" )]
        public ActionResult Edit( int id )
        {
            StudentModel SM = new StudentModel();
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


            Query = @"SELECT * FROM UPS_Students WHERE (STD_PKID = '" + id + "')";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                SM.STD_Address = FormDataTable.Rows[0]["STD_Address"].ToString();
                SM.STD_CCCityCode = FormDataTable.Rows[0]["STD_CCCityCode"].ToString();
                
                SM.STD_CCCntryCode = FormDataTable.Rows[0]["STD_CCCntryCode"].ToString();
                SM.STD_CNIC = FormDataTable.Rows[0]["STD_CNIC"].ToString();
                
                SM.STD_CrntSemester = FormDataTable.Rows[0]["STD_CrntSemester"].ToString();
                SM.STD_Email = FormDataTable.Rows[0]["STD_Email"].ToString();
                
                SM.STD_FirstName = FormDataTable.Rows[0]["STD_FirstName"].ToString();
                SM.STD_Gender = FormDataTable.Rows[0]["STD_Gender"].ToString();
                
                SM.STD_LastName = FormDataTable.Rows[0]["STD_LastName"].ToString();
                SM.STD_Password = FormDataTable.Rows[0]["STD_Password"].ToString();
                
                SM.STD_PhoneNo = FormDataTable.Rows[0]["STD_PhoneNo"].ToString();
                SM.STD_PRGPCode = FormDataTable.Rows[0]["STD_PRGPCode"].ToString();
                
                SM.STD_PRGSCode = FormDataTable.Rows[0]["STD_PRGSCode"].ToString();
                SM.STD_RollNo = FormDataTable.Rows[0]["STD_RollNo"].ToString();
                
                SM.STD_SCLSchoolCode = FormDataTable.Rows[0]["STD_SCLSchoolCode"].ToString();
                SM.PKID = FormDataTable.Rows[0]["STD_PKID"].ToString();
            }
            else
                ViewData["AlertData"] = ReturnValue;

            return View( SM );
        }


        [Route( "student/{id}" )]
        [HttpPatch]
        public ActionResult Edit( int id, StudentModel Model )
        {
            try
            {
                Query = @"UPDATE UPS_Students SET " +
                        "STD_RollNo = '" + Model.STD_RollNo + "', STD_Password ='" + Model.STD_Password + "', STD_FirstName ='" + Model.STD_FirstName + "'," +
                        "STD_LastName ='" + Model.STD_LastName + "', STD_CNIC ='" + Model.STD_CNIC + "', STD_Address ='" + Model.STD_Address + "', STD_SCLSchoolCode ='" + Model.STD_SCLSchoolCode + "', " +
                        "STD_PRGPCode ='" + Model.STD_PRGPCode + "', STD_PRGSCode ='" + Model.STD_PRGSCode + "', STD_CrntSemester ='" + Model.STD_CrntSemester + "', " +
                        "STD_PhoneNo ='" + Model.STD_PhoneNo + "', STD_Email ='" + Model.STD_Email + "', STD_Gender ='" + Model.STD_Gender + "', " +
                        "STD_CCCityCode ='" + Model.STD_CCCityCode + "', STD_CCCntryCode ='" + Model.STD_CCCntryCode + "'";

                ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
                if ( ReturnValue == "Y" )
                {
                    ViewData["AlertType"] = "success";
                    ViewData["AlertHeading"] = "Info";
                    ViewData["AlertData"] = "Data Successfully Updated.";
                }
                else
                    ViewData["AlertData"] = ReturnValue;

                return RedirectToAction( "Index" );
            }
            catch ( Exception ex )
            {
                ViewData["AlertData"] = ex.Message.ToString();
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
