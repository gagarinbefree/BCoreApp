using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using BCoreMvc.Models.ViewModels.Blog;
using System.Collections.Generic;
using System;
using BCoreDao;
using BCoreMvc.Models.Commands;

namespace BCoreMvc
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            _autoMapperConfig(services);

            services.AddSingleton<IConfiguration>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ClientId = "mvc",
                ClientSecret = "secret",

                ResponseType = "code id_token",
                Scope = { "BCoreIdentity", "offline_access" },

                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true
            });
            
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private void _autoMapperConfig(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<ICollection<Post>, UpdateViewModel>()
                    .ForMember(g => g.RecentPosts, o => o.MapFrom(c => c));

                config.CreateMap<ICollection<Post>, FeedViewModel>()
                    .ForMember(g => g.RecentPosts, o => o.MapFrom(c => c));

                config.CreateMap<ICollection<Post>, TopViewModel>()
                    .ForMember(g => g.RecentPosts, o => o.MapFrom(c => c));

                config.CreateMap<Post, PostViewModel>()
                    .ForMember(g => g.Parts, o => o.MapFrom(c => c.Parts))
                    .ForMember(g => g.Comments, o => o.MapFrom(c => c.Comments))
                    .ForMember(g => g.Hashes, o => o.MapFrom(c => c.PostHashes));

                config.CreateMap<Part, PartViewModel>();
                config.CreateMap<Comment, CommentViewModel>();
                config.CreateMap<PostHash, HashViewModel>()
                    .ForMember(g => g.Id, o => o.MapFrom(c => c.HashId))
                    .ForMember(g => g.Tag, o => o.MapFrom(c => c.Hash.Tag));

                config.CreateMap<WhatsNewViewModel, PartViewModel>()
                      .ForMember(g => g.Value, o => o.MapFrom(c => c.GetPartValue()))
                      .ForMember(g => g.PartType, o => o.MapFrom(c => c.GetPartTypeName()))
                      .ForMember(g => g.CreatedOn, o => o.MapFrom(c => DateTime.Now));

                //config.CreateMap<ApplicationUser, UserViewModel>();

                config.CreateMap<UpdateViewModel, Post>()
                    .ForMember(g => g.CreatedOn, o => o.MapFrom(c => c.PreviewPost.CreatedOn))
                    .ForMember(g => g.Parts, o => o.MapFrom(c => c.PreviewPost.Parts));

                config.CreateMap<PartViewModel, Part>();

                config.CreateMap<WhatsThinkViewModel, Comment>()
                    .ForMember(g => g.CreatedOn, o => o.MapFrom(c => DateTime.Now));
            });
        }
    }
}