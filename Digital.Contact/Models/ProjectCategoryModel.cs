using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class ProjectCategoryModel
    {
        [Key]
        public int ProjectCateID { get; set; }
        public string ProjectCateName { get; set; }

        public virtual ICollection<ProjectModel> ProjectModels { get; set; }
    }
}
