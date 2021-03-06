﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    public class NeighborhoodRepresentation : Graph
    {
        public Node[] Nodes { get; }
        private int _currentNumberOfNodes;
        private int _numberOfNodes;
        private int _numberOfEdges;

        public NeighborhoodRepresentation(int numberOfNodes)
        {
            Nodes = new Node[numberOfNodes];
            _currentNumberOfNodes = 0;
            _numberOfNodes = numberOfNodes;
        }

        public Node[] GetNodes()
        {
            return Nodes;
        }

        public MyList<Edge> GetEdges()
        {
            var edges = new MyList<Edge>();
            for (int i = 0; i < Nodes.Length; i++)
            {
                foreach (var edge in Nodes[i].EdgeList)
                {
                    edges.Add(edge);
                }
            }
            return edges;
        } 

        public void AddNode(int nodeId)
        {
            if (Nodes[nodeId] != null) return;
            Nodes[nodeId] = new Node(nodeId);
            _currentNumberOfNodes += 1;
        }

        private void DeleteNodeFromTable(int nodeId)
        {
            Nodes[nodeId] = null;
        }

        private void DeleteEdgesToDeletedNode(int deletedNodeId)
        {
            foreach (var node in Nodes.Where(node => node != null))
            {
                DeleteEdge(node.NodeId, deletedNodeId);
            }
        }

        public void DeleteNode(int nodeId)
        {
            DeleteNodeFromTable(nodeId);
            DeleteEdgesToDeletedNode(nodeId);
            _currentNumberOfNodes -= 1;
        }

        public void AddEdge(int firstNode, int secondNode, int weight)
        {

            if (Nodes[firstNode] == null)
            {
                AddNode(firstNode);
            }

            if (Nodes[secondNode] == null)
            {
                AddNode(secondNode);
            }

            _numberOfEdges += 1;
            var edge = new Edge(weight, Nodes[firstNode], Nodes[secondNode]);

            var node = Nodes[firstNode];
            node?.EdgeList.Add(edge);
        }

        
        public void DeleteEdge(int firstNode, int secondNode)
        {
            Edge element = null;
            foreach (Edge e in Nodes[firstNode].EdgeList)
            {
                if (e.NodeTo.NodeId.Equals(secondNode))
                {
                    element = e;
                    break;
                }
            }
            if (element != null)
            {
                Nodes[firstNode].EdgeList.Remove(element);
            }
        }

        public MyList<Node> GetNeighbors(int nodeId)
        {
            var neighborList = new MyList<Node>();
            
            foreach (var node in Nodes.Where(node => node != null))
            {
                foreach (var edge in node.EdgeList)
                {
                    if (edge.NodeTo.NodeId.Equals(nodeId))
                    {
                        if (!neighborList.Contains(node))
                        {
                            neighborList.Add(node);
                        }
                    } 
                }
            }

            foreach (var edge in Nodes[nodeId].EdgeList)
            {
                if (!neighborList.Contains(edge.NodeTo))
                {
                    neighborList.Add(edge.NodeTo);
                }
            }
            
            return neighborList;
        }

        public MyList<Edge> GetOutboundEdges(int nodeId)
        {
            var edges = new MyList<Edge>();

            foreach (var edge in Nodes[nodeId].EdgeList)
            {
                edges.Add(edge);
            }

            return edges;
        }

        public MyList<Edge> GetIncidentEdges(int nodeId)
        {
            var edges = new MyList<Edge>();
            var neighbors = new MyList<Node>();

            neighbors = GetNeighbors(nodeId);

            foreach (var neighbor in neighbors)
            {
                foreach (var edge in Nodes[neighbor.NodeId].EdgeList)
                {
                    if (edge.NodeTo.NodeId.Equals(nodeId))
                    {
                        edges.Add(edge);
                    }
                }
            }


            foreach (var edge in Nodes[nodeId].EdgeList)
            {
                edges.Add(edge);
            }
            
            return edges;
        }

        public Edge GetEdge(int nodeFrom, int nodeTo)
        {
            foreach (var edge in Nodes[nodeFrom].EdgeList)
            {
                if (edge.NodeTo.NodeId.Equals(nodeTo)) return edge;
            }

            return null;
        }

        public int GetNumberOfNodes()
        {
            return _currentNumberOfNodes;
        }

        public int GetCurrentNumberOfNodes()
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

        public bool isEdge(int nodeFrom, int nodeTo)
        {
            foreach (var edge in Nodes[nodeFrom].EdgeList)
            {
                if (edge.NodeTo.NodeId.Equals(nodeTo)) return true;
            }

            return false;
        }
    }
}