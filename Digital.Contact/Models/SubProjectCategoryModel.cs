using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class SubProjectCategoryModel
    {
        [Key]
        public int SubProjectCateID { get; set; }
        public string SubProjectCateName { get; set; }

        public virtual ICollection<SubProjectModel> SubProjectModels { get; set; }
    }
}
