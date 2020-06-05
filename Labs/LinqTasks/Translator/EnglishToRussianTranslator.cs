using System;
using System.Collections.Generic;
using System.Linq;

namespace Translator
{
    public static class EnglishToRussianTranslator
    {
        public static String Translate(String text, Dictionary<String, String> dictionary)
        {
            try
            {
                return text
                    .Split()
                    .Select(w => w.ToLower())
                    .Select(w => dictionary[w])
                    .Select((word, index) => new {Word = word, Index = index})
                    .GroupBy(e => e.Index / 3)
                    .Select(g => g
                        .Select(e => e.Word.ToUpper())
                        .DefaultIfEmpty(String.Empty)
                        .Aggregate((a, b) => a + " " + b))
                    .DefaultIfEmpty(String.Empty)
                    .Aggregate((a, b) => a + "\n" + b);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Dictionary do not contain all necessary words!");
            }
        }
    }
}
