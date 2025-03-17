using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace filecript
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                SelectActions();
            }
        }
        private static void SelectActions()
        {
            Console.WriteLine("выберите тип");
            Console.WriteLine("1. Зашифровка");
            Console.WriteLine("2. Расшифровка");

            int value = int.Parse(Console.ReadLine());
            DataEntry(value);
        }
        private static void DataEntry(int a)
        {
            string value = null;
            
            if (a == 1)
            {
                Console.WriteLine("введите название файла который хотите зашифровать");
            }
            if (a == 2)
            {
                Console.WriteLine("Введите название файла для расшифровки"); 
            }
            value = Console.ReadLine();
            Console.WriteLine("введите сдвиг данных 1-255");
            int shift = int.Parse(Console.ReadLine());

            Console.WriteLine("введите пароль");
            string key = Console.ReadLine();

            Console.WriteLine("введите сдвиг пароля 1-255");
            int keyshift = int.Parse(Console.ReadLine());
            if (a == 1)
            {
                Encript(value, shift, key, keyshift);
            }
            if (a == 2)
            {
                Decript(value, shift, key, keyshift);
            }
        }
        private static void Encript(string value, int shift, string key, int keyshift)
        {

            byte[] valueB = File.ReadAllBytes(value);
            byte[] keyB = ConvetByte(key);
            keyB = CaesarCipher(keyB.ToList(), keyshift).ToArray();
            int maxLength = Math.Max(valueB.Length, keyB.Length);
            List<byte> ExitByte = new List<byte>();
            ExitByte = ByteXor(maxLength, valueB, keyB);
            ExitByte = CaesarCipher(ExitByte, shift);
            byte[] byteArray = ExitByte.ToArray();
            Console.WriteLine("Выберите название зашифрованного файла");
            string filenew = Console.ReadLine();
            File.WriteAllBytes(filenew, byteArray);
            Console.WriteLine("файл сохранён");
            

            Console.WriteLine();
            Console.ReadKey();
        }
        private static List<byte> ByteXor(int maxLength, byte[] valueB, byte[] keyB)
        {
            List<byte> ExitByte = new List<byte>();
            for (int i = 0; i < maxLength; i++)
            {
                byte byte1 = valueB[i % valueB.Length];
                byte byte2 = keyB[i % keyB.Length];
                byte byte3 = (byte)(byte1 ^ byte2);
                ExitByte.Add(byte3);

            }
            return ExitByte;
        }
        private static void Decript(string value, int shift, string key, int keyshift)
        {
            byte[] byteArray = File.ReadAllBytes(value);

            byte[] keyB = ConvetByte(key);
            keyB = CaesarCipher(keyB.ToList(), keyshift).ToArray();

            int maxLength = Math.Max(byteArray.Length, keyB.Length);

            List<byte> shiftedBytes = CaesarCipher(byteArray.ToList(), -shift);

            List<byte> decryptedBytes = ByteXor(maxLength, shiftedBytes.ToArray(), keyB);

            byte[] byteArrayExit = decryptedBytes.ToArray();
            Console.WriteLine("Выберите название расшифрованного файла");
            string filenew = Console.ReadLine();
            File.WriteAllBytes(filenew, byteArrayExit);


            Console.WriteLine("файл сохранён");
            Console.ReadKey();
        }
        private static byte[] ConvetByte(string convert)
        {
            return Encoding.UTF8.GetBytes(convert);
        }
        static List<byte> CaesarCipher(List<byte> input, int shift)
        {
            List<byte> output = new List<byte>();
            foreach (byte b in input)
            {
                int shiftedByte = b + shift;

                if (shiftedByte > 255)
                {
                    shiftedByte -= 256;
                }
                else if (shiftedByte < 0)
                {
                    shiftedByte += 256;
                }

                output.Add((byte)shiftedByte);
            }
            return output;
        }
    }
}

