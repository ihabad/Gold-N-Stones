using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GoldAndSilver.Areas.Identity.IdentityHostingStartup))]
namespace GoldAndSilver.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}