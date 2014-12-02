using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("NewsComment", Schema = "dbo")]
    public class NewsCommentModel
    {
        [Key]
        public int CommentID { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }

        public int NewsID { get; set; }
        public int UserID { get; set; }
        public int ToUserID { get; set; }
        public bool IsClosed { get; set; }// 是否关闭
    }
}
