using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Assert.AreEqual(0, _subject.Data);
        }
    }
}
