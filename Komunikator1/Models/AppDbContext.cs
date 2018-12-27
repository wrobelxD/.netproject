using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Komunikator1.Models
{
    public class AppDbContext:DbContext 
    {
        public AppDbContext():base("ConnectionStringName")
        {
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }

    }
}