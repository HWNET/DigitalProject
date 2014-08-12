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
        [DataType(DataType.EmailAddress)]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
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


        /// <summary>
        /// 用户状态<br />
        /// 0正常，1锁定，2未通过邮件验证，3未通过管理员确认
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public Nullable<DateTime> LoginTime { get; set; }

        /// <summary>
        /// 上次登陆IP
        /// </summary>
        public string LoginIP { get; set; }

        /// <summary>
        /// 用户状态文字说明
        /// </summary>
        /// <returns></returns>
        public string StatusToString()
        {
            switch (Status)
            {
                case 0:
                    return "正常";
                case 1:
                    return "已锁定";
                case 2:
                    return "未通过邮件验证";
                case 3:
                    return "未通过管理员确认";
                default:
                    return "未知";
            }
        }

        public virtual ICollection<IdeaModel> IdeaModelList { get; set; }
    }
}
