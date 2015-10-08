using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    internal class NeighborhoodRepresentation : Graph
    {
        private Node[] _nodes;
        private int _numberOfNodes;
        private int _numberOfEdges;

        public NeighborhoodRepresentation(int numberOfNodes)
        {
            _nodes = new Node[numberOfNodes];
            _numberOfNodes = 0;
        }

        public void AddNode(int nodeId)
        {
            if (_nodes[nodeId] != null) return;
            _nodes[nodeId] = new Node(nodeId);
            _numberOfNodes += 1;
        }

        private void DeleteNodeFromTable(int nodeId)
        {
            _nodes[nodeId] = null;
        }

        private void DeleteEdgesToDeletedNode(int deletedNodeId)
        {
            foreach (var node in _nodes)
            {
                DeleteEdge(node.NodeId, deletedNodeId);
            }
        }

        public void DeleteNode(int nodeId)
        {
            DeleteNodeFromTable(nodeId);
            DeleteEdgesToDeletedNode(nodeId);
            _numberOfNodes -= 1;
        }

        public void AddEdge(int firstNode, int secondNode, int weight)
        {

            if (_nodes[firstNode] == null)
            {
                AddNode(firstNode);
            }

            if (_nodes[secondNode] == null)
            {
                AddNode(secondNode);
            }

            _numberOfEdges += 1;
            var edge = new Edge(weight, _nodes[secondNode]);

            var node = _nodes[firstNode];

            if (node.EdgeList.Head == null)
            {
                node.EdgeList.Head = node.EdgeList.Tail = edge;
            }
            else
            {
                node.EdgeList.Tail.NextEdge = edge;
                node.EdgeList.Tail = edge;
            }
        }

        public void DeleteFromList(Node node, int nodeId)
        {
            var edgeList = node.EdgeList;
            if (edgeList.Head == null) return;

            var element = edgeList.Head;
            var previous = element;
            while (element != null && !element.NodeObject.NodeId.Equals(nodeId))
            {
                previous = element;
                element = element.NextEdge;
            }

            if (element == null) return;

            if (element == edgeList.Head)
            {
                edgeList.Head = element.NextEdge;
            }
            else
            {
                if (element.NextEdge == null)
                {
                    edgeList.Tail = previous;
                    previous.NextEdge = null;
                }
                else
                {
                    previous.NextEdge = element.NextEdge;
                }
            }
        }

        public void DeleteEdge(int firstNode, int secondNode)
        {
            DeleteFromList(_nodes[firstNode], secondNode);
        }

        public List<Node> GetNeighbors(int nodeId)
        {
            var neighborList = new List<Node>();
            
            foreach (var node in _nodes)
            {
                var element = node.EdgeList.Head;

                while (element != null)
                {
                    if (!element.NodeObject.NodeId.Equals(nodeId) || !node.NodeId.Equals(nodeId)) continue;
                    if (!neighborList.Contains(element.NodeObject))
                    {
                        neighborList.Add(element.NodeObject);
                    }
                    element = element.NextEdge;
                }
            }
            
            return neighborList;
        }

        public List<Edge> GetIncidentEdges(int nodeId)
        {
            var edges = new List<Edge>();
            var neighbors = new List<Node>();

            neighbors = GetNeighbors(nodeId);

            foreach (var neighbor in neighbors)
            {
                var element = _nodes[neighbor.NodeId].EdgeList.Head;
                while (element != null)
                {
                    if (element.NodeObject.NodeId.Equals(nodeId))
                    {
                        edges.Add(element);
                    }
                    element = element.NextEdge;
                }
            }

            var e = _nodes[nodeId].EdgeList.Head;

            while (e != null)
            {
                edges.Add(e);
                e = e.NextEdge;
            } 
            

            return edges;
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
            var neighbors = GetNeighbors(firstNode);
            return neighbors.Exists(node => node.NodeId.Equals(secondNode));
        }
    }
}