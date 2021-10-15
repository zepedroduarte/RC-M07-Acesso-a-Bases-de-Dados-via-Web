using SantaShop.ConsV2.Interfaces;

using System;

namespace SantaShop.ConsV2.MenusUI
{
    public class MainMenu
    {
        public event EventHandler OnTerminarAplicacao;


        private readonly IOpcoesMenu _presentsMenu;
        private readonly IOpcoesMenu _behaviorMenu;
        private readonly IOpcoesMenu _childremMenu;

        public MainMenu(IOpcoesMenu presentsMenu, IOpcoesMenu behaviorMenu, IOpcoesMenu childremMenu)
        {
            _presentsMenu = presentsMenu ?? throw new ArgumentNullException(nameof(presentsMenu));
            _behaviorMenu = behaviorMenu ?? throw new ArgumentNullException(nameof(behaviorMenu));
            _childremMenu = childremMenu ?? throw new ArgumentNullException(nameof(childremMenu));
        }

        public void MenuPrincipal()
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

                    MenuTabela("Presentes", _presentsMenu);
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    MenuTabela("Comportamentos", _behaviorMenu);
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    MenuTabela("Crianças", _childremMenu);
                    break;

                default:
                    OnTerminarAplicacao(this, null);
                    break;
            }

        }

        private void OpcoesTabelas()
        {
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Procurar por pesquisa");
            Console.WriteLine("3 - Criar novo");
            Console.WriteLine("4 - Actualizar");
            Console.WriteLine("5 - Eliminar");
            Console.WriteLine("Qualquer tecla para voltar ao menu principal.");
        }

        private void MenuTabela(string DescricaoOpcao, IOpcoesMenu menu)
        {
            Console.Clear();
            Console.WriteLine($"*** TABELA: {DescricaoOpcao} ***");
            OpcoesTabelas();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    menu.ListarTodos();
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    menu.ProcurarPorPesquisa();
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    menu.Adicionar();
                    break;


                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    menu.Actualizar();
                    break;


                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    menu.Eliminar();
                    break;

                default:
                    MenuPrincipal();
                    break;
            }

            MenuTabela(DescricaoOpcao, menu);

        }
    }
}
