﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //Repositórios
            services.AddTransient<IVeiculoRepository, VeiculoRepository>();
            services.AddTransient<IMotoristaRepository, MotoristaRepository>();
            services.AddTransient<ICidadeIbgeRepository, CidadeIbgeRepository>();
            services.AddTransient<IViagemRepository, ViagemRepository>();
            //services.AddTransient<IViagemDespesaRepository, ViagemDespesaRepository>();

            //CORS
            services.AddCors(o => o.AddPolicy("CorsApi", builder => {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            })); 

            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options => 
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsApi"); 
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseResponseCompression();
        }
    }
}
