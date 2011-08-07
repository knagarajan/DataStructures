using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            _source = new int[] { 1, 2, 3 };
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
        public void ConstructionWithCapacity()
        {
            // Arrange
            var capacity = 4;

            // Act
            _subject = new LinkedList(capacity);

            // Assert
            Assert.AreEqual(capacity, _subject.Count);
            for (int i = 0; i < _subject.Count; i++)
                Assert.IsNotNull(_subject[i]);
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
        public void Remove()
        {
            VerifyRemove(_source.First(), _source);
            VerifyRemove(_source.Last(), _source);
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
            var actual = _subject[3];
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
            _subject[3] = new Node();
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

        private void Add(IList<int> expected)
        {
            foreach (var item in expected)
                _subject.Add(item);
        }

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

        private void VerifyRemove(int element, IList<int> source)
        {
            // Arrange
            Add(source);
            var index = _source.IndexOf(element);
            var count = _subject.Count;

            // Act
            _subject.Remove(element);

            // Assert
            Assert.AreEqual(count - 1, _subject.Count, "count");
            Assert.AreNotSame(element, _subject[index], "still exists");
        }
    }
}