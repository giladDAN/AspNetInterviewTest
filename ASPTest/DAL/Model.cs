using ASPTest.DAL;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ASPTest.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<SelectedItems> SelectedItems { get; set; }
        public Model() : base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
