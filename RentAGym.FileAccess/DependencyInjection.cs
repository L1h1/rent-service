using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RentAGym.Application.Interfaces;
using RentAGym.FileAccess.Services;

namespace RentAGym.FileAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileAccess(this IServiceCollection services)
        {
            services
                .AddScoped<IFileService, LocalFileService>();


            return services;
        }
    }
}
