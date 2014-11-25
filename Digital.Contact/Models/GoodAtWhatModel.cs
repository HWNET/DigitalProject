using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class GoodAtWhatModel
    {
        [Key]
        public int GoodAtWhatID { get; set; }
        public int UsersInfoID { get; set; }
        public int SkillId { get; set; }
        public int UpdateStatus { get; set; }
        public SkillsModel SkillsModel { get; set; }

    }
}
