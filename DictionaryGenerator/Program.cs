using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DictionaryGenerator
{
    class Program
    {
        private static List<string> s_words = new List<string>();
        private static List<string> s_final; 
 
        static void Main(string[] args)
        {
            Collect();
            BeginMerge();
             Console.ReadLine();
        }

        private static void BeginMerge()
        {
            AddAbv(ref s_words);
            s_final = new List<string>(s_words.ToArray());
            foreach (var word in s_words)
            {
                Merge(word, 0);
            }
        }

        private static void AddAbv(ref List<string> lst)
        {
            var list = new List<string>(lst);
            foreach (var word in lst)
            {
                var s = new string(word.Take(1).ToArray());
                if (!s.ContainsNumber())
                    list.Add(s);
            }
            lst = list;
        }

        private static void Collect()
        {
            Console.WriteLine("Enter words : ");
            string temp;
            do
            {
                temp = Console.ReadLine();
                if (temp != ".")
                    s_words.Add(temp);
            } while (temp != ".");
        }

        private static void Merge(string w, int depth)
        {
            depth++;
            foreach (var word in s_words)
            {
                string newWord = w + word;
                s_final.Add(newWord);
                int regex = 0;
                if (!word.ContainsNumber())
                    regex = Regex.Matches(newWord, word).Count;
                if (newWord.Length < 63 && depth < 4 && regex < 2)
                    Merge(newWord, depth);
            }
        }
    }

    public static class ExtensionMethods
    {
        public static bool ContainsNumber(this string str)
        {
            char[] numbers = {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
            if (numbers.Any(str.Contains))
            {
                return true;
            }
            return false;
        }
    }

}
