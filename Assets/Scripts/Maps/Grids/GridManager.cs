using System.Collections.Generic;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Maps
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }
        private GridManager() { }
        private void Awake()
        {
            Instance = this;
            Nodes = scriptableGrid.GenerateGrid();
            foreach (var node in Nodes.Values)
                node.CacheNeighbors();
        }
        [SerializeField] private ScriptableGrid scriptableGrid;
        public Dictionary<Vector3, NodeBase> Nodes { get; private set; }

    }
}
