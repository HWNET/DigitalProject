using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class NewsCommentModel
    {
        [Key]
        public int CommentID { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public int UserID { get; set; }
        public int ToUserID { get; set; }
        public bool IsClosed { get; set; }// 是否关闭
    }
}
