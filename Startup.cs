// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Microsoft.Bot.Schema;
using YunnyEchoBot.Bots;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;

namespace YunnyEchoBot
{
    public class Startup
    {
        private readonly string cosmosDBEndpointUrl;
        private readonly string cosmosDBPrimaryKey;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            cosmosDBEndpointUrl = configuration["CosmosDBEndpointUrl"];
            cosmosDBPrimaryKey = configuration["CosmosDBPrimaryKey"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            services.AddDataProtection();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the service client that encapsulates the endpoint and credentials and
            // connection policy used to access the Azure Cosmos DB Service.
            services.AddSingleton<IDocumentClient>(x => new DocumentClient(new Uri(cosmosDBEndpointUrl), cosmosDBPrimaryKey));

            // Add a singleton key that will be used for the encryption and decryption of the channelID.
            services.AddSingleton<String>(new String(""));

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, EchoBot>();
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
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
