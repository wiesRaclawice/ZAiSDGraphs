using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    class NeighborhoodRepresentation : Graph
    {
        private Node[] _nodes;
        private int _numberOfNodes;
        private int _lastIndex;

        public NeighborhoodRepresentation(int numberOfNodes)
        {
            _nodes = new Node[numberOfNodes];
            _numberOfNodes = 0;
            _lastIndex = 0;
        }

        private bool Found(object nodeId)
        {
            return _nodes.Any(node => node != null && node.NodeId.Equals(nodeId));
        }

        public void AddNode(object nodeId)
        {
            if (Found(nodeId)) return;
            _nodes[_lastIndex] = new Node(nodeId);
            _numberOfNodes += 1;
            _lastIndex += 1;
        }

        private void DeleteNodeFromTable(object nodeId)
        {
            for (var i = 0; i < _nodes.Length; i++)
            {
                if (!_nodes[i].NodeId.Equals(nodeId) || _nodes[i] == null) continue;
                _nodes[i] = null;
                break;
            }
        }

        private void DeleteIncidentEdges(object nodeId)
        {
            foreach (var node in _nodes)
            {
                var edgeList = node?.EdgeList;
                if (edgeList?.Head == null ) continue;
                
                var element = edgeList.Head;
                var previous = element;
                while (element != null && !element._nodeId.Equals(nodeId))
                {
                    previous = element;
                    element = element._nextEdge;
                }

                if (element == null) continue;

                if (element == edgeList.Head)
                {
                    edgeList.Head = element._nextEdge;
                }
                else
                {
                    if (element._nextEdge == null)
                    {
                        edgeList.Tail = previous;
                        previous._nextEdge = null;
                    }
                    else
                    {
                        previous._nextEdge = element._nextEdge;
                    }
                }
            }
        }

        public void DeleteNode(object nodeId)
        {
            DeleteNodeFromTable(nodeId);
            DeleteIncidentEdges(nodeId);
            _numberOfNodes -= 1;
        }

        public void AddEdge(object firstNode, object secondNode, int weight)
        {
            var edge = new Edge(weight, secondNode);
            foreach (var node in _nodes.Where(node => node.NodeId.Equals(firstNode)))
            {
                if (node.EdgeList.Head == null)
                {
                    node.EdgeList.Head = node.EdgeList.Tail = edge;
                }
                else
                {
                    node.EdgeList.Tail._nextEdge = edge;
                    node.EdgeList.Tail = edge;
                }
                break;
            }
        }

        public void DeleteEdge(object firstNode, object secondNode)
        {
            throw new NotImplementedException();
        }

        public List<int> GetNeighbors(object nodeId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetEdges(object nodeId)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfNodes()
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfEdges()
        {
            throw new NotImplementedException();
        }

        public bool areNeighbors(object firstNode, object secondNode)
        {
            throw new NotImplementedException();
        }
    }
}
