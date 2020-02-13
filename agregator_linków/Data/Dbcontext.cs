using agregator_linków.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Data
{
    public class Dbcontext : DbContext
    {
       public Dbcontext(DbContextOptions<Dbcontext> options): base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<UserLike>()
            .HasOne<User>(p => p.user)
            .WithMany(p => p.userLike)
            .HasForeignKey(p => p.userid)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLike>()
                .HasOne<Link>(p => p.link)
                .WithMany(p => p.userLike)
                .HasForeignKey(p => p.linkid)
                .OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<User> users { get; set; }
        public DbSet<Link> link { get; set; }
        public DbSet<UserLike> userLikes { get; set; }
    }
}
