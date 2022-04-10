using System;
using System.Collections.Generic;
using KomeijiRai.ContingencyProtocol.Entities.Units.Enemies;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Controllers.Pools
{
    public class EnemyPool : PoolBase<EnemyBase>
    {
        public static EnemyPool Instance { get; private set; }
        private EnemyPool() { }
        private void Awake()
        {
            Instance = this;
        }

        protected override void Init()
        {
            pool = new Dictionary<Type, Stack<EnemyBase>>();
            root = new GameObject("EnemyRoot").transform;
            typeIdxDict = new Dictionary<Type, int>();
            for (int i = 0; i < prefabs.Count; ++i)
                typeIdxDict[prefabs[i].GetType()] = i;
        }
    }
}
