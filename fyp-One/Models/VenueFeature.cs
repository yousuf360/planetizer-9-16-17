//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fyp_One.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VenueFeature
    {
        public int Id { get; set; }
        public string venuefeatures { get; set; }
        public int Id_venueInfo { get; set; }
    
        public virtual VenueInfo VenueInfo { get; set; }
    }
}
