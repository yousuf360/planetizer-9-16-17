using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fyp_One.Models
{
    public class VenueOrderViewModel
    {
        
        public int Venue_Id { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Please Enter Contact Number")]
            public string ContactNo { get; set; }

            [Required(ErrorMessage = "Please Enter Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please Enter Number Of Gests")]
            public string Guests { get; set; }

            public DateTime EventDate { get; set; }
        }

    public class CaterOrderViewModel
    {
        public int Menu_Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Number Of Gests")]
        public string Guests { get; set; }

        public DateTime EventDate { get; set; }
    }


    }