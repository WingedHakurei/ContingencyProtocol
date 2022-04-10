using System.Collections.Generic;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Maps
{
    public abstract class NodeBase : MonoBehaviour
    {
        #region Basic Data
        [SerializeField] protected float size;
        public float Size => size;
        public ICoord Coord { get; set; }
        public bool IsObstacle { get; set; }
        public float DistanceTo(NodeBase other) => Coord.DistanceTo(other.Coord);
        public void Init(ICoord coord, bool isObstacle)
        {
            this.Coord = coord;
            this.IsObstacle = isObstacle;

            transform.position = coord.RealPosition;
        }

        #endregion
        #region Pathfinding
        public IEnumerable<NodeBase> Neighbors { get; protected set; }
        public abstract void CacheNeighbors();
        public NodeBase Prev { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public float F => G + H;
        #endregion

    }
    public interface ICoord
    {
        Vector3 RealPosition { get; }
        float DistanceTo(ICoord other);
    }
}
