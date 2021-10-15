
using SantaShop.ConsV2.Interfaces;
using SantaShop.ConsV2.Models;

using System;
using System.Collections.Generic;

namespace SantaShop.ConsV2.MenusUI
{
    public class MenuChildren : DefaultSubMenu, IOpcoesMenu
    {


        private readonly IOpcoesMenu _menuPresents;
        private readonly IOpcoesMenu _menuBehavior;


        public MenuChildren(ISantaShopService serviceClass, IOpcoesMenu menuPresents, IOpcoesMenu menuBehavior)  : base(serviceClass)
        {
            DescricaoOpcao = "Crianças";

            _menuPresents = menuPresents ?? throw new ArgumentNullException(nameof(menuPresents));
            _menuBehavior = menuBehavior ?? throw new ArgumentNullException(nameof(menuBehavior));
        }

        public void Actualizar()
        {
            Console.WriteLine("\n*** Actualizar crianças ***");
            ListarTodos(true);

            long childID = ConsoleUtils.ReadLong("Qual o ID da criança a alterar? > ");

            Console.WriteLine("\n> Aguarde...");

            Child child = _serviceClass.Pesquisar<Child>(childID);

            if (child == null)
            {
                Console.WriteLine("\nNão existe criança com esse ID!");
            }
            else
            {

                child.ChildName =ConsoleUtils.ReplaceOrMaintain($"Qual o nome da criança?  (Actual: {child.ChildName} - deixar em vazio para manter)",
                            child.ChildName);

                child.YearOfBirth =ConsoleUtils.ReplaceOrMaintain($"Qual o ano de nascimento da criança?  (Actual: {child.YearOfBirth} - deixar em vazio para manter)",
                            child.YearOfBirth);

                _menuBehavior.ListarTodos(true);

                child.BehaviorID =ConsoleUtils.ReplaceOrMaintain($"Selecione o comportamento indicado o ID? (Actual: {child.BehaviorID} - deixar em vazio para manter)",
                                            (long)child.BehaviorID);
                

                _menuPresents.ListarTodos(true);


                child.PresentID =ConsoleUtils.ReplaceOrMaintain($"Selecione o presente indicado o ID? (Actual: {child.PresentID} - deixar em vazio para manter)",
                            (long)child.PresentID);


                ActualizarRegisto<Child>(child);
            }
        }

        public void Adicionar()
        {

            Child crianca = new Child();

            crianca.ChildName = ConsoleUtils.ReadString("Qual o nome da criança?");

            crianca.YearOfBirth = ConsoleUtils.ReadInt("Qual o ano de nascimento da criança ?");

            _menuBehavior.ListarTodos(true);
            long behaviorID = ConsoleUtils.ReadLong("Selecione o comportamento indicado o ID?");

            _menuPresents.ListarTodos(true);
            long presentID = ConsoleUtils.ReadLong("Selecione o presente indicado o ID?");


            Console.WriteLine("\n> Aguarde...");

            crianca.PresentID = presentID;
            crianca.BehaviorID = behaviorID;


            InserirRegisto<Child>(crianca);

        }


        public void ProcurarPorPesquisa()
        {

            ProcurarPorPesquisaMenu("*** Lista de Crianças ***",
                                "Nome da criança a pesquisar?", "child.ChildName");

        }

        public void Eliminar()
        {
            ApagarRegisto<Child>("a criança");
        }

        public void ListarTodos(bool soLista = false)
        {
            ListarTodosMenu(soLista);
        }


        protected override void MostrarLista(IEnumerable<dynamic> list)
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

            foreach (var item in list)
            {
                Console.WriteLine($"{item.ChildID,-5}" +
                    $"\t{item.ChildName,-30}" +
                    $"\t{(DateTime.Now.Year - item.YearOfBirth),-5}" +
                    $"\t{item.BehaviorDescription,-10}" +
                    $"\t{(item.IsEligibleForPresent ? "SIM" : "Não"),-15}" +
                    $"\t{item.PresentName,-30}");
            }
        }


    }
}
