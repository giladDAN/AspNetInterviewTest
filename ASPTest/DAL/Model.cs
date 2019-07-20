namespace ASPTest.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Lookup> Lookup { get; set; }
        public virtual DbSet<SelectedItems> SelectedItems { get; set; }

        public List<Tuple<int, int>> GetItemsAndCount()
        {
            using (var context = new Model())
            {
                try
                {
                    var res = context.Database.SqlQuery<SelectedItemsAndCount>("GetItemsAndCount").ToList();

                    return res.Select(item => new Tuple<int, int>(item.Number, item.Count)).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<Tuple<int, int>> GetItemsAndCountLinq()
        {
            using (var context = new Model())
            {
                try
                {
                    return context.SelectedItems
                        .GroupBy(item => item.Number)
                        .ToList()
                        .Select(group => new Tuple<int, int>(group.Key, group.Count())).ToList();
                        
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<Tuple<int, int>> GetItemsAccroding(DateTime Date)
        {
            using (var context = new Model())
            {
                try
                {
                    var clientIdParameter = new SqlParameter("@dateParam", Date.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    var res = context.Database.SqlQuery<SelectedItemsAndCount>("GetItemsAccroding @dateParam", clientIdParameter).ToList();

                    return res.Select(item => new Tuple<int, int>(item.Number, item.Count)).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<Tuple<int, int>> GetItemsAccrodingLinq(DateTime Date)
        {
            using (var context = new Model())
            {
                try
                {
                    return context.SelectedItems
                        .Where(item => item.ShowDate >= Date)
                        .GroupBy(item => item.Number)
                        .ToList()
                        .Select(group => new Tuple<int, int>(group.Key, group.Count())).ToList();
                        
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
