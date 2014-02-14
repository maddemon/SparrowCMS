using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace SparrowCMS.DAL.SqlServer
{
    public class DataContext : DbContext
    {
        protected DataContext() : base("name=DBDataContext")
        {
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Template> Templates { get; set; }

    }
}
