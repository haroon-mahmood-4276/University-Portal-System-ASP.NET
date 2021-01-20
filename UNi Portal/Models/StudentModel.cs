using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNi_Portal.Models
{
    public class StudentModel
    {
        [Display( Name = "User ID" )]
        [Required( ErrorMessage = "User ID is required." )]
        [MinLength( 11 )]
        [MaxLength( 11 )]
        public string STD_RollNo { get; set; }


        [Display( Name = "Password" )]
        [Required( ErrorMessage = "Password is required." )]
        [MinLength( 6 )]
        [DataType( DataType.Password )]
        public string STD_Password { get; set; }


        [Display( Name = "First Name" )]
        [Required( ErrorMessage = "First Name is required." )]
        [MaxLength( 50 )]
        public string STD_FirstName { get; set; }


        [Display( Name = "Last Name" )]
        [Required( ErrorMessage = "Last Name is required." )]
        [MaxLength( 50 )]
        public string STD_LastName { get; set; }


        [Display( Name = "CNIC" )]
        [Required( ErrorMessage = "CNIC is required." )]
        [MaxLength( 15 )]
        public string STD_CNIC { get; set; }
        

        [Display( Name = "Address" )]
        [Required( ErrorMessage = "Address is required." )]
        [MaxLength( 250 )]
        public string STD_Address { get; set; }


        [Display( Name = "School" )]
        [Required( ErrorMessage = "School is required." )]
        public string STD_SCLSchoolCode { get; set; }


        [Display( Name = "Program" )]
        [Required( ErrorMessage = "Program is required." )]
        public string STD_PRGPCode { get; set; }


        [Display( Name = "Section" )]
        [Required( ErrorMessage = "Section is required." )]
        public string STD_PRGSCode { get; set; }


        [Display( Name = "Current Semester" )]
        [Required( ErrorMessage = "Current Semester is required." )]
        public string STD_CrntSemester { get; set; }


        [Display( Name = "Phone Number" )]
        [Required( ErrorMessage = "Phone Number is required." )]
        public string STD_PhoneNo { get; set; }


        [Display( Name = "Email" )]
        [Required( ErrorMessage = "Email is required." )]
        [MaxLength( 60 )]
        public string STD_Email { get; set; }


        [Display( Name = "Section" )]
        [Required( ErrorMessage = "Section is required." )]
        public string STD_Gender { get; set; }

        
        [Display( Name = "City" )]
        [Required( ErrorMessage = "City is required." )]
        public string STD_CCCityCode { get; set; }

        
        [Display( Name = "Country" )]
        [Required( ErrorMessage = "Country is required." )]
        public string STD_CCCntryCode { get; set; }


        [Display( Name = "Picture" )]
        //[Required( ErrorMessage = "Country is required." )]
        [MaxLength( 150 )]
        public string STD_Picture { get; set; }

    }
}