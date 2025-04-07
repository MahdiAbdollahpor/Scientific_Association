using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Identity
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Tiltle { get; set; }
        public bool IsDeleted { get; set; }

        #region rel

        public IEnumerable<UserRole> UserRoles { get; set; }

        #endregion
    }
}
