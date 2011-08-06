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
            Node head = _subject.First;
            int count = _subject.Count;

            // Assert
            Assert.IsNull(head, "head");
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
            // Arrange
            Add(_source);
            var index = 1;
            var original = _subject[index];
            Node newNode = new Node { Value = 10 };

            // Act
            _subject[index] = newNode;

            // Assert
            var actual = _subject[index];
            Assert.AreSame(newNode, actual, "node");
            Assert.AreSame(original.Next, actual.Next, "next node");
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
        #endregion Indexer tests

        private void Add(IList<int> expected)
        {
            foreach (var item in expected)
                _subject.Add(item);
        }
    }
}
