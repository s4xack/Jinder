using System;
using System.Collections.Generic;
using System.Linq;
using Joiner;
using NUnit.Framework;

namespace LinqTasks.Tests
{
    public class Tests
    {
        private class NamedInt : INamed
        {
            public String Name { get; }
            public Int32 Value { get; }

            public NamedInt(String name, Int32 value)
            {
                Name = name;
                Value = value;
            }
        }

        [Test]
        public void JoinNames_with_empty_list_should_return_empty_string()
        {
            // Arrange
            String expected = String.Empty;
            List<NamedInt> list = new List<NamedInt>();
            // Act
            String result = NameJoiner.JoinNames(list, "#");
            // Assert
            Assert.That(expected == result);
        }

        [Test]
        public void JoinNames_with_list_with_one_element_should_return_only_one_name()
        {
            // Arrange
            String name = "name";
            String expected = name;
            List<NamedInt> list = new List<NamedInt>() {new NamedInt(name, 1)};
            // Act
            String result = NameJoiner.JoinNames(list, "#");
            // Assert
            Assert.That(expected == result);
        }

        [Test]
        public void JoinNames_with_list_three_elements_should_return_joined_string_with_delimiter()
        {
            // Arrange
            String delimiter = "#";
            List<NamedInt> list = new List<NamedInt>() {new NamedInt("a", 1), new NamedInt("b", 1), new NamedInt("c", 1)};
            String expected = String.Join(delimiter, list.Select(e => e.Name));
            // Act
            String result = NameJoiner.JoinNames(list, "#");
            // Assert
            Assert.That(expected == result);
        }
    }
}