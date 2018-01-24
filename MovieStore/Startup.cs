using Microsoft.Owin;
using MovieStore.Models;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(MovieStore.Startup))]
namespace MovieStore
{
    public partial class Startup
    {


        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }


    }
}
