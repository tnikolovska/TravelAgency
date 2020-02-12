using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TravelAgency.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Cruise> Cruises { get; set; }
        public DbSet<DistantDestination> DistantDestinations { get; set; }
        public DbSet<HotelSpecialOffer> HotelSpecialOffers { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<CruisePlan> CruisePlans { get; set; }
        public DbSet<CruiseArrangement> CruiseArrangements { get; set; }
        public DbSet<ArrangementPrice> ArrangementPrices { get; set; }
        public DbSet<ArrangementPriceSpecial> ArrangementPriceSpecials { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}