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
            new Currency { Id = 1, Name = "Dominican Peso", ShortCode= "DOP",Symbol = "RD$" },
            new Currency { Id = 2, Name = "US Dollar", ShortCode = "USD", Symbol = "$" },
            new Currency { Id = 3, Name = "Euro", ShortCode = "EUR", Symbol = "€" },
            new Currency { Id = 4, Name = "Pound Sterling", ShortCode = "GBP", Symbol = "£" },
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new[]
        {
            new ExpenseCategory{ Id = 1, Name = "Customer Support" },
            new ExpenseCategory{ Id = 2, Name = "Development / R&D" },
            new ExpenseCategory{ Id = 3, Name = "Marketing" },
            new ExpenseCategory{ Id = 4, Name = "Ops" },
            new ExpenseCategory{ Id = 5, Name = "Web Hosting" },
            new ExpenseCategory{ Id = 6, Name = "Productivity" }
        });

        modelBuilder.Entity<Service>().HasData(new[]
        {
            new Service {Id = 1, Name = "MongoDB", HomePage="https://www.mongodb.com/atlas" },
            new Service {Id = 2, Name = "Monday", HomePage="https://monday.com/"},
            new Service {Id = 3, Name = "CircleCI",  HomePage="https://circleci.com/"},
            new Service {Id = 4, Name = "Filestage",  HomePage="https://filestage.io/"},
            new Service {Id = 5, Name = "Hubspot",  HomePage="https://www.hubspot.com/"},
            new Service {Id = 6, Name = "Balsamiq", HomePage= "https://balsamiq.com/"},
            new Service {Id = 7, Name = "Adobe", HomePage= "https://www.adobe.com/"},
            new Service {Id = 8, Name = "Abstract",  HomePage="https://www.abstract.com/"},
            new Service {Id = 9, Name = "Algolia",  HomePage="https://www.algolia.com/"},
            new Service {Id = 10, Name =  "AWS", HomePage= "https://aws.amazon.com/"},
            new Service {Id = 11, Name =  "Hetzner",  HomePage="https://www.hetzner.com/cloud"},
            new Service {Id = 12, Name =  "Google Cloud",  HomePage="https://cloud.google.com/"},
            new Service {Id = 13, Name =  "Azure",  HomePage="https://azure.microsoft.com/en-us/"},
            new Service {Id = 14, Name =  "Vultr",  HomePage="https://www.vultr.com/"},
            new Service {Id = 15, Name =  "PayPal",  HomePage="https://www.paypal.com/"},
            new Service {Id = 16, Name =  "Digital Ocean",  HomePage="https://www.digitalocean.com/"},
            new Service {Id = 17, Name =  "Netcup",  HomePage="https://www.netcup.eu/"},
            new Service {Id = 18, Name =  "Stripe",  HomePage="https://stripe.com/"},
            new Service {Id = 19, Name =  "Square",  HomePage="https://squareup.com/"},
            new Service {Id = 20, Name =  "Coinbase Commerce",  HomePage="https://commerce.coinbase.com/"},
            new Service {Id = 21, Name =  "Linode",  HomePage="https://www.linode.com/"},
            new Service {Id = 22, Name =  "Mailchimp",  HomePage="https://mailchimp.com/"},
            new Service {Id = 23, Name =  "Azul dominicana",  HomePage="https://www.azul.com.do/Pages/es/default.aspx"},
            new Service {Id = 24, Name =  "Cardnet dominicana",  HomePage="https://www.cardnet.com.do/"},
            new Service {Id = 25, Name =  "Godaddy",  HomePage="https://www.godaddy.com/"},
            new Service {Id = 26, Name =  "Github", HomePage= "https://github.com/"},
            new Service {Id = 27, Name =  "Gitlab",  HomePage="https://about.gitlab.com/"},
            new Service {Id = 28, Name =  "Vercel",  HomePage="https://vercel.com/"},
            new Service {Id = 29, Name =  "Netifly",  HomePage="https://www.netlify.com/"},
            new Service {Id = 30, Name =  "Cloudflare",  HomePage="https://www.cloudflare.com/"},
            new Service {Id = 31, Name =  "Figma",  HomePage="https://www.figma.com/"},
            new Service {Id = 32, Name =  "Facebook Business",  HomePage="https://business.facebook.com/"},
            new Service {Id = 33, Name =  "Google Ads",  HomePage="https://ads.google.com/home/"},
            new Service {Id = 34, Name =  "Intercom",  HomePage="https://www.intercom.com/"}
        });
    }
}