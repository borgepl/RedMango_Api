using DataAccess.Data;
using RedMango_Api.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data.Identity;

namespace RedMango_Api.Services
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DBInitializer> _logger;

        public DBInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext,
            ILogger<DBInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            // migrations if they are not yet applied
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during migrations");

            }

            // create roles if they are not created  
            await SeedRoles.SeedUsersAndRoles(_roleManager, _userManager, _dbContext);

        }
    }
}
