using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
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
            string[] byteStrings = null;
            if (a == 1)
            {
                Console.WriteLine("введите исходное сообщение");
                value = Console.ReadLine();
            }
            if(a == 2)
            {
                Console.WriteLine("Введите байты через пробел:");

                string input = Console.ReadLine();
                byteStrings = input.Split(' ');
            }
            Console.WriteLine("введите сдвиг данных 1-255");
            int shift = int.Parse(Console.ReadLine());

            Console.WriteLine("введите пароль, не должен быть длинее сообщения");
            string key = Console.ReadLine();

            Console.WriteLine("введите сдвиг пароля 1-255");
            int keyshift = int.Parse(Console.ReadLine());
            if (a == 1)
            {
                Encript(value,shift,key,keyshift);
            }
            if (a == 2)
            {
                Decript(byteStrings,shift,key,keyshift);
            }
        }
        private static void Encript(string value, int shift,string key, int keyshift)
        {
           
            byte[] valueB = ConvetByte(value);
            byte[] keyB = ConvetByte(key);
            keyB = CaesarCipher(keyB.ToList(), keyshift).ToArray();
            int maxLength = Math.Max(valueB.Length, keyB.Length);
            List<byte> ExitByte = new List<byte>();
            ExitByte = ByteXor(maxLength,valueB,keyB);
            ExitByte = CaesarCipher(ExitByte, shift);
            byte[] byteArray = ExitByte.ToArray();
            Console.WriteLine("зашифрованные байты");
            foreach (byte b in byteArray)
            {
                Console.Write(b + " ");
            }

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
        private static void Decript(string[] byteStrings, int shift, string key, int keyshift)
        {
            byte[] byteArray = new byte[byteStrings.Length];
            for (int i = 0; i < byteStrings.Length; i++)
            {
                byteArray[i] = byte.Parse(byteStrings[i]);
            }
            
            byte[] keyB = ConvetByte(key);
            keyB = CaesarCipher(keyB.ToList(), keyshift).ToArray();

            int maxLength = Math.Max(byteArray.Length, keyB.Length);
           
            List<byte> shiftedBytes = CaesarCipher(byteArray.ToList(), -shift);

            List<byte> decryptedBytes = ByteXor(maxLength, shiftedBytes.ToArray(), keyB);

            byte[] byteArrayExit = decryptedBytes.ToArray();
            string result = Encoding.UTF8.GetString(byteArrayExit);

            Console.WriteLine(result);
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
