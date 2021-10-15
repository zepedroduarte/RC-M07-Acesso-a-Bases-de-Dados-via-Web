
using SantaShop.Core;
using SantaShop.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaShop.ConsV3.MenusUI
{
    public abstract class DefaultSubMenu
    {

        protected string DescricaoOpcao;
        

        protected readonly ISantaShopService _serviceClass;


        public DefaultSubMenu(ISantaShopService serviceClass)
        { 
            _serviceClass = serviceClass ?? throw new ArgumentNullException(nameof(serviceClass)); ;
        }


        protected void ListarTodosMenu(bool soLista = false)
        {
            Console.WriteLine("> Aguarde...");
            var listAll = _serviceClass.PesquisarTodos<dynamic>();

            if (listAll is null || listAll.Count() == 0)
            {
                ConsoleUtils.MensagemSemRegistos();
            }
            else
            {
                Console.WriteLine($"\n*** Lista de {DescricaoOpcao} **");
                MostrarLista(listAll);
            }

            if (!soLista)
               ConsoleUtils.AguardaQualquerTecla();
        }


        protected void ApagarRegisto<T>(string texto) where T : class
        {
            Console.WriteLine($"\n*** Eliminar {DescricaoOpcao} ***");
            ListarTodosMenu(true);

            long childID = ConsoleUtils.ReadLong($"Qual o ID d{texto} a eliminar?");

            Console.WriteLine("\n> Aguarde...");

            DeletedStatusEnum deleted = _serviceClass.Eliminar<T>(childID);

            switch (deleted)
            {
                case DeletedStatusEnum.NotFound:
                    Console.WriteLine("\nNão existe registo com esse ID!");
                    break;
                case DeletedStatusEnum.Deleted:
                    Console.WriteLine("\nRegisto eliminado com sucesso!");
                    break;
                case DeletedStatusEnum.NotDeleted:
                    Console.WriteLine("\nNão foi possivel eliminar. Notifique o suporte!");
                    break;
                default:
                    break;
            }
           ConsoleUtils.AguardaQualquerTecla();
        }


        protected void ProcurarPorPesquisaMenu(string cabecalho, string mensagem, string nomeCampo)
        {
            Console.WriteLine($"\n{cabecalho}");
            Console.Write($"\n{mensagem} > ");

            string readKeyBoard = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(readKeyBoard))
            {
                Console.WriteLine("Sem critério de pesquisa!");
               ConsoleUtils.AguardaQualquerTecla();
                return;

            }

            Console.WriteLine("> Aguarde...");
            var listAll = _serviceClass.Pesquisar<dynamic>(nomeCampo, readKeyBoard);

            if (listAll is null || listAll.Count() == 0)
            {
                ConsoleUtils.MensagemSemRegistos();
            }
            else
            {
                Console.WriteLine($"\n{cabecalho}");

                MostrarLista(listAll);
               ConsoleUtils.AguardaQualquerTecla();
            }

    
        }

        protected void InserirRegisto<T>(T item) where T : class
        {
            Console.WriteLine("> Aguarde...");

            long newID = _serviceClass.Inserir<T>(item);

            if (newID <= 0)
            {
                Console.WriteLine("\nNão foi possível adicionar novo registo. Contacte o suporte!");
            }
            else
            {
                Console.WriteLine($"\nNovo registo adicionado com o ID {newID} com sucesso!");
            }
           ConsoleUtils.AguardaQualquerTecla();
        }


        protected void ActualizarRegisto<T>(T item) where T : class
        {
            Console.WriteLine("> Aguarde...");

            bool wasUpdated = _serviceClass.Actualizar<T>(item);

            if (!wasUpdated)
            {
                Console.WriteLine("\nNão foi possível actualizar o registo. Contacte o suporte!");
            }
            else
            {
                Console.WriteLine($"\nRegisto actualizado com sucesso!");
                
            }
           ConsoleUtils.AguardaQualquerTecla();
        }

        protected abstract void MostrarLista(IEnumerable<dynamic> list);

       

    }
}
