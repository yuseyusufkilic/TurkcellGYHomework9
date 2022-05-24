using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RetroShirt.Business.Abstract;
using RetroShirt.Business.Concrete;
using RetroShirt.Business.MapperProfile;
using RetroShirtDAL.Data;
using RetroShirtDAL.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Http;


//api datayý baþka bir apiden çekiyor. microservice bu iþte.
//rest api istemci ile ilgili hiçbir þeyi tutmaz. state management de yok. ( query string,session,cookie )
//basic auth. (btoa) riskli. çözülüyor sýkýntý bu .
//response caching belli actionun çýktýsýný cacheliyor.

namespace RetroShirt.API
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
            var logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Information().MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            services.AddLogging(x=>
            {
                x.ClearProviders();
                x.AddSerilog(logger);
                
                
            });

            services.AddControllers();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RetroShirt.API", Version = "v1" });
            });
            services.AddMemoryCache();
            
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryTeamService, CategoryTeamService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();

            services.AddScoped<ITeamRepository, EFTeamRepository>();
            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<ICategoryRepository, EFCategoryRepository>();
            services.AddScoped<ICategoryTeamRepository, EFCategoryTeamRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();

            var connectionString = Configuration.GetConnectionString("db");
            var connectionForCache = Configuration.GetConnectionString("db2");
            services.AddDbContext<RetroShirtDBContext>(opt => opt.UseSqlServer(connectionString));

            services.AddAutoMapper(typeof(MapProfile));

            services.AddCors(opt => opt.AddPolicy("Allow",builder => // allow bizim koydugumuz poliçe adý.
            {
                 builder.AllowAnyOrigin();
                 builder.AllowAnyMethod(); // put post hepsine izin ver dedik.
                builder.AllowAnyHeader(); // auth. iþlemleri için header'a da izin verdik.
                // istek göndermeye izin vermek. kimler gönderebilir vs. port http falan origin. herhangiyi kabul et dedik biz.
            }));

            
            //burda yapýcaðýmýz config onay için jwt.

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret key generating here."));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.SaveToken = true;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateActor = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,

                            ValidIssuer = "Turkcell",
                            ValidAudience = "Turkcell",
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,

                        };

                    });

            services.AddDistributedMemoryCache();
            services.AddDistributedSqlServerCache(opt => {
                opt.ConnectionString = connectionForCache;
                opt.SchemaName = "dbo";
                opt.TableName = "TestCache";
                });
            
            //var config = new HttpConfiguration();
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
            //                                Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime xapp)
        {
            /* bu terminal middleware , sonrasýnda context varmýþ yokmuþ sallamaz çalýþýr baþka da bi þey çalýþmaz.*/
            //run ile kullanýlýnca tm oluyor.
            //app.Run(async x => await x.Response.WriteAsync("This is a terminal middleware."));

            
            
            app.Map("/test", xapp => xapp.Run(async x =>
            {
                var queryIsExist = x.Request.Query.ContainsKey("id");
                if (queryIsExist)
                {
                    var id = int.Parse(x.Request.Query["id"]);
                    var scope = app.ApplicationServices.CreateScope();
                    var productService= scope.ServiceProvider.GetRequiredService<IProductService>();
                    if (await productService.ProductIsExistWithId(id))
                    {
                        await x.Response.WriteAsync($"Product found with id:{id} ");
                    }
                    else
                    {
                        await x.Response.WriteAsync($"Product can not found with id:{id} ");
                    }

                }

                else
                {
                    await x.Response.WriteAsync($"There is no id.");
                }
                
            }
            
             ));

            
            
            // her request buna uðrýycak. kullandýgýmýz http metodu hangisi ve json'u var mý onu döndürüyo bu yazdýklarým.

            //app.Use(async (ctx, next) =>
            //{
            //    Console.WriteLine(ctx.Request.Method);
            //    Console.WriteLine(ctx.Request.HasJsonContentType());

            //    await next.Invoke();
            //});

            
            //burda aslýnda filtre gibi bi görev yapýyor. ama projede yer alan tüm requestler buraya uðruyor fark bu. attribute gibi tek tek yazmýyoruz.
            // bu middleware json token hatasý verdirtiyo. ama kendi iþlevini yerine getiriyo.


            //app.Use(async (ctx, nxt) =>
            //{
            //    var isJsonContent = ctx.Request.HasJsonContentType();

            //    if (isJsonContent)
            //    {
            //        object body = await ctx.Request.ReadFromJsonAsync<object>();
            //        dynamic type = JsonConvert.DeserializeObject<dynamic>(body.ToString());
            //        Console.WriteLine(type.name);
            //    }

            //    await nxt.Invoke(); // üstündeki iþlemleri yapýp diðer middleware'ye paslýyo invoke, sonra geri dönüyo iþlem varsa devam ediyo.


            //});

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RetroShirt.API v1"));
                
            }
            
            app.UseHttpsRedirection();
            //cache'e neler ekliycen byte array olcak db'de saklýycagýmýz icin.
            xapp.ApplicationStarted.Register(() =>
            {
                
                var scope = app.ApplicationServices.CreateScope();
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                
                var json = JsonConvert.SerializeObject(userService.GetAllUsers());
                //var encodedJsonProducts = Encoding.UTF8.GetBytes(json); // json'u byte arraya çevirdik serileþtirip. cache set için lazýmdý.
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
                app.ApplicationServices.GetService<IDistributedCache>().SetString("myUsers", json, options); //cachei set ediyoz.
                //tekrar stringe çevirmekle ugrasmamak icin direk json verdik cache'e.
            });
            app.UseRouting();
            app.UseCors("Allow");
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
