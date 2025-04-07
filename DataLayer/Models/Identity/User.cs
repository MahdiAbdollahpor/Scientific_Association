using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public string PhoneNumber { get; set; }
        public string nationalCode { get; set; }
        public string studentNumber { get; set; }
        public string Password { get; set; }
        public DateTime RegisterTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }


        #region rel

        public IEnumerable<UserRole> UserRoles { get; set; }


        #endregion

    }
}
