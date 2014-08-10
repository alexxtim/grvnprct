using Autofac;
using Autofac.Configuration;
using GrosvenorPracticum_BL;

namespace GrosvenorPracticum
{
    internal class Bootstrapper
    {
        public static IContainer RegisterIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FoodServiceManager>().As<IFoodServiceManager>();
            builder.RegisterType<Console>().As<IConsole>();
            builder.RegisterModule(new ConfigurationSettingsReader());
            var container = builder.Build();
            return container;
        }
    }
}
