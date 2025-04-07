using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Identity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }


    }
}
