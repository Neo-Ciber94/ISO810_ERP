using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Models;

public class ErpDbContext : DbContext
{
    public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<Expense> Expenses { get; set; } = default!;
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedDatabase(modelBuilder);
    }

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().HasData(new[]
    {
            new Currency { Name = "Dominican Peso", ShortCode= "DOP",Symbol = "RD$" },
            new Currency { Name = "US Dollar", ShortCode = "USD", Symbol = "$" },
            new Currency { Name = "Euro", ShortCode = "EUR", Symbol = "€" },
            new Currency { Name = "Pound Sterling", ShortCode = "GBP", Symbol = "£" },
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new[]
        {
            new ExpenseCategory{ Name = "Customer Support" },
            new ExpenseCategory{ Name = "Development / R&D" },
            new ExpenseCategory{ Name = "Marketing" },
            new ExpenseCategory{ Name = "Ops" },
            new ExpenseCategory{ Name = "Web Hosting" },
            new ExpenseCategory{ Name = "Productivity" }
        });

        modelBuilder.Entity<Service>().HasData(new[]
        {
            new Service { Name = "MongoDB", HomePage="https://www.mongodb.com/atlas" },
            new Service {Name = "Monday", HomePage="https://monday.com/"},
            new Service {Name = "CircleCI",  HomePage="https://circleci.com/"},
            new Service {Name = "Filestage",  HomePage="https://filestage.io/"},
            new Service {Name = "Hubspot",  HomePage="https://www.hubspot.com/"},
            new Service {Name = "Balsamiq", HomePage= "https://balsamiq.com/"},
            new Service {Name = "Adobe", HomePage= "https://www.adobe.com/"},
            new Service {Name = "Abstract",  HomePage="https://www.abstract.com/"},
            new Service {Name = "Algolia",  HomePage="https://www.algolia.com/"},
            new Service {Name =  "AWS", HomePage= "https://aws.amazon.com/"},
            new Service {Name =  "Hetzner",  HomePage="https://www.hetzner.com/cloud"},
            new Service {Name =  "Google Cloud",  HomePage="https://cloud.google.com/"},
            new Service {Name =  "Azure",  HomePage="https://azure.microsoft.com/en-us/"},
            new Service {Name =  "Vultr",  HomePage="https://www.vultr.com/"},
            new Service {Name =  "PayPal",  HomePage="https://www.paypal.com/"},
            new Service {Name =  "Digital Ocean",  HomePage="https://www.digitalocean.com/"},
            new Service {Name =  "Netcup",  HomePage="https://www.netcup.eu/"},
            new Service {Name =  "Stripe",  HomePage="https://stripe.com/"},
            new Service {Name =  "Square",  HomePage="https://squareup.com/"},
            new Service {Name =  "Coinbase Commerce",  HomePage="https://commerce.coinbase.com/"},
            new Service {Name =  "Linode",  HomePage="https://www.linode.com/"},
            new Service {Name =  "Mailchimp",  HomePage="https://mailchimp.com/"},
            new Service {Name =  "Azul dominicana",  HomePage="https://www.azul.com.do/Pages/es/default.aspx"},
            new Service {Name =  "Cardnet dominicana",  HomePage="https://www.cardnet.com.do/"},
            new Service {Name =  "Godaddy",  HomePage="https://www.godaddy.com/"},
            new Service {Name =  "Github", HomePage= "https://github.com/"},
            new Service {Name =  "Gitlab",  HomePage="https://about.gitlab.com/"},
            new Service {Name =  "Vercel",  HomePage="https://vercel.com/"},
            new Service {Name =  "Netifly",  HomePage="https://www.netlify.com/"},
            new Service {Name =  "Cloudflare",  HomePage="https://www.cloudflare.com/"},
            new Service {Name =  "Figma",  HomePage="https://www.figma.com/"},
            new Service {Name =  "Facebook Business",  HomePage="https://business.facebook.com/"},
            new Service {Name =  "Google Ads",  HomePage="https://ads.google.com/home/"},
            new Service {Name =  "Intercom",  HomePage="https://www.intercom.com/"}
        });
    }
}