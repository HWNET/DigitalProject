using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{

    public enum TitleType
    {
        /// <summary>
        /// 头条
        /// </summary>
        Topline = 0, 
        /// <summary>
        /// 推荐
        /// </summary>
        Recommend = 1, 
        /// <summary>
        /// 幻灯
        /// </summary>
        Slide = 2, 
        /// <summary>
        /// 特荐
        /// </summary>
        SRecommend = 3, 
        /// <summary>
        /// 滚动
        /// </summary>
        Roll=4,
        /// <summary>
        /// 加粗
        /// </summary>
        Overstriking=5
    }

    public class NewsModels
    {
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 标题属性
        /// </summary>
        public TitleType? NewTitleType { get; set; }
        ///// <summary>
        ///// 浏览权限
        ///// </summary>
        //public BrowseRightModel BrRight { get; set; }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string PicAddress { get; set; }

        public string NewSource { get; set; }
        public virtual ICollection<IdeaModel> IdeaModelList { get; set; }
    }
}
