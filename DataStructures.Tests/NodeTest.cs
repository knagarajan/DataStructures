using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataStructures.Tests
{
    [TestClass()]
    public class NodeTest
    {
        private Node _subject;

        [TestInitialize]
        public void Initialize()
        {
            _subject = new Node();
        }

        [TestMethod()]
        public void DefaultConstruction()
        {
            // Arrange
            Node next = _subject.Next;

            // Assert
            Assert.AreEqual(0, _subject.Value, "value");
            Assert.IsNull(next, "next");
        }
    }
}
