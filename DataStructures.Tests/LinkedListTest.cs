using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace DataStructures.Tests
{
    [TestClass]
    public class LinkedListTest
    {
        private LinkedList _subject;
        private IList<int> _source;

        [TestInitialize]
        public void TestInitialize()
        {
            _subject = new LinkedList();
            _source = new int[] { 1, 2, 3, 4 };
        }

        [TestMethod]
        public void DefaultConstruction()
        {
            // Arrange
            Node first = _subject.First;
            int count = _subject.Count;

            // Assert
            Assert.IsNull(first, "head");
            Assert.AreEqual(0, count, "count");
        }

        [TestMethod]
        public void AddFirst()
        {
            // Arrange
            var expected = 1;

            // Act
            _subject.Add(expected);

            // Assert
            Assert.AreEqual(expected, _subject[0].Value);
        }

        [TestMethod]
        public void AddMany()
        {
            // Act
            Add(_source);

            // Assert
            Assert.AreEqual(_source.Count(), _subject.Count, "count");
            for (int i = 0; i < _subject.Count; i++)
                Assert.AreEqual(_source[i], _subject[i].Value);
        }

        [TestMethod]
        public void RemoveFirst()
        {
            // first
            VerifyRemove(new LinkedList(), _source.First(), _source, _source.Count() - 1);
            //middle
            VerifyRemove(new LinkedList(), 2, _source, _source.Count() - 1);
            // last
            VerifyRemove(new LinkedList(), _source.Last(), _source, _source.Count() - 1);
            // does not exist
            VerifyRemove(new LinkedList(), 1138, _source, _source.Count());
        }

        [TestMethod]
        public void ToStringTest()
        {
            // Arrange
            Add(_source);
            var expected = string.Join(LinkedList.Seperator, _source);

            // Act
            var actual = _subject.ToString();

            // Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void IndexOf()
        {
            // first
            VerifyIndexOf(0, _source[0], _source);
            // middle
            VerifyIndexOf(1, _source[1], _source);
            // last
            VerifyIndexOf(_source.Count() - 1, _source.Last(), _source);
            // does not exist
            VerifyIndexOf(-1, 1138, _source);
        }

        [TestMethod]
        public void TailIndexOf()
        {
            // first
            VerifyTailIndexOf(0, _source.Last(), _source);
            //middle
            VerifyTailIndexOf(2, _source[_source.Count() - 1 - 2], _source);
            // last
            VerifyTailIndexOf(_source.Count() - 1, _source.First(), _source);
            // does not exist
            VerifyIndexOf(-1, 1138, _source);
        }

        /// <summary>
        /// the remaining parts of GetAt and SetAt 
        /// are tested through the indexer
        /// </summary>
        [TestMethod]
        public void GetSetAtInvalidIndex()
        {
            Assert.IsNull(_subject.GetAt(1138));
            //shouldn't throw exception
            _subject.SetAt(1138, new Node());
        }

        #region Indexer tests
        [TestMethod]
        public void IndexerGet()
        {
            // Arrange
            Add(_source);
            var index = 1;
            var expectedValue = 2;

            // Act
            Node actual = _subject[index];

            // Assert
            Assert.AreEqual(expectedValue, actual.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerGetGreaterThanCount()
        {
            Add(_source);
            var actual = _subject[8];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerGetEqualToCount()
        {
            Add(_source);
            var actual = _subject[1138];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerGetNegative()
        {
            Add(_source);
            var actual = _subject[-1];
        }

        [TestMethod]
        public void IndexerSet()
        {
            VerifyElementSetAt(0, _source);
            VerifyElementSetAt(1, _source);
            VerifyElementSetAt(_source.Count() - 1, _source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerSetGreaterThanCount()
        {
            Add(_source);
            _subject[8] = new Node();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerSetEqualToCount()
        {
            Add(_source);
            _subject[1138] = new Node();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerSetNegative()
        {
            Add(_source);
            _subject[-1] = new Node();
        }

        [TestMethod]
        public void IndexerSetNullElement()
        {
            // Arrange
            Add(_source);
            var index = 0;
            var count = _subject.Count;
            var element = _subject[index];

            // Act
            _subject[index] = null;

            // Assert
            Assert.AreEqual(count - 1, _subject.Count, "count");
            Assert.AreNotSame(element, _subject[index], "still exists");
        }

        #endregion Indexer tests

        private void VerifyElementSetAt(int index, IList<int> source)
        {
            // Arrange
            Add(source);
            var original = _subject[index];
            Node newNode = new Node { Value = 10 };

            // Act
            _subject[index] = newNode;

            // Assert
            var actual = _subject[index];
            Assert.AreSame(newNode, actual, "node for {0}", index);
            Assert.AreSame(original.Next, actual.Next, "next node for {0}", index);
        }

        private static void VerifyRemove(LinkedList subject, int element, IList<int> elements, int expectedCount)
        {
            // Arrange
            Add(subject, elements);
            var index = elements.IndexOf(element);

            // Act
            subject.Remove(element);

            // Assert
            Assert.AreEqual(expectedCount, subject.Count, "count for {0}", element);
            Assert.AreNotSame(element, subject.GetAt(index), "{0} still exists", element);
        }

        private static void VerifyIndexOf(int expectedIndex, int element, IList<int> source)
        {
            VerifyIndexMethod(new LinkedList(), l => l.IndexOf, expectedIndex, element, source);
        }

        private static void VerifyTailIndexOf(int expectedIndex, int element, IList<int> source)
        {
            VerifyIndexMethod(new LinkedList(), l => l.TailIndexOf, expectedIndex, element, source);
        }

        private static void VerifyIndexMethod(LinkedList subject, Func<LinkedList, Func<int, int>> indexOfMethod, int expectedIndex, int element, IList<int> source)
        {
            Add(subject, source);

            // Act
            int actual = indexOfMethod(subject).Invoke(element);

            // Assert
            Assert.AreEqual(expectedIndex, actual);
        }

        private static void Add(LinkedList target, IList<int> elements)
        {
            foreach (var item in elements)
                target.Add(item);
        }

        private void Add(IList<int> elements)
        {
            Add(_subject, elements);
        }
    }
}