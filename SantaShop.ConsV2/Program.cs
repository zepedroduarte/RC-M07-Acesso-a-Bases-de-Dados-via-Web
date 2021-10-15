using Microsoft.Data.SqlClient;

using SantaShop.ConsV2.Services;
using SantaShop.ConsV2.MenusUI;

using System;
using System.Text;

namespace SantaShop.ConsV2
{
    class Program
    {
        public static string ConnectionString = "" +
            "Server=tcp:pmwebsql.database.windows.net,1433;" +
            "Database=SantaShop;" +
            "User ID=tgpsi;" +
            "Password=edigital@2021;" +
            "Trusted_Connection=False;" +
            "Encrypt=True;" +
            "Application Name=SantaShop.Consola;" +
            "MultipleActiveResultSets=True;";



        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            SqlConnection connection = new SqlConnection(ConnectionString);


            BehaviorService behaviorService = new BehaviorService(connection);
            PresentsService presentsService = new PresentsService(connection);
            ChildrenService childrenService= new ChildrenService(connection);


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
