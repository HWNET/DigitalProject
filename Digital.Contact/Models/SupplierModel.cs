using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class SupplierModel
    {
        [Key]
        public int SupplierID { get; set; }
        public int CompanyID { get; set; } //被选为供应商的企业ID
        public int Level { get; set; }//1、项目级供应商  2、全局供应商  3、优质供应商  4、战略合作伙伴

    }
}
