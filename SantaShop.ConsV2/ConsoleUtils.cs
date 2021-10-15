using System;
using System.Collections.Generic;
using System.Text;

namespace SantaShop.ConsV2
{
    public static class ConsoleUtils
    {
        public static void AguardaQualquerTecla()
        {
            Console.WriteLine("\nPrima qualquer tecla para continuar...");
            Console.ReadKey(true);
        }

        public static void MensagemSemRegistos()
        {
            Console.WriteLine("Não existem registos!");
           ConsoleUtils.AguardaQualquerTecla();
        }

        public static long ReadLong(string Message)
        {
            Console.Write($"{Message} > ");

            string readKeyBoard = Console.ReadLine();
            long recID;

            long.TryParse(readKeyBoard, out recID);

            return recID;
        }


        public static int ReadInt(string Message)
        {
            Console.Write($"{Message} > ");

            string readKeyBoard = Console.ReadLine();
            int recID;

            int.TryParse(readKeyBoard, out recID);

            return recID;
        }


        public static string ReadString(string Message)
        {
            Console.Write($"{Message} > ");

            return Console.ReadLine();
        }

        public static long ReplaceOrMaintain(string Message, long CurrentValue)
        {
            long finalValue = CurrentValue;

            string readKeyBoard = ConsoleUtils.ReadString($"{Message}");

            if (!string.IsNullOrWhiteSpace(readKeyBoard))
            {
                long.TryParse(readKeyBoard, out finalValue);
            }

            return finalValue;
        }

        public static int ReplaceOrMaintain(string Message, int CurrentValue)
        {
            int finalValue = CurrentValue;

            string readKeyBoard = ConsoleUtils.ReadString($"{Message}");

            if (!string.IsNullOrWhiteSpace(readKeyBoard))
            {
                int.TryParse(readKeyBoard, out finalValue);
            }

            return finalValue;
        }

        public static string ReplaceOrMaintain(string Message, string CurrentValue)
        {
            string finalValue = CurrentValue;

            string readKeyBoard = ConsoleUtils.ReadString($"{Message}");


            if (!string.IsNullOrWhiteSpace(readKeyBoard))
            {
                finalValue = readKeyBoard;
            }

            return finalValue;
        }


        public static bool ReadBool(string Message)
        {
            bool finalValue;

            Console.Write($"{Message}> ");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.S:
                    finalValue = true;
                    break;

                default:
                    finalValue = false;
                    break;

            }

            return finalValue;
        }
    }
}
