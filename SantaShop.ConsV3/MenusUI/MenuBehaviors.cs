
using SantaShop.ConsV3.Interfaces;
using SantaShop.Core.Interfaces;
using SantaShop.Core.TablesModels;

using System;
using System.Collections.Generic;

namespace SantaShop.ConsV3.MenusUI
{
    public class MenuBehaviors : DefaultSubMenu, IOpcoesMenu
    {


        public MenuBehaviors(ISantaShopService serviceClass) : base(serviceClass)
        {
            DescricaoOpcao = "Presentes";
        }

        public void Actualizar()
        {
            Console.WriteLine("\n*** Actualizar comportamentos ***");
            ListarTodos(true);


            long behaviorID = ConsoleUtils.ReadLong("Qual o ID do comportamento a alterar?");
            Console.WriteLine("\n> Aguarde...");

            Behavior behavior = _serviceClass.Pesquisar<Behavior>(behaviorID);

            if (behavior == null)
            {
                Console.WriteLine("\nNão existe comportamento com esse ID!");
            }
            else
            {
                behavior.BehaviorDescription =ConsoleUtils.ReplaceOrMaintain($"Qual descrição do comportamento? (Actual: {behavior.BehaviorDescription} - deixar em vazio para manter)",
                    behavior.BehaviorDescription);

                behavior.IsEligibleForPresent = ConsoleUtils.ReadBool($"\nO comportamento dá direito a presente (S/N) (Actual {(behavior.IsEligibleForPresent ? "SIM" : "NÃO")})");

                ActualizarRegisto<Behavior>(behavior);

            }
        }
        public void Adicionar()
        {
            Console.WriteLine("\n*** Adicionar comportamentos ***");

            Behavior behavior = new Behavior();


            behavior.BehaviorDescription = ConsoleUtils.ReplaceOrMaintain("Qual descrição do comportamento?", behavior.BehaviorDescription);

            behavior.IsEligibleForPresent = ConsoleUtils.ReadBool("O comportamento dá direito a presente (S/N)");


            InserirRegisto<Behavior>(behavior);
        }
        public void Eliminar()
        {
            ApagarRegisto<Child>("o comportamento");
        }
        public void ListarTodos(bool soLista = false)
        {
            ListarTodosMenu(soLista);
        }

        public void ProcurarPorPesquisa()
        {

            ProcurarPorPesquisaMenu("*** Lista de Comportamentos ***", 
                                "Qual o comportamento a pesquisar?", "BehaviorDescription");
        }


        protected override void MostrarLista(IEnumerable<dynamic> list)
        {

            string message = $"{"ID",-5}" +
                                $"\t{"Comportamento",-30}" +
                                $"\t{"Tem Presente?",-15}";

            Console.WriteLine(message);

            string strFill = new string('-', message.Length + 5);
            Console.WriteLine(strFill);

            foreach (var item in list)
            {
                Console.WriteLine($"{item.BehaviorID,-5}" +
                    $"\t{item.BehaviorDescription,-30}" +
                    $"\t{(item.IsEligibleForPresent ? "SIM" : "Não"),-15}");
            }
        }

    }
}
