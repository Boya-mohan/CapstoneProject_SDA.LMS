using mohan_CapstoneProject_SDA.LMS.Data.Static;
using mohan_CapstoneProject_SDA.LMS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScop = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScop.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // Medicines
                if (!context.Medicines.Any())
                {
                    context.Medicines.AddRange(new List<Medicine>() {
                    new Medicine()
                    {
                        ImageCode = 1,
                        Name = "Metanium Everyday Barrier Ointment 40gm",
                        Description="Brand: Metanium, PROTECT and PREVENT the baby from nappy rash, delicate of skin",
                        Price=45.77,
                        MedicineCategory=MedicineCategory.MumAndBaby},
                    new Medicine()
                    {
                        ImageCode = 2,
                        Name = "Biomagic Hair Color Cream",
                        Description="Brand: BioMagic, Keratin & Argan Oil, 8.72 Light Beige Blonde Color, Free From Ammonia - 1 Kit",
                        Price=74.75,
                        MedicineCategory=MedicineCategory.BeautyCare},

                    new Medicine()
                    {
                        ImageCode = 3,
                        Name = "Apricot Scrub",
                        Description="Brand: Nature'S Bounty, Apricot Scrub Gently Eliminates Dead Skin Cells And Roughness",
                        Price=33.50,
                        MedicineCategory=MedicineCategory.PersonalCare},

                    new Medicine()
                    {
                        ImageCode = 4,
                        Name = "Panadol Blue",
                        Description="Brand: Panadol, Panadol Blue Paracetamol 500 Mg - 24 Tabs",
                        Price=5.00,
                        MedicineCategory=MedicineCategory.MedicineAndTreatment},

                    new Medicine()
                    {
                        ImageCode = 5,
                        Name = "Paradox Omega 3",
                        Description="Brand: Paradox, Omega 3 Chews for School Kids, suitable for ages 5 - 18",
                        Price=93.00,
                        MedicineCategory=MedicineCategory.Vitamins},

                    new Medicine()
                    {
                        ImageCode = 6,
                        Name = "Nivea After Shave Balm Replenishing",
                        Description="Deeply moisturizer Lotion helps prevent dry and tight skin delivers a long-lasting",
                        Price=31.76,
                        MedicineCategory=MedicineCategory.PersonalCare},

                    new Medicine()
                    {
                        ImageCode = 7,
                        Name = "King.C.Gillette, Beard Trimmer",
                        Description="King.C.Gillette, Beard Trimmer 4 Accessories - 1 Kit",
                        Price=151.86,
                        MedicineCategory=MedicineCategory.PersonalCare},

                    new Medicine()
                    {
                        ImageCode = 8,
                        Name = "Betika, Ointment, Intensive Care",
                        Description="Improve Skin Appearance, For Wound Healing - 60 Gm",
                        Price=79.35,
                        MedicineCategory=MedicineCategory.MedicineAndTreatment},

                    new Medicine()
                    {
                        ImageCode = 9,
                        Name = "Biodal 50.000, Vitamin D3",
                        Description="Biodal 50.000 Iu 20 Tab, Vitamin D, Storage conditions: store below 30°c",
                        Price=64.1,
                        MedicineCategory=MedicineCategory.Vitamins}

                    });
                    context.SaveChanges();

                }


            }
        }
        
        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScop = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Roles
                var roleManager = serviceScop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); // Used default IdentityRole V.83
                
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                     await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                     await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                //~

                // Users | Admin
                var userManager = serviceScop.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); // Created model customized inheret from IdentityUserRole V.83
                string AdminUserEmail= "admin@capstone.com";
                var adminUser = await userManager.FindByEmailAsync(AdminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser
                    {
                        FullName = "Admin",
                        UserName = "admin-User",
                        Email = AdminUserEmail,
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };
                    await userManager.CreateAsync(newAdminUser, "!Admin1");          // [TroubleShooting] when seed pass shoud provide pass_requiremen otherwice get FK_Conflect at FK_AspNetUserRoles MSH
                    await userManager.AddToRoleAsync(newAdminUser,UserRoles.Admin);
                }//~
                
                // Users | User
                string XUserEmail = "xuser@capstone.com";
                var XUser = await userManager.FindByEmailAsync(XUserEmail);
                if (XUser == null)
                {
                    var newXUser = new ApplicationUser
                    {
                        FullName = "User No.0",
                        UserName = "Xuser-User",
                        Email = XUserEmail,
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };
                    await userManager.CreateAsync(newXUser, "!User1");
                    await userManager.AddToRoleAsync(newXUser, UserRoles.User);
                }//~ 

            }
        }
        
    }
}



                         










                                     
                        
                       
                    


   
    
