using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fyp_One.Models
{
    public class VenueEditVM
    {
        public VenueInfo venueinfo { get; set; }

        public List<VenueAddress> venueaddress { get; set; }


        public List<VenueFeature> venuefeature { get; set; }
    }
}