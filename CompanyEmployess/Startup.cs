using AutoMapper;
using CompanyEmployess.Extensions;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;

namespace CompanyEmployess
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.AddControllers();
            services.ConfigureIRepManagger();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers(config => {

                config.ReturnHttpNotAcceptable = true;
                config.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters().AddCustomCSVFormatter();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Company, CompanyDto>()
                    .ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
               //???????????????????
                CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
                CreateMap<Engine, EngineDto>();
                CreateMap<Car, CarDto>();
                CreateMap<Employee, EmployeeDto>();
                CreateMap<CompanyForCreationDto, Company>();
                CreateMap<EmployeeForCreationDto, Employee>();
                CreateMap<EmployeeForUpdateDto, Employee>();
                CreateMap<CompanyForUpdateDto, Company>();               
                CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
                CreateMap<EngineForCreationDto, Engine>();
                CreateMap<CarForCreationDto, Car>();
                CreateMap<CarForUpdateDto, Car>();
                CreateMap<EngineForUpdateDto, Engine>();
                CreateMap<CarForUpdateDto, Car>().ReverseMap();

            }
        }
    }
}
