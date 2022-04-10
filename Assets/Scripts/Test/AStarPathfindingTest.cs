using System.Collections.Generic;
using System.Linq;
using KomeijiRai.ContingencyProtocol.Maps;
using UnityEngine;

namespace KomeijiRai
{
    public class AStarPathfindingTest : MonoBehaviour
    {
        private List<NodeBase> path = null;

        public void FindPathTest()
        {
            if (path != null)
            {
                foreach (var item in path)
                    item.GetComponentInChildren<Renderer>().material.color = new Color32(102, 204, 255, 255);
            }
            var keys = GridManager.Instance.Nodes.Keys.ToList();
            int size = keys.Count;
            int rand = 0;
            NodeBase startNode = null;
            NodeBase targetNode = null;
            Debug.Log("---start---");
            do
            {
                rand = Random.Range(0, size);
                Debug.Log(keys[rand]);
                startNode = GridManager.Instance.Nodes[keys[rand]];
            } while (startNode.IsObstacle);
            Debug.Log("---target---");
            do
            {
                rand = Random.Range(0, size);
                Debug.Log(keys[rand]);
                targetNode = GridManager.Instance.Nodes[keys[rand]];
            } while (targetNode.IsObstacle);
            path = AStarPathfinding.FindPath(startNode, targetNode);
            foreach (var item in path)
                item.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
    }
}
