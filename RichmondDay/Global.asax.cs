using Autofac;
using Autofac.Integration.Mvc;
using RichmondDay.Concrete;
using RichmondDay.Context;
using RichmondDay.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RichmondDay
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacDIResolver();
        }
        private void AutofacDIResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<Info>().As<IInfo>();
            builder.RegisterType<RichmonddayDbContext>().As<IRichmonddayDbContext>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
