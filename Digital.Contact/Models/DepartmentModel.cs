using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("Department", Schema = "dbo")]
    public class DepartmentModel
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

    }
}
