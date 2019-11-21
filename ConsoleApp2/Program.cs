using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    using System;

    public class VigenereCipher
    {
        const string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        readonly string letters;

        public VigenereCipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
        }

        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }

            return p.Substring(0, n);
        }

        private string Vigenere(string text, string password, bool encrypting = true)
        {
            var gamma = GetRepeatKey(password, text.Length);
            var retValue = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(gamma[i]);
                if (letterIndex < 0)
                {
                    retValue += text[i].ToString();
                }
                else
                {
                    retValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                }
            }

            return retValue;
        }

        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }

    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\ostap\source\repos\ConsoleApp2\cod.txt");
            var cipher = new VigenereCipher("АБВГДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ");
            Console.Write("Введіть текст: ");
            var inputText = Console.ReadLine().ToUpper();
            Console.Write("Введіть ключ: ");
            var password = Console.ReadLine().ToUpper();
            var encryptedText = cipher.Encrypt(inputText, password);
            Console.WriteLine("Зашифроване повідомлення: {0}", encryptedText);
            Console.WriteLine("Дешифроване повідомлення: {0}", cipher.Decrypt(encryptedText, password));
            Console.ReadLine();

            sw.WriteLine("Зашифроване повідомлення: {0}", encryptedText);
            sw.WriteLine("Дешифроване повідомлення: {0}", cipher.Decrypt(encryptedText, password));
            sw.Close();

        }
    }

}