using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZAiSD_Graphs.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Helpers;

namespace ZAiSD_Graphs.Classes.Tests
{
    [TestClass()]
    public class MatrixRepresentationTests
    {

        private MatrixRepresentation repr;

        [TestMethod()]
        public void MatrixRepresentationTest()
        {
            repr = new MatrixRepresentation(21);
            Assert.IsNotNull(repr);
        }

        [TestMethod()]
        public void LoadingMatrixGraphTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            Assert.IsNotNull(repr);
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            repr = new MatrixRepresentation(22);
            ReadFromFile.LoadMatrixGraph(repr);
            repr.AddNode(21);
            Assert.IsNotNull(repr.Nodes[21]);
        }

        [TestMethod()]
        public void DeleteNodeTest()
        {
            repr = new MatrixRepresentation(22);
            ReadFromFile.LoadMatrixGraph(repr);
            repr.DeleteNode(18);
            Assert.IsNull(repr.Nodes[18]);
            Assert.IsNull(repr.Matrix[18,19]);
            Assert.IsNull(repr.Matrix[19,18]);
        }

        [TestMethod()]
        public void AddEdgeTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            repr.AddEdge(18,3,50);
            Assert.IsNotNull(repr.Matrix[18,3]);
            Assert.AreEqual(repr.Matrix[18,3].Weight,50);
        }

        [TestMethod()]
        public void DeleteEdgeTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            repr.DeleteEdge(13,11);
            Assert.IsNull(repr.Matrix[13,11]);
        }

        [TestMethod()]
        public void GetNeighborsTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            var list = repr.GetNeighbors(10);
            Assert.AreEqual(list.Length, 9);
        }

        [TestMethod()]
        public void GetIncidentEdgesTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            var list = repr.GetIncidentEdges(10);
            Assert.AreEqual(list.Length, 11);
        }

        [TestMethod()]
        public void GetNumberOfNodesTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            Assert.AreEqual(repr.GetNumberOfNodes(),20);
            repr.DeleteNode(3);
            Assert.AreEqual(repr.GetNumberOfNodes(),19);
        }

        [TestMethod()]
        public void GetNumberOfEdgesTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            Assert.AreEqual(repr.GetNumberOfEdges(),100);
            repr.DeleteEdge(1,10);
            Assert.AreEqual(repr.GetNumberOfEdges(),99);
        }

        [TestMethod()]
        public void areNeighborsTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadMatrixGraph(repr);
            Assert.IsTrue(repr.areNeighbors(1,10));
            Assert.IsFalse(repr.areNeighbors(18,2));
        }
    }
}