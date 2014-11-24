using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class MenuModel
    {
        [Key]
        public int ID { get; set; }
        public string MenuName { get; set; }
        /// <summary>
        /// 可能其他的名字，根据不同的用户类型
        /// </summary>
        public string OtherName { get; set; }
        public int ParentId { get; set; }
        public string Url { get; set; }
        public string Style { get; set; }
    }
}
