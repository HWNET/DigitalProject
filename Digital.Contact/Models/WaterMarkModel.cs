using Digital.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    public class WaterMarkModel
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }
        public int WaterPostion { get; set; }
        public bool IsCompanyName { get; set; }
        public bool IsWebsite { get; set; }
    }
}
