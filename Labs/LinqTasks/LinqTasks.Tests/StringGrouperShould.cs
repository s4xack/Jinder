using System;
using System.Collections.Generic;
using System.Linq;
using Grouper;
using NUnit.Framework;

namespace LinqTasks.Tests
{
    [TestFixture]
    public class StringGrouperShould
    {
        [Test]
        public void StringGrouper_should_return_expected_answers_for_example_sentence()
        {
            // Arrange
            String sentece = "Это что же получается: ходишь, ходишь в школу, а потом бац - вторая смена";
            List<(int, int)> expected = new List<(Int32, Int32)>() {(6, 3), (5, 3), (3, 3), (1, 2), (10, 1), (2, 1)};
            // Act
            List<IGrouping<Int32, String>> result = StringGrouper.GroupWordsByLength(sentece).ToList();
            // Assert
            Assert.That(expected.Count == result.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.That(result[i].Key, Is.EqualTo(expected[i].Item1));
                Assert.That(result[i].Count(), Is.EqualTo(expected[i].Item2));
            }
        }
    }
}