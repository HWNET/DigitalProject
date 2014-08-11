using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class UsersModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "用户名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "密 码")]
        public string Passwords { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "注册时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]  
        public DateTime RegisterDate { get; set; }
        public virtual ICollection<IdeaModel> IdeaModelList { get; set; }
    }
}
