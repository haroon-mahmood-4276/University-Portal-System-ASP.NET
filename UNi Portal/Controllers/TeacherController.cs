using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNi_Portal.Models;

namespace UNi_Portal.Controllers
{
    public class TeacherController : Controller
    {
        string ReturnValue = "";
        string Query = "";
        DataTable FormDataTable = new DataTable();

        [Route( "teacher" )]
        public ActionResult Index()
        {
            ViewData["AlertData"] = "";
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";
            Query = @"SELECT TCHR_ID, TCHR_FirstName, TCHR_LastName, 
                    (SELECT DISTINCT PRG_ProgramName FROM UPS_Programs WHERE (PRG_PCode = TCHR_PRGPCode)) AS PRG_ProgramName, 
                    (SELECT DISTINCT PRG_SectionName FROM UPS_Programs WHERE (PRG_PCode = TCHR_PRGPCode) AND (PRG_SCode = TCHR_PRGSCode)) AS PRG_SectionName, TCHR_Post,
                    (SELECT DISTINCT SCL_SchoolName FROM UPS_Schools WHERE (SCL_SchoolCode = TCHR_SCLSchoolCode)) AS TCHR_SchoolName, 
                    (SELECT DISTINCT SCL_SchoolAbb FROM UPS_Schools WHERE (SCL_SchoolCode = TCHR_SCLSchoolCode)) AS TCHR_SchoolAbb, TCHR_PKID 
                    FROM UPS_Teachers ORDER BY TCHR_ID";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ViewData["Teachers"] = FormDataTable;
            }
            else
            {
                ViewData["AlertData"] = ReturnValue;
            }
            return View();
        }


        [Route( "teacher/{id}" )]
        public ActionResult Details( int id )
        {
            ViewData["AlertData"] = "";
            ViewData["AlertType"] = "danger";
            ViewData["AlertHeading"] = "Error";

            Query = @"SELECT * FROM UPS_Teachers WHERE (TCHR_PKID = '" + id + "')";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                ViewData["TeacherData"] = FormDataTable;
            }
            else
                ViewData["AlertData"] = ReturnValue;

            return View();
        }


        [Route( "teacher/create" )]
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


        [Route( "teacher/create" )]
        [HttpPost]
        public ActionResult Create( TeacherModel Model )
        {
            try
            {
                Query = @"INSERT INTO UPS_Teachers (           TCHR_ID,                TCHR_Password,                TCHR_FirstName,                TCHR_LastName,                TCHR_CNIC,                TCHR_Address,                TCHR_SCLSchoolCode,                TCHR_PRGPCode,                TCHR_PRGSCode,                TCHR_Post,                TCHR_PhoneNo,                TCHR_Email,                TCHR_Gender,                TCHR_CCCityCode,                TCHR_CCCntryCode)
                        VALUES                     ('" + Model.TCHR_ID + "','" + Model.TCHR_Password + "','" + Model.TCHR_FirstName + "','" + Model.TCHR_LastName + "','" + Model.TCHR_CNIC + "','" + Model.TCHR_Address + "','" + Model.TCHR_SCLSchoolCode + "','" + Model.TCHR_PRGPCode + "','" + Model.TCHR_PRGSCode + "','" + Model.TCHR_Post + "','" + Model.TCHR_PhoneNo + "','" + Model.TCHR_Email + "','" + Model.TCHR_Gender + "','" + Model.TCHR_CCCityCode + "','" + Model.TCHR_CCCntryCode + "'";
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


        [Route( "teacher/{id}/edit" )]
        public ActionResult Edit( int id )
        {
            TeacherModel Model = new TeacherModel();
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


            Query = @"SELECT * FROM UPS_Teachers WHERE (TCHR_PKID = '" + id + "')";
            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                Model.TCHR_Address = FormDataTable.Rows[0]["TCHR_Address"].ToString();
                Model.TCHR_CCCityCode = FormDataTable.Rows[0]["TCHR_CCCityCode"].ToString();

                Model.TCHR_CCCntryCode = FormDataTable.Rows[0]["TCHR_CCCntryCode"].ToString();
                Model.TCHR_CNIC = FormDataTable.Rows[0]["TCHR_CNIC"].ToString();

                Model.TCHR_Post = FormDataTable.Rows[0]["TCHR_Post"].ToString();
                Model.TCHR_Email = FormDataTable.Rows[0]["TCHR_Email"].ToString();

                Model.TCHR_FirstName = FormDataTable.Rows[0]["TCHR_FirstName"].ToString();
                Model.TCHR_Gender = FormDataTable.Rows[0]["TCHR_Gender"].ToString();

                Model.TCHR_LastName = FormDataTable.Rows[0]["TCHR_LastName"].ToString();
                Model.TCHR_Password = FormDataTable.Rows[0]["TCHR_Password"].ToString();

                Model.TCHR_PhoneNo = FormDataTable.Rows[0]["TCHR_PhoneNo"].ToString();
                Model.TCHR_PRGPCode = FormDataTable.Rows[0]["TCHR_PRGPCode"].ToString();

                Model.TCHR_PRGSCode = FormDataTable.Rows[0]["TCHR_PRGSCode"].ToString();
                Model.TCHR_ID = FormDataTable.Rows[0]["TCHR_ID"].ToString();

                Model.TCHR_SCLSchoolCode = FormDataTable.Rows[0]["TCHR_SCLSchoolCode"].ToString();
                Model.PKID = FormDataTable.Rows[0]["TCHR_PKID"].ToString();
            }
            else
                ViewData["AlertData"] = ReturnValue;

            return View( Model );
        }


        [Route( "teacher/{id}" )]
        [HttpPatch]
        public ActionResult Edit( int id, TeacherModel Model )
        {
            try
            {
                Query = @"UPDATE UPS_Teachers SET " +
                        "TCHR_ID = '" + Model.TCHR_ID + "', TCHR_Password ='" + Model.TCHR_Password + "', TCHR_FirstName ='" + Model.TCHR_FirstName + "'," +
                        "TCHR_LastName ='" + Model.TCHR_LastName + "', TCHR_CNIC ='" + Model.TCHR_CNIC + "', TCHR_Address ='" + Model.TCHR_Address + "', TCHR_SCLSchoolCode ='" + Model.TCHR_SCLSchoolCode + "', " +
                        "TCHR_PRGPCode ='" + Model.TCHR_PRGPCode + "', TCHR_PRGSCode ='" + Model.TCHR_PRGSCode + "', TCHR_Post ='" + Model.TCHR_Post + "', " +
                        "TCHR_PhoneNo ='" + Model.TCHR_PhoneNo + "', TCHR_Email ='" + Model.TCHR_Email + "', TCHR_Gender ='" + Model.TCHR_Gender + "', " +
                        "TCHR_CCCityCode ='" + Model.TCHR_CCCityCode + "', TCHR_CCCntryCode ='" + Model.TCHR_CCCntryCode + "'";

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


        [Route( "teacher/{id}" )]
        [HttpDelete]
        public ActionResult Delete( FormCollection collection )
        {
            Query = @"DELETE FROM UPS_Teachers WHERE (TCHR_PKID = '" + collection["PKID"] + "')";
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
