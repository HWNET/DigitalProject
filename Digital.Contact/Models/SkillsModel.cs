using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class SkillsModel
    {
        [Key]
        public int SkillId { get; set; }
       
        /// <summary>
        /// 技能名称
        /// </summary>
        public string Name { get; set; }
    }
}
