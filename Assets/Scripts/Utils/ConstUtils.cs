using KomeijiRai.ContingencyProtocol.Maps;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Utils
{
    public class ConstUtils
    {
        public static readonly float SQRT_2 = Mathf.Sqrt(2);
        public static readonly WaitForSeconds WAIT_FOR_1_SEC = new WaitForSeconds(1f);
        public static readonly WaitForSeconds WAIT_FOR_500_MS = new WaitForSeconds(0.5f);
        public static readonly WaitForSeconds WAIT_FOR_100_MS = new WaitForSeconds(0.1f);
        public static readonly WaitForSeconds WAIT_FOR_10_MS = new WaitForSeconds(0.01f);
        public static readonly int GROUND_LAYER = LayerMask.NameToLayer("Ground");
        public const string BULLET_NAMESPACE_NAME = "KomeijiRai.ContingencyProtocol.Entities.Units.Bullets";
        public const string ENEMY_NAMESPACE_NAME = "KomeijiRai.ContingencyProtocol.Entities.Units.Enemies";
    }
}
