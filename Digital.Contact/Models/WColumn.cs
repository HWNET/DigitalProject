using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    public class WColumn
    {

        [Key]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string ColumnName { get; set; }

        public int MappingId { get; set; }

        public int ColumnType { get; set; }

        public int SingleId { get; set; }
    }

    public enum MappingColumn : int
    {
        NewsModel = 1,
        CasesModel = 2,
        PatentModel=3
    }

    public enum ColumnType:int 
    {
        SinglePageModel=1,
        List=2
    }

}
