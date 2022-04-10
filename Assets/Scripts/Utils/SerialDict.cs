using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Utils
{
    [System.Serializable]
    public class SerialDict<K, V>
    {
        [System.Serializable]
        public class KV<KEY, VALUE>
        {
            public KEY key;
            public VALUE value;
        }
        [SerializeField] private KV<K, V>[] data;
        public KV<K, V>[] Data => data;
        private Dictionary<K, V> dict;
        public Dictionary<K, V> Dict => dict;
        public void Init()
        {
            dict = new Dictionary<K, V>();
            if (data == null)
                return;
            foreach (var item in data)
            {
                if (!dict.ContainsKey(item.key))
                    dict[item.key] = item.value;
            }
        }
        public void Serialize()
        {
            if (dict == null)
                return;
            var arr = dict.ToArray();
            data = new KV<K, V>[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
                data[i] = new KV<K, V>() { key = arr[i].Key, value = arr[i].Value };
        }
    }
}
