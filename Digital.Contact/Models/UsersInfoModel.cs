using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class UsersInfoModel
    {
        [Key]
        public int UsersInfoID { get; set; }
       
        [Display(Name = "真实姓名")]
        public string TrueName { get; set; }

       
        [Display(Name = "昵称")]
        public string NickName { get; set; }

      
        public int Sex { get; set; }

      
        [Display(Name = "电话")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        /// <summary>
        /// 显示头像
        /// </summary>
        public string DisplayPicture { get; set; }

        /// <summary>
        /// 从XML中获取省份ID
        /// </summary>
        public int ProvinceID { get; set; }



        /// <summary>
        /// 从XML中获取城市ID
        /// </summary>
        public int CityID { get; set; }


        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }


        public string Email { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name = "邮编")]
        public string Zip { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "个人介绍")]
        public string BeGoodAtIntroduction { get; set; }


        public int GoodAtWhatID { get; set; }

        public virtual List<GoodAtWhatModel> GoodAtWhatModels { get; set; }

        public CityModel CityModels { get; set; }
       
        public int UpdateStatus { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string SexToString()
        {
            switch (Sex)
            {
                case 0:
                    return "男";
                case 1:
                    return "女";
                default:
                    return "未知";
            }
        }
    }
}
