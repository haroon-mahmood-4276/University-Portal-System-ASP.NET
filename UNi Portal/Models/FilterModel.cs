using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNi_Portal.Models
{
    public class FilterModel
    {
        [Display( Name = "Schools" )]
        [Required( ErrorMessage = "School is required" )]
        public string PRG_SCLSchoolCode { get; set; }


        [Display( Name = "Program" )]
        [Required( ErrorMessage = "Program is required" )]
        public string PRG_PCode { get; set; }


        [Display( Name = "Section" )]
        [Required( ErrorMessage = "Section is required" )]
        public string PRG_SCode { get; set; }


        [Display( Name = "Subject" )]
        [Required( ErrorMessage = "Subject is required" )]
        [MaxLength( 50 )]
        public string Subject { get; set; }


        [Display( Name = "Class Date" )]
        [Required( ErrorMessage = "Class Date is required" )]
        public string ClassDate { get; set; }


        [Display( Name = "Exam Type" )]
        [Required( ErrorMessage = "Exam Type is required" )]
        public string ExamType { get; set; }


        [Display( Name = "Total Marks" )]
        [Required( ErrorMessage = "Total Marks is required" )]
        public double TotalMarks { get; set; }


        [Display( Name = "Class Start Time" )]
        [Required( ErrorMessage = "Class Start Time is required" )]
        public string StartTime { get; set; }


        [Display( Name = "Class End Time" )]
        [Required( ErrorMessage = "Class End Time is required" )]
        public string EndTime { get; set; }
    }
}