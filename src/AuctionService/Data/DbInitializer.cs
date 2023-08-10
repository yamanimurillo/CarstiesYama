using AuctionService.Data.Seeds;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app) 
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
    }

    private static void SeedData(AuctionDbContext context)
    {
        context.Database.Migrate();

        if(context.Auctions.Any()) 
        {
            Console.WriteLine("Data already loaded");
            return;
        }

        var auctions = Auctions.Seeds;

        context.AddRange(auctions);

        context.SaveChanges();
    }
}
