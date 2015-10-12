using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZAiSD_Graphs.Helpers;

namespace ZAiSD_Graphs.Classes.Tests
{
    [TestClass()]
    public class MatrixRepresentationTests
    {

        private Graph repr;
        private string path = "C:\\Users\\Sabina\\Source\\Repos\\ZAiSD_Graphs\\ZAiSD_Graphs\\graf.txt";

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
            ReadFromFile.LoadGraph(repr, path);
            Assert.IsNotNull(repr);
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            repr = new MatrixRepresentation(22);
            ReadFromFile.LoadGraph(repr,path);
            repr.AddNode(21);
            var matrixRepresentation = (MatrixRepresentation) repr;
            Assert.IsNotNull(matrixRepresentation.Nodes[21]);
        }

        [TestMethod()]
        public void DeleteNodeTest()
        {
            repr = new MatrixRepresentation(22);
            ReadFromFile.LoadGraph(repr, path);
            repr.DeleteNode(18);
            var matrixRepresentation = (MatrixRepresentation)repr;
            Assert.IsNull(matrixRepresentation.Nodes[18]);
            Assert.IsNull(matrixRepresentation.Matrix[18,19]);
            Assert.IsNull(matrixRepresentation.Matrix[19,18]);
        }

        [TestMethod()]
        public void AddEdgeTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            repr.AddEdge(18,3,50);
            var matrixRepresentation = (MatrixRepresentation)repr;
            Assert.IsNotNull(matrixRepresentation.Matrix[18,3]);
            Assert.AreEqual(matrixRepresentation.Matrix[18,3].Weight,50);
        }

        [TestMethod()]
        public void DeleteEdgeTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            repr.DeleteEdge(13,11);
            var matrixRepresentation = (MatrixRepresentation)repr;
            Assert.IsNull(matrixRepresentation.Matrix[13,11]);
        }

        [TestMethod()]
        public void GetNeighborsTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            var list = repr.GetNeighbors(10);
            Assert.AreEqual(list.Length, 9);
        }

        [TestMethod()]
        public void GetIncidentEdgesTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            var list = repr.GetIncidentEdges(10);
            Assert.AreEqual(list.Length, 11);
        }

        [TestMethod()]
        public void GetNumberOfNodesTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            Assert.AreEqual(repr.GetCurrentNumberOfNodes(),20);
            repr.DeleteNode(3);
            Assert.AreEqual(repr.GetCurrentNumberOfNodes(),19);
        }

        [TestMethod()]
        public void GetNumberOfEdgesTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            Assert.AreEqual(repr.GetNumberOfEdges(),100);
            repr.DeleteEdge(1,10);
            Assert.AreEqual(repr.GetNumberOfEdges(),99);
        }

        [TestMethod()]
        public void areNeighborsTest()
        {
            repr = new MatrixRepresentation(21);
            ReadFromFile.LoadGraph(repr, path);
            Assert.IsTrue(repr.areNeighbors(1,10));
            Assert.IsFalse(repr.areNeighbors(18,2));
        }
    }
}