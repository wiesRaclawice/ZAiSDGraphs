using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    class NeighborhoodRepresentation : Graph
    {
        private Node[] _nodes;
        private int _numberOfNodes ;

        public NeighborhoodRepresentation(int numberOfNodes)
        {
            _nodes = new Node[numberOfNodes];
            _numberOfNodes = 0;
        }

        private bool Found(object nodeId)
        {
            return _nodes.Any(node => node.NodeId.Equals(nodeId));
        }

        public void AddNode(object nodeId)
        {
            if (Found(nodeId)) return;
            _nodes[_numberOfNodes] = new Node(nodeId);
            _numberOfNodes += 1;
        }

        public void DeleteNode(object nodeId)
        {
            throw new NotImplementedException();
        }

        public void AddEdge(object firstNode, object secondNode, object weight)
        {
            throw new NotImplementedException();
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
