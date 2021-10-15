using Dapper;
using Dapper.Contrib.Extensions;

using Microsoft.Data.SqlClient;

using SantaShop.Cons.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace SantaShop.Cons
{

    class Program
    {
        static string ConnectionString = "" +
            "Server=tcp:pmwebsql.database.windows.net,1433;" +
            "Database=SantaShop;" +
            "User ID=tgpsi;" +
            "Password=edigital@2021;" +
            "Trusted_Connection=False;" +
            "Encrypt=True;" +
            "Application Name=SantaShop.Consola;" +
            "MultipleActiveResultSets=True;";


        static string selectCriancas = @"
                            select	
                                [ChildID]
                                ,[ChildName]
                                ,[YearOfBirth]
                                ,behave.[BehaviorID]
                                ,[BehaviorDescription]
                                ,[IsEligibleForPresent]
                                ,present.[PresentID]
                                ,[PresentName]
                            from
                                SantaShop.Children as child
                            inner join
                                SantaShop.Behaviors as behave on behave.BehaviorID = child.BehaviorID
                            inner join 
                                SantaShop.Presents as present on present.PresentID = child.PresentID";


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            SqlConnection connection = new SqlConnection(ConnectionString);


            while (true)
            {
                MenuTabelas();
            }
        }

        static void MenuTabelas()
        {
            Console.Clear();
            Console.WriteLine("*** TABELAS ***");
            Console.WriteLine("1 - Presentes");
            Console.WriteLine("2 - Comportamentos");
            Console.WriteLine("3 - Crianças");
            Console.WriteLine("Qualquer tecla para sair.");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    MenuPresentes();
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    MenuComportamentos();
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    MenuCriancas();
                    break;

                default:
                    Environment.Exit(0);
                    break;
            }



        }

        private static void OpcoesTabelas()
        {
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Procurar por pesquisa");
            Console.WriteLine("3 - Criar novo");
            Console.WriteLine("4 - Actualizar");
            Console.WriteLine("5 - Eliminar");
            Console.WriteLine("Qualquer tecla para voltar ao menu principal.");
        }

        private static void MenuCriancas()
        {
            Console.Clear();
            Console.WriteLine("*** TABELA: CRIANÇAS ***");
            OpcoesTabelas();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    Criancas_ListarTodos();
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    Criancas_ProcurarPorPesquisa();
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    Criancas_Adicionar();
                    break;


                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    Criancas_Actualizar();
                    break;


                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    Criancas_Eliminar();
                    break;

                default:
                    MenuTabelas();
                    break;
            }

        }

        private static void Criancas_Eliminar()
        {
            Console.WriteLine("\n*** Eliminar criança ***");
            Criancas_ListarTodos(true);

            SqlConnection con = new SqlConnection(ConnectionString);

            Console.Write("Qual o ID da criança a eliminar? > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("\nSem critério de pesquisa!");

            }

            Console.WriteLine("\n> Aguarde...");

           Child crianca = con.Get<Child>(readKeyBoard);

            if (crianca == null)
            {
                Console.WriteLine("\nNão existe presente com esse ID!");
            }
            else
            {
                bool deleted = con.Delete<Child>(crianca);

                if (deleted)
                {
                    Console.WriteLine("\nCriança eliminada com sucesso!");
                    AguardaQualquerTecla();
                    MenuCriancas();
                }
                else
                {
                    Console.WriteLine("\nNão foi possivel eliminar. Notifique o suporte!");
                    AguardaQualquerTecla();
                    MenuCriancas();
                }

            }
        }

        private static void Criancas_Actualizar()
        {
            Console.WriteLine("\n*** Actualizar crianças ***");
            Criancas_ListarTodos(true);

            SqlConnection con = new SqlConnection(ConnectionString);

            Console.Write("Qual o ID da criança a alterar? > ");

            string readKeyBoard = Console.ReadLine();

            Console.WriteLine("\n> Aguarde...");

            Child child = con.Get<Child>(readKeyBoard);

            if (child == null)
            {
                Console.WriteLine("\nNão existe criança com esse ID!");
            }
            else
            {
                Console.Write($"Qual o nome da criança? (Actual: {child.ChildName} - deixar em vazio para manter) > ");


                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {
                    child.ChildName = readKeyBoard;

                }

                Console.Write($"Qual o ano de nascimento da criança? (Actual: {child.YearOfBirth} - deixar em vazio para manter) > ");

                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {
                    int yearOfBirth;
                    int.TryParse(readKeyBoard, out yearOfBirth);
                    child.YearOfBirth = yearOfBirth;
                }

                Comportamentos_ListarTodos(true);

                Console.Write($"Selecione o comportamento indicado o ID? (Actual: {child.BehaviorID} - deixar em vazio para manter) > ");

                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {
                    int behaviorID;
                    int.TryParse(readKeyBoard, out behaviorID);
                    child.BehaviorID = behaviorID;
                }

                Presentes_ListarTodos(true);

                Console.Write($"Selecione o presente indicado o ID? (Actual: {child.ChildID} - deixar em vazio para manter) > ");
                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {
                    int presentID;
                    int.TryParse(readKeyBoard, out presentID);
                    child.PresentID = presentID;
                }


                Console.WriteLine("\n> Aguarde...");

                bool wasUpdated = con.Update<Child>(child);

                if (!wasUpdated)
                {
                    Console.WriteLine("\nNão foi possível actualizar a criança. Contacte o suporte!");
                    AguardaQualquerTecla();
                    MenuCriancas();
                }
                else
                {
                    Console.WriteLine($"\nCriança actualizada com sucesso!");
                    AguardaQualquerTecla();
                    MenuCriancas();
                }

            }
        }

        private static void Criancas_ProcurarPorPesquisa()
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            Console.WriteLine("\n*** Lista de Crianças ***");
            Console.Write("\nNome da criança? > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("Sem critério de pesquisa!");
                AguardaQualquerTecla();
                MenuCriancas();

            }

            string sql = selectCriancas +
                        @" where child.ChildName like @ChildName";


            readKeyBoard = $"%{readKeyBoard}%";


            Console.WriteLine("> Aguarde...");
            var listAll = con.Query(sql, new { ChildName = readKeyBoard }).AsList();

            if (listAll is null || listAll.Count == 0)
            {
                 MensagemSemRegistos();
                MenuCriancas();
            }
            else
            {
                Console.WriteLine("\n*** Lista de Crianças ***");

                Criancas_MostrarLista(listAll);

                AguardaQualquerTecla();
                MenuCriancas();
            }
        }

        private static void Criancas_ListarTodos(bool soListar=false)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            Console.WriteLine("> Aguarde...");
            var listAll = con.Query(selectCriancas).AsList();

            if (listAll is null)
            {
                 MensagemSemRegistos();

                if(!soListar)
                    MenuCriancas();
            }
            else
            {
                Console.WriteLine("\n*** Lista de Crianças ***");

                Criancas_MostrarLista(listAll);

                if(!soListar)
                {
                    AguardaQualquerTecla();
                    MenuCriancas();

                }
            }


        }

        private static void Criancas_MostrarLista(List<dynamic> listAll)
        {

            string message = $"{"ID",-5}" +
                $"\t{"Nome",-30}" +
                $"\t{"Idade",-5}" +
                $"\t{"Comportamento",-10}" +
                $"\t{"Tem Presente?",-15}" +
                $"\t{"Presente",-30}";

            Console.WriteLine(message);

            string strFill = new string('-', message.Length);
            Console.WriteLine(strFill);

            foreach (var item in listAll)
            {
                Console.WriteLine($"{item.ChildID,-5}" +
                    $"\t{item.ChildName,-30}" +
                    $"\t{(DateTime.Now.Year - item.YearOfBirth),-5}" +
                    $"\t{item.BehaviorDescription,-10}" +
                    $"\t{(item.IsEligibleForPresent ? "SIM" : "Não"),-15}" +
                    $"\t{item.PresentName,-30}");
            }
        }

        private static void MensagemSemRegistos()
        {
            Console.WriteLine("Não existem registos!");
            AguardaQualquerTecla();
        }

        private static void Criancas_Adicionar()
        {
            Console.WriteLine("\n*** Adicionar crianças ***");

            SqlConnection con = new SqlConnection(ConnectionString);

            Child crianca = new Child();


            Console.Write("Qual o nome da criança > ");

            crianca.ChildName = Console.ReadLine();

            Console.Write("Qual o ano de nascimento da criança? > ");

            string readKeyBoard = Console.ReadLine();

            int yearOfBirth;

            bool isYearOfBirthInt = int.TryParse(readKeyBoard, out yearOfBirth);

            if (!isYearOfBirthInt)
            {
                Console.WriteLine("\nO ano de nascimento é inválido! Tem de ser numero!");
                return;
            }

            crianca.YearOfBirth = yearOfBirth;

            Comportamentos_ListarTodos(true);

            Console.Write("Selecione o comportamento indicado o ID? > ");

            readKeyBoard = Console.ReadLine();

            int behaviorID;
            int.TryParse(readKeyBoard, out behaviorID);

            Presentes_ListarTodos(true);

            Console.Write("Selecione o presente indicado o ID? > ");
            readKeyBoard = Console.ReadLine();

            int presentID;

            int.TryParse(readKeyBoard, out presentID);


            Console.WriteLine("\n> Aguarde...");

            crianca.PresentID = presentID;
            crianca.BehaviorID = behaviorID;

            long newID = con.Insert<Child>(crianca);

            if (newID <= 0)
            {
                Console.WriteLine("\nNão foi possível adicionar a criança. Contacte o suporte!");
                AguardaQualquerTecla();
                MenuCriancas();
            }
            else
            {
                Console.WriteLine($"\nCriança adicionada com o ID {newID}");
                AguardaQualquerTecla();
                MenuCriancas();
            }
        }


        private static void MenuComportamentos()
        {
            Console.Clear();
            Console.WriteLine("*** TABELA: COMPORTAMENTOS ***");
            OpcoesTabelas();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    Comportamentos_ListarTodos();
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    Comportamentos_ProcurarPorPesquisa();
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    Comportamentos_Adicionar();
                    break;


                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    Comportamentos_Actualizar();
                    break;


                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    Comportamentos_Eliminar();
                    break;

                default:
                    MenuTabelas();
                    break;
            }

        }


        private static void Comportamentos_ListarTodos(bool soLista = false)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            Console.WriteLine("> Aguarde...");
            var listAll = con.GetAll<Behavior>().AsList();

            if (listAll is null)
            {

                 MensagemSemRegistos();

                if (!soLista)
                    MenuCriancas();
            }
            else
            {
                Console.WriteLine("\n*** Lista de Comportamentos ***");
                Comportamentos_MostrarLista(listAll);

                if (!soLista)
                {
                    AguardaQualquerTecla();
                    MenuComportamentos();
                }

            }
        }

        private static void Comportamentos_MostrarLista(List<Behavior> listAll)
        {
            string message = $"{"ID",-5}" +
                $"\t{"Comportamento",-30}" +
                $"\t{"Tem Presente?",-15}";

            Console.WriteLine(message);

            string strFill = new string('-', message.Length+5);
            Console.WriteLine(strFill);

            foreach (var item in listAll)
            {
                Console.WriteLine($"{item.BehaviorID,-5}" +
                    $"\t{item.BehaviorDescription,-30}" +
                    $"\t{(item.IsEligibleForPresent ? "SIM" : "Não"),-15}");
            }
        }

        private static void Comportamentos_ProcurarPorPesquisa()
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            Console.WriteLine("\n*** Lista de Comportamentos ***");

            Console.Write("\nQual o comportamento a pesquisar? > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("\nSem critério de pesquisa!");

            }

            Console.WriteLine("> Aguarde...");

            var queryParam = new
            {
                pBehaviorDescription = $"%{readKeyBoard}%"
            };

            var listAll = con.Query<Behavior>("select * from SantaShop.Behaviors where BehaviorDescription like @pBehaviorDescription", queryParam).AsList();


            if (listAll is null)
            {

                 MensagemSemRegistos();
                MenuComportamentos();
            }
            else
            {
                Comportamentos_MostrarLista(listAll);
                AguardaQualquerTecla();
                MenuComportamentos();
            }
        }

        private static void AguardaQualquerTecla()
        {
            Console.WriteLine("\nPrima qualquer tecla para continuar...");
            Console.ReadKey(true);
        }

        private static void Comportamentos_Adicionar()
        {
            Console.WriteLine("\n*** Adicionar comportamentos ***");

            SqlConnection con = new SqlConnection(ConnectionString);

            Behavior behavior = new Behavior();

            Console.Write($"Qual descrição do comportamento? > ");

            string readKeyBoard = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(readKeyBoard))
            {
                behavior.BehaviorDescription = readKeyBoard;
            }

            Console.Write($"O comportamento dá direito a presente (S/N)> ");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.S:
                    behavior.IsEligibleForPresent = true;
                    break;

                default:
                    behavior.IsEligibleForPresent = false;
                    break;

            }


            Console.WriteLine("> Aguarde...");

            long newID = con.Insert<Behavior>(behavior);

            if (newID <= 0)
            {
                Console.WriteLine("\nNão foi possível adicionar o comportamento. Contacte o suporte!");
                AguardaQualquerTecla();
                MenuComportamentos();
            }
            else
            {
                Console.WriteLine($"\nComportamento adicionado com o ID {newID} com sucesso!");
                AguardaQualquerTecla();
                MenuComportamentos();
            }
        }
    
        private static void Comportamentos_Actualizar()
        {
            Console.WriteLine("\n*** Actualizar comportamentos ***");
            Comportamentos_ListarTodos(true);

            SqlConnection con = new SqlConnection(ConnectionString);

            Console.Write("Qual o ID do comportamento a alterar? > ");

            string readKeyBoard = Console.ReadLine();

            Console.WriteLine("\n> Aguarde...");

            Behavior behavior = con.Get<Behavior>(readKeyBoard);

            if (behavior == null)
            {
                Console.WriteLine("\nNão existe comportamento com esse ID!");
            }
            else
            {
                Console.Write($"Qual descrição do comportamento? (Actual: {behavior.BehaviorDescription} - deixar em vazio para manter) > ");


                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {
                    behavior.BehaviorDescription = readKeyBoard;
                }

                Console.Write($"\nO comportamento dá direito a presente (S/N) (Actual {(behavior.IsEligibleForPresent ? "SIM":"NÃO")} > ");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.S:
                        behavior.IsEligibleForPresent = true;
                        break;

                    case ConsoleKey.N:
                        behavior.IsEligibleForPresent = false;
                        break;

                }


                Console.WriteLine("> Aguarde...");

                bool wasUpdated = con.Update<Behavior>(behavior);

                if (!wasUpdated)
                {
                    Console.WriteLine("\nNão foi possível actualizar o comportamento. Contacte o suporte!");
                    AguardaQualquerTecla();
                    MenuComportamentos();
                }
                else
                {
                    Console.WriteLine($"\nComportamento actualizado com sucesso!");
                    AguardaQualquerTecla();
                    MenuComportamentos();
                }

            }
        }

        private static void Comportamentos_Eliminar()
        {
            Console.WriteLine("\n*** Eliminar comportamentos ***");
            Comportamentos_ListarTodos(true);

            SqlConnection con = new SqlConnection(ConnectionString);

            Console.Write("Qual o ID do comportamento a eliminar? > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("Sem critério de pesquisa!");

            }

            Console.WriteLine("\n> Aguarde...");

            Child behavior = con.Get<Child>(readKeyBoard);

            if (behavior == null)
            {
                Console.WriteLine("\nNão existe comportamento com esse ID!");
            }
            else
            {
                bool deleted = con.Delete<Child>(behavior);

                if (deleted)
                {
                    Console.WriteLine("\nComportamento eliminado com sucesso!");
                    AguardaQualquerTecla();
                    MenuPresentes();
                }
                else
                {
                    Console.WriteLine("\nNão foi possivel eliminar. Notifique o suporte!");
                    AguardaQualquerTecla();
                    MenuPresentes();
                }

            }
        }



        private static void MenuPresentes()
        {
            Console.Clear();
            Console.WriteLine("*** TABELA: PRESENTES ***");
            OpcoesTabelas();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    Presentes_ListarTodos();
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    Presentes_ProcurarPorPesquisa();
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    Presentes_Adicionar();
                    break;


                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    Presentes_Actualizar();
                    break;


                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    Presentes_Eliminar();
                    break;

                default:
                    MenuTabelas();
                    break;
            }


        }

        private static void Presentes_ListarTodos(bool soListar = false)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            Console.WriteLine("\n> Aguarde...");
            var listAll = con.GetAll<Present>().AsList();

            if (listAll is null)
            {
                 MensagemSemRegistos();
                if (!soListar) 
                    MenuPresentes();
            }
            else
            {
                Console.WriteLine("\n*** Lista de Presentes ***");

                Presentes_MostrarLista(listAll);

                if (!soListar)
                {
                    AguardaQualquerTecla();
                    MenuPresentes();
                }

            }
        }

        private static void Presentes_MostrarLista(List<Present> listAll)
        {
            string message = $"{"ID",-5}" +
                $"\t{"Presente",-30}" +
                $"\t{"Stock",-5}";

            Console.WriteLine(message);

            string strFill = new string('-', message.Length + 5);
            Console.WriteLine(strFill);

            foreach (var item in listAll)
            {
                Console.WriteLine($"{item.PresentID,-5}" +
                    $"\t{item.PresentName,-30}" +
                    $"\t{(item.Stock > 0 ? item.Stock.ToString() : "Sem stock!"),-15}");
            }
        }


        private static void Presentes_ProcurarPorPesquisa()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            Console.WriteLine("\n*** Lista de Presentes ***");

            Console.Write("\nQual o presente a pesquisar? > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("\nSem critério de pesquisa!");

            }

            Console.WriteLine("\n> Aguarde...");

            var queryParam = new
            {
                pPresentName = $"%{readKeyBoard}%"
            };

            var listAll = con.Query<Present>("select * from SantaShop.Presents where PresentName like @pPresentName", queryParam).AsList();


            if (listAll is null)
            {

                 MensagemSemRegistos();
                MenuPresentes();
            }
            else
            {
                Presentes_MostrarLista(listAll);
                AguardaQualquerTecla();
                MenuPresentes();
            }
        }

        private static void Presentes_Adicionar()
        {
            Console.WriteLine("\n*** Adicionar presentes ***");

            SqlConnection con = new SqlConnection(ConnectionString);

            Present present = new Present();


            Console.Write("Qual descrição do presente? > ");

            present.PresentName = Console.ReadLine();

            Console.Write("Qual o stock do presente? > ");

            string readKeyBoard = Console.ReadLine();

            int stock;

            bool isStockInt = int.TryParse(readKeyBoard, out stock);

            if(!isStockInt)
            {
                Console.WriteLine("\nO valor do stock é inválido! Tem de ser numero!");
                return;                
            }

            present.Stock = stock;


            Console.WriteLine("\n> Aguarde...");

            long newID = con.Insert<Present>(present);

            if (newID <= 0)
            {
                Console.WriteLine("\nNão foi possível adicionar o presente. Contacte o suporte!");
                AguardaQualquerTecla();
                MenuPresentes();
            }
            else
            {       
                Console.WriteLine($"\nPresente adicionado com o ID {newID}");
                AguardaQualquerTecla();
                MenuPresentes();
            }
        }

        private static void Presentes_Actualizar()
        {

            Console.WriteLine("\n*** Actualizar presentes ***");
            Presentes_ListarTodos(true);

            SqlConnection con = new SqlConnection(ConnectionString);

            Console.Write("Qual o ID do presente a alterar? > ");

            string readKeyBoard = Console.ReadLine();

            Console.WriteLine("\n> Aguarde...");

            Present present = con.Get<Present>(readKeyBoard);

            if (present == null)
            {
                Console.WriteLine("\nNão existe presente com esse ID!");
            }
            else
            {
                Console.Write($"Qual descrição do presente? (Actual: {present.PresentName} - deixar em vazio para manter) > ");


                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {
                    present.PresentName = readKeyBoard;

                }

                Console.Write($"Qual o stock do presente? {present.Stock} - deixar em vazio para manter) > ");

                readKeyBoard = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(readKeyBoard))
                {

                    int stock;

                    bool isStockInt = int.TryParse(readKeyBoard, out stock);

                    if (!isStockInt)
                    {
                        Console.WriteLine("\nO valor do stock é inválido! Tem de ser numero!");
                        return;
                    }

                    present.Stock = stock;

                }


                Console.WriteLine("\n> Aguarde...");

                bool wasUpdated = con.Update<Present>(present);

                if(!wasUpdated)
                {
                    Console.WriteLine("\nNão foi possível actualizar o presente. Contacte o suporte!");
                    AguardaQualquerTecla();
                    MenuPresentes();
                }
                else
                {
                    Console.WriteLine($"\nPresente actualizado com sucesso!");
                    AguardaQualquerTecla();
                    MenuPresentes();
                }

            }
        }

        private static void Presentes_Eliminar()
        {
            Console.WriteLine("\n*** Eliminar presentes ***");
            Presentes_ListarTodos(true);
            
            SqlConnection con = new SqlConnection(ConnectionString);

            Console.Write("Qual o ID do presente a eliminar? > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("\nSem critério de pesquisa!");

            }

            Console.WriteLine("\n> Aguarde...");

            Present present = con.Get<Present>(readKeyBoard);

            if(present == null)
            {
                Console.WriteLine("\nNão existe presente com esse ID!");
            }
            else
            {

                bool deleted = false;
                try
                {
                    deleted = con.Delete<Present>(present);
                }
                catch (SqlException s)
                {
                    Console.WriteLine(s.ToString());
                    throw;
               }
                

                if(deleted)
                {
                    Console.WriteLine("\nPresente eliminado com sucesso!");
                    AguardaQualquerTecla();
                    MenuPresentes();
                }
                else
                {
                    Console.WriteLine("\nNão foi possivel eliminar. Notifique o suporte!");
                    AguardaQualquerTecla();
                    MenuPresentes();
                }

            }

        }

    }
}
