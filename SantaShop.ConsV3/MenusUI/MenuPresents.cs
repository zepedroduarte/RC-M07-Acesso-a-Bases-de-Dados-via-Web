
using SantaShop.Core.Interfaces;


using System;
using System.Collections.Generic;
using SantaShop.ConsV3.Interfaces;
using SantaShop.Core.TablesModels;

namespace SantaShop.ConsV3.MenusUI
{
    public class MenuPresents : DefaultSubMenu, IOpcoesMenu
    {

        public MenuPresents(ISantaShopService serviceClass) : base(serviceClass)
        {
            DescricaoOpcao = "Presentes";
        }


        public void Actualizar()
        {
            Console.WriteLine("\n*** Actualizar presentes ***");
            ListarTodos(true);
            long presentID = ConsoleUtils.ReadLong("Qual o ID do presente a alterar?");

            Console.WriteLine("\n> Aguarde...");

            Present present = _serviceClass.Pesquisar<Present>(presentID);

            if (present == null)
            {
                Console.WriteLine("\nNão existe presente com esse ID!");
            }
            else
            {

                present.PresentName =ConsoleUtils.ReplaceOrMaintain($"Qual descrição do presente? (Actual: {present.PresentName} - deixar em vazio para manter)",
                    present.PresentName);


                present.Stock =ConsoleUtils.ReplaceOrMaintain($"Qual o stock do presente? {present.Stock} - deixar em vazio para manter)",
                                                 present.Stock);

                ActualizarRegisto<Present>(present);
            }
        }

        public void Adicionar()
        {
            Console.WriteLine("\n*** Adicionar presentes ***");
            Present present = new Present();

            present.PresentName = ConsoleUtils.ReadString("Qual descrição do presente?");
            present.Stock = ConsoleUtils.ReadInt("Qual o stock do presente?");

            InserirRegisto<Present>(present);
        }

        public void Eliminar()
        {
            ApagarRegisto<Present>("o presente");
        }

        public void ListarTodos(bool soLista = false)
        {
            ListarTodosMenu(soLista);
        }

        public void ProcurarPorPesquisa()
        {

            ProcurarPorPesquisaMenu("*** Lista de presentes ***",
                                "Nome do presente a pesquisar?", "PresentName");

        }


        protected override void MostrarLista(IEnumerable<dynamic> list)
        {
            string message = $"{"ID",-5}" +
                            $"\t{"Presente",-30}" +
                            $"\t{"Stock",-5}";

            Console.WriteLine(message);

            string strFill = new string('-', message.Length + 5);
            Console.WriteLine(strFill);

            foreach (var item in list)
            {
                Console.WriteLine($"{item.PresentID,-5}" +
                    $"\t{item.PresentName,-30}" +
                    $"\t{(item.Stock > 0 ? item.Stock.ToString() : "Sem stock!"),-15}");
            }
        }
    }
}
