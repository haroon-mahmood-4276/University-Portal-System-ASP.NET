using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNi_Portal.Models
{
    public class TeacherModel
    {
        [Display( Name = "User ID" )]
        [Required( ErrorMessage = "User ID is required." )]
        [MinLength( 11 )]
        [MaxLength( 11 )]
        public string TCHR_ID { get; set; }


        [Display( Name = "Password" )]
        [Required( ErrorMessage = "Password is required." )]
        [MinLength( 6 )]
        [DataType( DataType.Password )]
        public string TCHR_Password { get; set; }


        [Display( Name = "First Name" )]
        [Required( ErrorMessage = "First Name is required." )]
        [MaxLength( 50 )]
        public string TCHR_FirstName { get; set; }


        [Display( Name = "Last Name" )]
        [Required( ErrorMessage = "Last Name is required." )]
        [MaxLength( 50 )]
        public string TCHR_LastName { get; set; }


        [Display( Name = "CNIC" )]
        [Required( ErrorMessage = "CNIC is required." )]
        [MaxLength( 15 )]
        public string TCHR_CNIC { get; set; }


        [Display( Name = "Address" )]
        [Required( ErrorMessage = "Address is required." )]
        [MaxLength( 250 )]
        public string TCHR_Address { get; set; }


        [Display( Name = "School" )]
        [Required( ErrorMessage = "School is required." )]
        public string TCHR_SCLSchoolCode { get; set; }


        [Display( Name = "Program" )]
        [Required( ErrorMessage = "Program is required." )]
        public string TCHR_PRGPCode { get; set; }


        [Display( Name = "Section" )]
        [Required( ErrorMessage = "Section is required." )]
        public string TCHR_PRGSCode { get; set; }


        [Display( Name = "Phone Number" )]
        [Required( ErrorMessage = "Phone Number is required." )]
        public string TCHR_PhoneNo { get; set; }


        [Display( Name = "Email" )]
        [Required( ErrorMessage = "Email is required." )]
        [MaxLength( 60 )]
        public string TCHR_Email { get; set; }


        [Display( Name = "Gender" )]
        [Required( ErrorMessage = "Gender is required." )]
        public string TCHR_Gender { get; set; }


        [Display( Name = "City" )]
        [Required( ErrorMessage = "City is required." )]
        public string TCHR_CCCityCode { get; set; }


        [Display( Name = "Country" )]
        [Required( ErrorMessage = "Country is required." )]
        public string TCHR_CCCntryCode { get; set; }


        [Display( Name = "Post" )]
        [Required( ErrorMessage = "Post is required." )]
        [MaxLength( 50 )]
        public string TCHR_Post { get; set; }
        public string PKID { get; set; }
    }
}