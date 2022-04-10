using System.Collections.Generic;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Maps
{
    public abstract class ScriptableGrid : ScriptableObject
    {
        [SerializeField] protected NodeBase nodePrefab;
        [SerializeField] protected GameObject groundNodeModelPrefab;
        [SerializeField] protected GameObject obstacleNodeModelPrefab;
        [SerializeField] protected GameObject borderPrefab;
        [SerializeField, Range(0, 6)] private int obstacleWeight = 3;
        public abstract Dictionary<Vector3, NodeBase> GenerateGrid();
        protected bool DecideIfObstacle() => Random.Range(1, 20) <= obstacleWeight;
    }
}
