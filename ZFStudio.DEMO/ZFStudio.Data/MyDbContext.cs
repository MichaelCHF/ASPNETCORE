using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFStudio.Models;
using ZFStudio.Models.Interface;

namespace ZFStudio.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; }

        /// <summary>
        /// 容器初始化时传入options
        /// </summary>
        /// <param name="options"></param>
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges(); // Important!
            ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified && x is IBaseCreateTime)
                .Select(x => x.Entity).ToList().ForEach(item => {
                    (item as IBaseCreateTime).CreateTime = DateTime.Now;
                });

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
