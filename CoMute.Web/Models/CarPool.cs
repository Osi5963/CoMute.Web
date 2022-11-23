using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace CoMute.Web.Models
{
    public class CarPool
    {
        [Key] 
        
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ExpectedArrivalTime { get; set; }
        [Required]
        public string Origin { get; set; }
        public int DaysAvailable { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int AvailableSeats { get; set; }

        [ForeignKey("OwnerId")]
        [Required]
        public Guid OwnerId { get; set; }
        public string  Note { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}
