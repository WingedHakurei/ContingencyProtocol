using System;
using KomeijiRai.ContingencyProtocol.Controllers.Pools;
using KomeijiRai.ContingencyProtocol.Entities.Units.Bullets;
using KomeijiRai.ContingencyProtocol.Utils;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Robots
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private string bulletTypeStr;
        [SerializeField] private Transform firePositionTrans;
        private Type bulletType;
        private void Start()
        {
            bulletType = Type.GetType(
                ConstUtils.BULLET_NAMESPACE_NAME + "." +
                bulletTypeStr);
        }
        public void Fire()
        {
            BulletBase bullet = BulletPool.Instance.Get(bulletType);
            bullet.Init(firePositionTrans.position, firePositionTrans.forward);
        }
    }
}
