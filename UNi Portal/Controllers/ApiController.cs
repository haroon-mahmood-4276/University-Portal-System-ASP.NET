using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using UNi_Portal.Models;

namespace UNi_Portal.Controllers
{
    public class ApiController : Controller
    {
        string ReturnValue = "";
        string Query = "";
        DataTable FormDataTable = new DataTable();


        [Route( "api/schools/{id}" )]
        public JsonResult ProgramList( string id )
        {
             Query = @"SELECT DISTINCT PRG_PCode, PRG_ProgramName FROM UPS_Programs WHERE (PRG_SCLSchoolCode = '" + id + "')";
             ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                ReturnValue = JsonConvert.SerializeObject( FormDataTable );
                return Json( ReturnValue, JsonRequestBehavior.AllowGet );
            }
            return Json( "Data Not Found", JsonRequestBehavior.AllowGet );
        }

        [Route( "api/programs/{schoolid}/{id}" )]
        public JsonResult ProgramList(string schoolid, string id )
        {
            DataTable FormDataTable = new DataTable();
            string Query = @"SELECT DISTINCT PRG_SCode, PRG_SectionName FROM UPS_Programs WHERE (PRG_SCLSchoolCode = '" + schoolid + "' AND PRG_PCode = '" + id + "')";
            string ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );
            if ( ReturnValue == "Y" )
            {
                ReturnValue = JsonConvert.SerializeObject( FormDataTable );
                return Json( ReturnValue, JsonRequestBehavior.AllowGet );
            }
            return Json( "Data Not Found", JsonRequestBehavior.AllowGet );
        }


        [Route( "api/teacher/students/get/{schoolid}/{programid}/{id}" )]
        public JsonResult STDList( string schoolid,string programid, string id )
        {
            ViewData[ "AlertData" ] = "";
            ViewData[ "AlertType" ] = "danger";
            ViewData[ "AlertHeading" ] = "Error";
            Query = @"SELECT STD_RollNo, STD_FirstName, STD_LastName, 
                    (SELECT DISTINCT PRG_ProgramName FROM UPS_Programs WHERE (PRG_PCode = STD_PRGPCode)) AS PRG_ProgramName, 
                    (SELECT DISTINCT PRG_SectionName FROM UPS_Programs WHERE (PRG_PCode = STD_PRGPCode) AND (PRG_SCode = STD_PRGSCode)) AS PRG_SectionName, STD_CrntSemester,
                    (SELECT DISTINCT SCL_SchoolName FROM UPS_Schools WHERE (SCL_SchoolCode = STD_SCLSchoolCode)) AS SCL_SchoolName, 
                    (SELECT DISTINCT SCL_SchoolAbb FROM UPS_Schools WHERE (SCL_SchoolCode = STD_SCLSchoolCode)) AS SCL_SchoolAbb, STD_PKID 
                    FROM UPS_Students WHERE (STD_PRGPCode = '" + programid + "') AND (STD_PRGSCode = '" + id + "') AND (STD_SCLSchoolCode = '" + schoolid + "') ORDER BY STD_RollNo";

            ReturnValue = DBQueries.DBFilDTable( ref FormDataTable, Query );

            if ( ReturnValue == "Y" )
            {
                ReturnValue = JsonConvert.SerializeObject( FormDataTable );
                return Json( ReturnValue, JsonRequestBehavior.AllowGet );
            }
            return Json( "Data Not Found", JsonRequestBehavior.AllowGet );
        }
    }
}