using Microsoft.Data.SqlClient;

using SantaShop.Core.Interfaces;
using SantaShop.ConsV3.MenusUI;

using System;
using System.Text;
using SantaShop.Core.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SantaShop.ConsV2
{
    class Program
    {
        static void Main(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                .AddEnvironmentVariables()
                                .AddJsonFile("appsettings.json", false)
                                .Build();

            Console.OutputEncoding = Encoding.UTF8;

            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("SantaBD"));


            BehaviorService behaviorService = new BehaviorService();
            behaviorService.DbConnection = connection;

            PresentsService presentsService = new PresentsService();
            presentsService.DbConnection = connection;


            ChildrenService childrenService = new ChildrenService();
            childrenService.DbConnection = connection;



            MenuPresents menuPresents = new MenuPresents(presentsService);
            MenuBehaviors menuBehaviors = new MenuBehaviors(behaviorService);
            


            MainMenu mainMenu = new MainMenu(   presentsMenu: menuPresents,
                                                behaviorMenu: menuBehaviors,
                                                childremMenu: new MenuChildren(childrenService, menuPresents, menuBehaviors)
                                            );


            mainMenu.OnTerminarAplicacao += MainMenu_OnTerminarAplicacao;


            while (true)
            {
                mainMenu.MenuPrincipal();
            }



        }

        private static void MainMenu_OnTerminarAplicacao(object sender, EventArgs e)
        {
            Console.WriteLine("\n.FIM.\n");
            Environment.Exit(0);
        }
    }
}
