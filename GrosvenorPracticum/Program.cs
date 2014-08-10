using Autofac;
using GrosvenorPracticum_BL;

namespace GrosvenorPracticum
{
    public class Program
    {
        public static IContainer Container { private get; set; }

        public static void Main(string[] args)
        {
            if (Container == null)
            {
                Container = Bootstrapper.RegisterIoC();
            }

            var console = Container.Resolve<IConsole>();
            var foodServiceManager = Container.Resolve<IFoodServiceManager>();

            var inputString = console.ReadLine();
            console.WriteLine(foodServiceManager.GetDishes(inputString));

            console.ReadLine();
        }
    }
}