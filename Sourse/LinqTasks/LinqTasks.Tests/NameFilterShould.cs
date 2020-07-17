using System;
using System.Collections.Generic;
using System.Linq;
using Filter;
using NUnit.Framework;

namespace LinqTasks.Tests
{
    [TestFixture]
    public class NameFilterShould
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

            public override Boolean Equals(Object? obj)
            {
                return obj is INamed other && Name == other.Name;
            }
        }

        [Test]
        public void FilFilterByPosition_with_empty_list_should_return_empty_sequence()
        {
            // Arrange
            List<NamedInt> list = new List<NamedInt>();
            // Act
            IEnumerable<INamed> result = NameFilter.FilterByPosition(list);
            // Assert
            Assert.That(!result.Any());
        }

        [Test]
        public void FilterByPosition_with_list_with_elements_name_length_equal_one_should_return_only_first_element()
        {
            // Arrange
            List<NamedInt> list = new List<NamedInt>() {new NamedInt("a", 1), new NamedInt("b", 1), new NamedInt("c", 1)};
            List<NamedInt> expected = list.Take(1).ToList();
            // Act
            IEnumerable<INamed> result = NameFilter.FilterByPosition(list);
            // Assert
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void FilterByPosition_with_list_with_elements_name_length_greater_than_position_should_return_all_elements()
        {
            // Arrange
            List<NamedInt> list = new List<NamedInt>() {new NamedInt("a", 1), new NamedInt("ab", 1), new NamedInt("abc", 1)};
            List<NamedInt> expected = list;
            // Act
            IEnumerable<INamed> result = NameFilter.FilterByPosition(list);
            // Assert
            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}