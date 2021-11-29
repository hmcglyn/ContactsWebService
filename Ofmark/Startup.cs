using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ofmark.Business.Abstract;
using Ofmark.Business.Services;
using Ofmark.Entities;
using Ofmark.Models.Dtos;

namespace Ofmark
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
            services.AddControllers();
            services.AddTransient<IContactService, ContactManager>();
            services.AddTransient<IEmailService, EmailManager>();
            services.AddTransient<IPhoneNumberService, PhoneNumberManager>();
            services.AddTransient<IAddressService, PublicAddressManager>();
            // AutoMapper Configurations
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactDto, Contact>();                    
                cfg.CreateMap<Contact, ContactDto>();                   
                cfg.CreateMap<ContactListDto, Contact>();
                cfg.CreateMap<Contact, ContactListDto>();
                cfg.CreateMap<EmailAddresses, EmailDto>();
                cfg.CreateMap<EmailDto, EmailAddresses>();
                cfg.CreateMap<PhoneNumbers, PhoneNumberDto>();
                cfg.CreateMap<PhoneNumberDto, PhoneNumbers>();
                cfg.CreateMap<PublicAddresses, PublicAddressDto>();
                cfg.CreateMap<PublicAddressDto, PublicAddresses>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
