using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Digital.Contact.DAL
{
    public class CommunicationContext:DbContext
    {
        public CommunicationContext():base("CommunicationContext"){}

        public DbSet<Digital.Contact.Models.IdeaModel> IdeaModels { get; set; }

        public DbSet<Digital.Contact.Models.UsersModel> UsersModels { get; set; }

        public DbSet<Digital.Contact.Models.TemplateModel> TemplateModels { get; set; }

        public DbSet<Digital.Contact.Models.TempColumnModel> TempColumnModels { get; set; }

        public DbSet<Digital.Contact.Models.RelationTableModels> RelationTableModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
