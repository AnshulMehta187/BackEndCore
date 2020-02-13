using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models.Models
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MenuBatch> MenuBatch { get; set; }
        public virtual DbSet<MenuCity> MenuCity { get; set; }
        public virtual DbSet<MenuCountry> MenuCountry { get; set; }
        public virtual DbSet<MenuState> MenuState { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentDetails> StudentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }

        
    }
}
