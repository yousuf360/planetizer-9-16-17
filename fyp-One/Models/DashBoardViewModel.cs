using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fyp_One.Models
{
    public class DashBoardViewModel
    {
        [Required(ErrorMessage = "Please Enter Name of your venue")]
        public string venueName { get; set; }

        [Required(ErrorMessage = "Please Enter Youe venue type")]
        public string venueType { get; set; }

        [Required(ErrorMessage = "Please Enter Capacity of your venue")]
        public string capacity { get; set; }

        public string venueWebsite{ get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        public string vContactName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Street Address")]
        public string vStreetAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Your City")]
        public string vCity { get; set; }

        [Required(ErrorMessage = "Please Enter Your Country")]
        public string vCountry { get; set; }

        [Required(ErrorMessage = "Please Enter ZipCode")]
        public string vZipCode { get; set; }

        [Required(ErrorMessage = "Please Enter Your Contact number")]
        public string vContactNumber1 { get; set; }

        public string vContactNumber2 { get; set; }

        [Required(ErrorMessage = "Please Enter atleast one Feature")]
        public string venueFeature { get; set; }

        public IEnumerable<HttpPostedFileBase> venuePhoto { get; set; }

        [Required(ErrorMessage = "Please Enter Booking Rates")]
        public string venuePrice { get; set; }

        
    }

    public class VenueDateViewModel
    {
        
        public string VenueDate { get; set; }

        public int Venue_Id { get; set; }
    }
        public class AddCaterViewModel
    {
        //
        //Cater View Model
        //

        [Required(ErrorMessage = "Please Enter Cater Service Name")]
        public string CaterServiceName { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string cStreetAddress { get; set; }

        [Required(ErrorMessage = "Please Enter City")]
        public string cCity { get; set; }

        [Required(ErrorMessage = "Please Enter Country")]
        public string cCountry { get; set; }

        [Required(ErrorMessage = "Please Enter Zip Code")]
        public string cZipCode { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Number")]
        public string cContactNumber { get; set; }

        public string cContactNumber2 { get; set; }
    }

    public class MenuFormViewModel
    {
        public int Cater_Id { get; set; }

        [Required(ErrorMessage = "Please Enter Menu Name")]
        public string mName { get; set; }

        [Required(ErrorMessage = "Please Enter Menu Price per head")]
        public string mPrice { get; set; }

        [Required(ErrorMessage = "Please Enter Menu Item")]
        public string ItemName { get; set; }
    }
} 