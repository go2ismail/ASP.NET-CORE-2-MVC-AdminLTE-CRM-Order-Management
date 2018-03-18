using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using motekarteknologi.Areas.security.Models;
using motekarteknologi.Data;
using motekarteknologi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace motekarteknologi.Motekar
{
    public static class Seed
    {
        private static string _defaultCompany = "Default";
        private static string _defaultAdminEmail = "admin@gmail.com";
        private static string _defaultPassword = "P@$$w0rd";

        public static async Task CreateDefaultCompany(ApplicationDbContext _context)
        {
            var company = new Company();
            company.Name = _defaultCompany;
            company.Description = "This is your default Company. You can add more companies";

            _context.Company.Add(company);
            await _context.SaveChangesAsync();
        }

        private static async Task<IdentityResult> CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            IdentityResult roleResult = new IdentityResult();

            var roleExist = await roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            return roleResult;
        }

        private static async Task<IdentityResult> CreateUser(UserManager<ApplicationUser> userManager, ApplicationUser appUser, string userPassword)
        {
            var user = await userManager.FindByEmailAsync(appUser.Email);

            IdentityResult userResult = new IdentityResult();

            if (user == null)
            {
                userResult = await userManager.CreateAsync(appUser, userPassword);

                return userResult;
            }

            return userResult;
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //modules
            foreach (var item in Modules.All)
            {
                await CreateRole(RoleManager, item);
            }

            //security
            foreach (var item in SecurityModules.All)
            {
                await CreateRole(RoleManager, item);
            }

            //todo: crm
            //todo: inventory
            //todo: po
            //todo: so
        }

        public static async Task CreateUsers(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var emailAdmin = _defaultAdminEmail;

            var passwordDemo = _defaultPassword;

            var superAdmin = new ApplicationUser
            {
                UserName = emailAdmin,
                Email = emailAdmin
            };

            var superAdminResult = await CreateUser(UserManager,
                superAdmin,
                passwordDemo);

            if (superAdminResult.Succeeded)
            {
                //modules
                foreach (var item in Modules.All)
                {
                    await UserManager.AddToRoleAsync(superAdmin, item);
                }

                //security
                foreach (var item in SecurityModules.All)
                {
                    await UserManager.AddToRoleAsync(superAdmin, item);
                }

                //todo: crm
                //todo: inventory
                //todo: po
                //todo: so
            }

            
        }

        public static async Task CreateCompanyRole (ApplicationDbContext _context)
        {
            var company = _context.Company
                .Where(x => x.Name.Equals(_defaultCompany)).FirstOrDefault();

            var admin = _context.ApplicationUser
                .Where(x => x.Email.Equals(_defaultAdminEmail)).FirstOrDefault();

            var companyUser = new CompanyUser {
                Company = company,
                ApplicationUser = admin
            };

            _context.CompanyUser.Add(companyUser);
            await _context.SaveChangesAsync();
        }
    }
}
