using System;
using System.Collections.Generic;
using NUnit.Framework;
using Translator;

namespace LinqTasks.Tests
{
    [TestFixture]
    public class EnglishToRussianTranslatorTests
    {
        [Test]
        public void Translate_should_return_expected_answers_for_example_sentence()
        {
            // Arrange
            String text = "This dog eats too much vegetables after lunch";
            Dictionary<String, String> dictionary = new Dictionary<String, String>()
            {
                {"this", "эта"}, 
                {"dog", "собака"},
                {"eats", "ест"},
                {"too", "слишком"},
                {"much", "много"},
                {"vegetables", "овощей"},
                {"after", "после"},
                {"lunch", "обеда"}
            };
            String expected = "ЭТА СОБАКА ЕСТ\nСЛИШКОМ МНОГО ОВОЩЕЙ\nПОСЛЕ ОБЕДА";
            // Act
            String result = EnglishToRussianTranslator.Translate(text, dictionary);
            // Assert
            Assert.That(result == expected);
        }
    }
}