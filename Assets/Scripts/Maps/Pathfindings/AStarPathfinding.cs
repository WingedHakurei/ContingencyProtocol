using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Maps
{
    public class AStarPathfinding : MonoBehaviour
    {
        public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode, int maxHop = 100)
        {
            List<NodeBase> path = new List<NodeBase>();
            var toSearch = new List<NodeBase>() { startNode };
            var processed = new List<NodeBase>();

            while (toSearch.Any())
            {
                #region 从toSearch寻找最小代价的节点
                var cur = toSearch[0];
                foreach (var t in toSearch)
                {
                    if (t.F < cur.F || (t.F == cur.F && t.H < cur.H))
                        cur = t;
                }

                toSearch.Remove(cur);
                processed.Add(cur);
                #endregion
                #region 寻路结束
                if (cur == targetNode)
                {
                    var curPathNode = targetNode;

                    int count = maxHop;
                    while (curPathNode != startNode)
                    {
                        path.Add(curPathNode);
                        curPathNode = curPathNode.Prev;

                        --count;
                        if (count < 0)
                            throw new Exception();
                    }
                    path.Reverse();
                    return path;
                }
                #endregion
                #region 寻路中
                foreach (var neighbor in cur.Neighbors.Where(n => !n.IsObstacle && !processed.Contains(n)))
                {
                    float neighborG = cur.G + cur.DistanceTo(neighbor);
                    bool hasSearched = toSearch.Contains(neighbor);
                    if (!hasSearched || neighborG < neighbor.G)
                    {
                        neighbor.G = neighborG;
                        neighbor.Prev = cur;

                        if (!hasSearched)
                        {
                            neighbor.H = neighbor.DistanceTo(targetNode);
                            toSearch.Add(neighbor);
                        }
                    }
                }
                #endregion
            }
            return path;
        }
    }
}
