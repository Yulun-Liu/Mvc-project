using _1121726Final.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using X.PagedList;
using X.PagedList.Extensions;
namespace _1121726Final.Data
{
    public class CmsContext:DbContext
    {
        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {
        }
        //public DbSet<Models.Group> TableGroup { get; set; }
       // public DbSet<Concert> TableConcert { get; set; }
       // public DbSet<Ticket> TableTicket { get; set; }
        public DbSet<Models.Group> TableGroups1121726 { get; set; }
        public DbSet<Concert> TableConcerts1121726 { get; set; }
        public DbSet<Ticket> TableTickets1121726 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
