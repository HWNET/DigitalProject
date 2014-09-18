using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class CompanyModel
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
    }
}
