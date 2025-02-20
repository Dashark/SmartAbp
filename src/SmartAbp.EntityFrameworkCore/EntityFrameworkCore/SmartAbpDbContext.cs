﻿using Microsoft.EntityFrameworkCore;
using SmartAbp.Books;
using SmartAbp.Stations;
using SmartAbp.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace SmartAbp.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See SmartAbpMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]  //数据源，在appsettings.json中
    public class SmartAbpDbContext : AbpDbContext<SmartAbpDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        //下面是我添加的为了生成Books
        public DbSet<Book> Books { get; set; }
        //下面是我添加的为了生成
        public DbSet<Station> Stations { get; set; }
        //下面是我添加的为了生成
        public DbSet<WeldSection> WeldSections { get; set; }
        //下面是我添加的为了生成
        public DbSet<Robot> Robots { get; set; }
        //下面是我添加的为了生成
        public DbSet<TransportRobot> TransportRobots { get; set; }
        //下面是我添加的为了生成
        public DbSet<Station.ProtectedGas> ProtectedGass { get; set; }
        //下面是我添加的为了生成
        public DbSet<Station.CompressedGas> CompressedGass { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside SmartAbpDbContextModelCreatingExtensions.ConfigureSmartAbp
         */

        public SmartAbpDbContext(DbContextOptions<SmartAbpDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention(); //变量名与表字段的映射
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the SmartAbpEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureSmartAbp method */

            builder.ConfigureSmartAbp();
        }
    }
}
