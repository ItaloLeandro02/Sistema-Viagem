using System;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Testes
{
    public class DAO
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataDbContext(serviceProvider.GetRequiredService<DbContextOptions<DataDbContext>>()))
            {
                
            }
        }
    }
}