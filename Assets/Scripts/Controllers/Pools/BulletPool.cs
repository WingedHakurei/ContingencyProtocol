using System;
using System.Collections.Generic;
using KomeijiRai.ContingencyProtocol.Entities.Units.Bullets;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Controllers.Pools
{
    public class BulletPool : PoolBase<BulletBase>
    {
        public static BulletPool Instance { get; private set; }
        private BulletPool() { }
        private void Awake()
        {
            Instance = this;
        }

        protected override void Init()
        {
            pool = new Dictionary<Type, Stack<BulletBase>>();
            root = new GameObject("BulletRoot").transform;
            typeIdxDict = new Dictionary<Type, int>();
            for (int i = 0; i < prefabs.Count; ++i)
                typeIdxDict[prefabs[i].GetType()] = i;
        }
    }
}
