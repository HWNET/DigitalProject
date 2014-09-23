using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class ProjectProgressModel
    {
        [Key]
        public int ProgressID { get; set; } //总进度表ID
        public string ProgressName { get; set; }
        public string PhaseEvent { get; set; }
        public string CurrentEvent { get; set; }
        public double FinishPercent { get; set; }
    }
}
