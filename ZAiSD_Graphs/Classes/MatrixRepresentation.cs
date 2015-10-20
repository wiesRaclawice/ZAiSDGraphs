using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    public class MatrixRepresentation : Graph
    {
        public Edge[,] Matrix { get; set; }
        public Node[] Nodes { get; set; }
        private int _numberOfNodes;
        private int _currentNumberOfNodes;
        private int _numberOfEdges;

        public MatrixRepresentation(int numberOfNodes)
        {
            Matrix = new Edge[numberOfNodes,numberOfNodes];
            Nodes = new Node[numberOfNodes];
            _numberOfNodes = numberOfNodes;
            _numberOfEdges = 0;
        }

        public Node[] GetNodes()
        {
            return Nodes;
        }

        public MyList<Edge> GetEdges()
        {
            var edges = new MyList<Edge>();
            for (int i = 0; i < _numberOfNodes; i++)
            {
                for (int j = 0; j < _numberOfNodes; j++)
                {
                    if (Matrix[i, j] != null)
                    {
                        edges.Add(Matrix[i,j]);
                    }
                }
            }
            return edges;
        } 

        public void AddNode(int nodeId)
        {
            Nodes[nodeId] = new Node(nodeId);
            _currentNumberOfNodes += 1;
        }

        public void DeleteNode(int nodeId)
        {
            for (int i = 0; i < _numberOfNodes; i++)
            {
                Matrix[nodeId, i] = null;
                Matrix[i, nodeId] = null;
            }
            Nodes[nodeId] = null;
            _currentNumberOfNodes -= 1;
        }

        public void AddEdge(int firstNode, int secondNode, int weight)
        {
            if (Nodes[firstNode] == null) AddNode(firstNode);
            if (Nodes[secondNode] == null) AddNode(secondNode);

            Matrix[firstNode,secondNode] = new Edge(weight, Nodes[firstNode], Nodes[secondNode]);
            _numberOfEdges += 1;
        }

        public void DeleteEdge(int firstNode, int secondNode)
        {
            Matrix[firstNode, secondNode] = null;
            _numberOfEdges -= 1;
        }

        public MyList<Node> GetNeighbors(int nodeId)
        {
            var neighbors = new MyList<Node>();
            for (int i = 0; i < _numberOfNodes; i++)
            {
                if (Matrix[nodeId, i] != null)
                {
                    neighbors.Add(Nodes[i]);
                } 

            }

            for (int i = 0; i < _numberOfNodes; i++)
            {
                if (Matrix[i, nodeId] != null)
                {
                    if (!neighbors.Contains(Nodes[i]))
                    {
                        neighbors.Add(Nodes[i]);
                    }
                }
            }

            return neighbors;
        }

        public MyList<Edge> GetOutboundEdges(int nodeId)
        {
            var edges = new MyList<Edge>();

            for (int i = 0; i < _numberOfNodes; i++)
            {
                if (Matrix[nodeId, i] != null) edges.Add(Matrix[nodeId, i]);
            }

            return edges;
        }

        public MyList<Edge> GetIncidentEdges(int nodeId)
        {
            var edges = new MyList<Edge>();

            for (int i = 0; i < _numberOfNodes; i++)
            {
                if (Matrix[nodeId, i] != null) edges.Add(Matrix[nodeId, i]);
                if (Matrix[i, nodeId] != null) edges.Add(Matrix[i, nodeId]);
            }

            return edges;
        }

        public Edge GetEdge(int nodeFrom, int nodeTo)
        {
            return Matrix[nodeFrom, nodeTo];
        }

        public int GetCurrentNumberOfNodes()
        {
            return _currentNumberOfNodes;
        }

        public int GetNumberOfNodes()
        {
            return _numberOfNodes;
        }

        public int GetNumberOfEdges()
        {
            return _numberOfEdges;
        }

        public bool areNeighbors(int firstNode, int secondNode)
        {
            return Matrix[firstNode, secondNode] != null || Matrix[secondNode, firstNode] != null;
        }

        public bool isEdge(int nodeFrom, int nodeTo)
        {
            return Matrix[nodeFrom, nodeTo] != null;
        }
    }
}
