using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Conexão com o banco
            services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //Repositórios
            services.AddTransient<IVeiculoRepository, VeiculoRepository>();
            services.AddTransient<IMotoristaRepository, MotoristaRepository>();
            services.AddTransient<ICidadeIbgeRepository, CidadeIbgeRepository>();
            services.AddTransient<IViagemRepository, ViagemRepository>();
            services.AddTransient<IViagemDespesaRepository, ViagemDespesaRepository>();

           

        }
    }
}
