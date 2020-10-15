using greenSacrifice.Blazor.Models.DbDataModels;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.EntityFrameworkCore;

namespace greenSacrifice.Blazor.Repo.Context
{
    public class HelloWorldDbContext : AppDbContext
    {
        public HelloWorldDbContext(AzureADOptions azureAdOptions, DbContextOptions<HelloWorldDbContext> options)
        : base(azureAdOptions, options)
        {
        }

        public virtual DbSet<TbDemoMessage> TbDemo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // sql tables
            modelBuilder.Entity<TbDemoMessage>(tb => tb.ToTable("tbDemoMessage", "HelloWorld"));
        }
    }
}
