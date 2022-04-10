using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KomeijiRai.ContingencyProtocol.Controllers.Pools;
using KomeijiRai.ContingencyProtocol.Entities.Units.Enemies;
using KomeijiRai.ContingencyProtocol.Maps;
using KomeijiRai.ContingencyProtocol.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KomeijiRai.ContingencyProtocol.Controllers
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }
        private EnemySpawner() { }
        private void Awake()
        {
            Instance = this;
        }
        private List<NodeBase> walkableNodes;
        [SerializeField] private SerialDict<string, int>[] waves;
        private int waveIdx = 0;
        private bool isSpawning = false;
        private int waveRemainingEnemyCount = 0;
        public void DoOnEnemyDeath() => --waveRemainingEnemyCount;
        private Dictionary<string, Type> types;

        private void Start()
        {
            walkableNodes = GridManager.Instance.Nodes.Values.Where(t => !t.IsObstacle).ToList();
            types = new Dictionary<string, Type>();
        }
        private void Update()
        {
            if (!isSpawning && waveIdx < waves.Length && waveRemainingEnemyCount == 0)
                StartCoroutine(CRTSpawnWave(5f));
        }
        private IEnumerator CRTSpawnWave(float dontSpawnDistance)
        {
            isSpawning = true;
            var data = waves[waveIdx].Data;
            foreach (var kv in data)
            {
                waveRemainingEnemyCount += kv.value;
            }
            yield return ConstUtils.WAIT_FOR_1_SEC;
            foreach (var kv in data)
            {
                if (!types.ContainsKey(kv.key))
                    types.Add(kv.key, Type.GetType(ConstUtils.ENEMY_NAMESPACE_NAME + "." + kv.key));
                for (int i = 0; i < kv.value; ++i)
                {
                    NodeBase node = null;
                    while (node == null)
                    {
                        node = walkableNodes[Random.Range(0, walkableNodes.Count)];
                        if (node.DistanceTo(PlayerInputManager.Instance.PlayerRobot.CurNode) < dontSpawnDistance)
                            node = null;
                    }
                    Spawn(types[kv.key], node);
                    yield return ConstUtils.WAIT_FOR_500_MS;
                }
            }
            ++waveIdx;
            isSpawning = false;
        }


        private void Spawn(Type type, NodeBase node)
        {
            EnemyBase enemy = EnemyPool.Instance.Get(type);
            enemy.Init(node);
        }

    }
}
