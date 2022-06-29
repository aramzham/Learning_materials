using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL_Api.GQL.MutationTypes;
using GraphQL_Api.GQL.QueryTypes;
using GraphQL_Api.GQL.SubscriptionTypes;
using GraphQL_Api.GQL.Types;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GraphQL_Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<PersonType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions(); // change to a persistence layer in production

            services.AddScoped<IMongoClient, MongoClient>(
                _ => new MongoClient(Configuration.GetConnectionString("Mongodb"))
                );

            //services.AddPooledDbContextFactory<DbContext>(x => x.); // use AddPooledDbContextFactory for this kind of queries, because dbcontext is not multithreaded out of the box
            //query{
            //    a: all{
            //        name
            //            passportNumber
            //    }
            //    b: all{
            //        name
            //            passportNumber
            //    }
            //    c: all{
            //        name
            //            passportNumber
            //    }
            //}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
