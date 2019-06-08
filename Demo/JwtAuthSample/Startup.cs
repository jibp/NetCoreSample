using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JwtAuthSample.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using JwtAuthSample.Store;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
//using JwtAuthSample.Filter;

namespace JwtAuthSample
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
        
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            var jwtSeetings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSeetings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               
            }).
            AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidIssuer = jwtSeetings.Issuser,
                    ValidAudience = jwtSeetings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSeetings.SecretKey))
                };

                //o.SecurityTokenValidators.Clear();
                //o.SecurityTokenValidators.Add(new MyTokenValidator());
                //o.Events = new JwtBearerEvents()
                //{
                //    OnMessageReceived = context => {
                //        var token = context.Request.Headers["mytoken"];
                //        context.Token = token.FirstOrDefault();
                //        return Task.CompletedTask;
                //    }
                //};


            });

            //添加Claim授权
            services.AddAuthorization(options => {
                options.AddPolicy("SuperAdminOnly", policy => { policy.RequireClaim("SuperAdminOnly"); });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "DemoJwt Apijdk"
                });

                // 为 Swagger JSON and UI设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
                swagger.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Jwt Bearer Authentication.",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                swagger.DocumentFilter<SecurityRequirementsDocumentFilter>();
            });
            var decryptStr = DesDecrypt(Configuration["ConnectionString"], "whp4-3hn8-sqoy19");
            services.AddDbContextPool<JwtDbContext>(options =>
            {
                 options.UseSqlServer("server=139.224.59.224,9999;uid=SaaSOp;pwd=sM1!2@3#;database=SupportManagementDB_20180719;");
                //services.AddDbContextPool<BloggingContext>(options => options.UseMySql("Server=localhost;Port=3306;Database=WebBloggingDB; User=root;Password=;"));
               // options.UseMySql("Server=localhost;Port=3306;Database=WebBloggingDB; User=root;Password=;");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JwtAuthSample.xml");
                c.RoutePrefix = string.Empty;
            });
        }

        public static string DesEncrypt(string input, string key)
        {

            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key);
            byte[] allKey = new byte[24];
            System.Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            System.Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Dispose();
            cTransform.Dispose();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DesDecrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key);
            byte[] allKey = new byte[24];
            System.Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            System.Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Dispose();
            cTransform.Dispose();
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
