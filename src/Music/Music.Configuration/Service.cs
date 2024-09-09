using Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Music.Application;
using Music.Contract.Contracts;
using Music.Domain.Entities.ArtistAgg;
using Music.Domain.Entities.BandAgg;
using Music.EfCore;
using Music.EfCore.Repositories;

namespace Music.Configuration
{
    public static class Service 
    {
        public static void AddMusicServices(this IServiceCollection services, string cs)
        {
            // Repositories
            services.AddTransient<IBandRepository, BandRepository>();
            services.AddTransient<IArtistRepository, ArtistRepository>();


            // Applications
            services.AddTransient<IBandApplication, BandApplication>();
            services.AddTransient<IArtistApplication,ArtistApplication>();


            services.AddDbContext<MusicDbContext>(e => e.UseSqlServer(cs));
        }
    }
}
