using System.Linq;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Maps
{
    public class Iso4Node : NodeBase
    {
        public override void CacheNeighbors()
        {
            Neighbors = from kv in GridManager.Instance.Nodes
                        where DistanceTo(kv.Value) == 1
                        select kv.Value;
        }
    }
    public class Iso4Coord : ICoord
    {
        public readonly int x, y;
        public Vector3 RealPosition { get; private set; }
        public Iso4Coord(int x, int y, float scale)
        {
            this.x = x;
            this.y = y;
            RealPosition = new Vector3(x, 0, y) * scale;
        }

        public float DistanceTo(ICoord other)
        {
            Iso4Coord iso4 = other as Iso4Coord;
            if (iso4 != null)
                return Mathf.Abs(x - iso4.x) + Mathf.Abs(y - iso4.y);
            else
                return -1;
        }
    }
}
