using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoMute.Web.Models
{
    public class LinkedCarPools
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("CarPoolId")]
        public  Guid? CarPoolId { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }

        public List<CarPool> CarPools { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
