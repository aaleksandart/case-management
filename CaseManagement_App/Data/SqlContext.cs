using CaseManagement_App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Data
{
    internal class SqlContext : DbContext
    {
        public SqlContext()
        {

        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ContactInfo> ContactInfos { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CaseState> CaseStates { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cases> Cases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aleks\Desktop\Case_Management\CaseManagement_App\CaseManagement_App\Data\sql_case-management.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }
}
