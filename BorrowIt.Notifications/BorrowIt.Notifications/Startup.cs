using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;
using BorrowIt.Common.Extensions;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Common.Infrastructure.Implementations;
using BorrowIt.Common.Infrastructure.IoC;
using BorrowIt.Common.Mongo.IoC;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.Common.Rabbit.IoC;
using BorrowIt.Notifications.Domain.Users;
using BorrowIt.Notifications.Inbound.Messages;
using BorrowIt.Notifications.Repositories;
using BorrowIt.Notifications.Services;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace BorrowIt.Notifications
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
            services.AddSignalR();
            services.AddCors();
            services.AddHttpClient<PushServiceClient>();
            services.Configure<PushNotificationsOptions>(Configuration.GetSection("PushNotifications"));

            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RawRabbitModule(Configuration));
            builder.RegisterModule(new MongoDbModule(Configuration, "mongoDb"));
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRepositories<UserRepository>()
                .AddGenericRepository(typeof(GenericMongoRepository<,>));
            builder.AddSerilog();
            builder.AddServices<IUsersService>();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.Register(ctx =>
            {
                var assemblies = new List<Assembly>
                {
                    Assembly.GetEntryAssembly()
                };

                var mapperConfig = new MapperConfiguration(x =>
                {
                    x.DisableConstructorMapping();
                    x.AddProfiles(assemblies);
                });


                return mapperConfig.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterAssemblyTypes(typeof(IUserFactory).Assembly).Where(x => x.Name.EndsWith("Factory"))
                .AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseApiExceptionMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitMq()
                .SubscribeMessage<ToDoListHourLeftEvent>()
                .SubscribeMessage<UserChangedEvent>()
                .SubscribeMessage<UserRemovedEvent>();
        }
    }
}
