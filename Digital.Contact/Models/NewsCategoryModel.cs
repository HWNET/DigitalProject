using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class NewsCategoryModel
    {
        [Key]
        public int NewsCateID { get; set; }
        public string NewsCateName { get; set; }
        public string NewsCateOrder { get; set; }
        public int ParentNewsCateID { get; set; }

        public virtual ICollection<NewsModel> NewsModels { get; set; }
    }
}
