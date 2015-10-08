using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            node?.EdgeList.Add(edge);
        }

        
        public void DeleteEdge(int firstNode, int secondNode)
        {
            Edge element = null;
            foreach (Edge e in _nodes[firstNode].EdgeList)
            {
                if (e.NodeObject.NodeId.Equals(secondNode))
                {
                    element = e;
                    break;
                }
            }
            if (element != null)
            {
                _nodes[firstNode].EdgeList.Remove(element);
            }
        }

        public MyList<Node> GetNeighbors(int nodeId)
        {
            var neighborList = new MyList<Node>();
            
            foreach (var node in _nodes)
            {
                foreach (var edge in node.EdgeList)
                {
                    if (!edge.NodeObject.NodeId.Equals(nodeId) || !node.NodeId.Equals(nodeId)) continue;
                    if (!neighborList.Contains(edge.NodeObject))
                    {
                        neighborList.Add(edge.NodeObject);
                    }
                }
            }
            
            return neighborList;
        }

        public MyList<Edge> GetIncidentEdges(int nodeId)
        {
            var edges = new MyList<Edge>();
            var neighbors = new MyList<Node>();

            neighbors = GetNeighbors(nodeId);

            foreach (var neighbor in neighbors)
            {
                foreach (var edge in _nodes[neighbor.NodeId].EdgeList)
                {
                    if (edge.NodeObject.NodeId.Equals(nodeId))
                    {
                        edges.Add(edge);
                    }
                }
            }


            foreach (var edge in _nodes[nodeId].EdgeList)
            {
                edges.Add(edge);
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
            var found = false;
            foreach (var neighbor in neighbors)
            {
                if (neighbor.NodeId.Equals(secondNode))
                {
                    found = true;
                }
            }
            return found;
        }
    }
}