using LocationMap.Common;
using LocationMap.DataLayer.Context;
using LocationMap.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LocationMap.Services
{
    public interface IDbInitializerService
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }

    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        //  private readonly ISecurityService _securityService;

        public DbInitializerService(
            IServiceScopeFactory scopeFactory
            //ISecurityService securityService
        )
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            // _securityService = securityService;
            // _securityService.CheckArgumentIsNull(nameof(_securityService));
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (!context.LocationType.Any())
                    {
                        var work = new LocationType()
                        {
                            Name = "Work"
                        };

                        var business = new LocationType()
                        {
                            Name = "Business"
                        };


                        context.Add(work);
                        context.Add(business);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}