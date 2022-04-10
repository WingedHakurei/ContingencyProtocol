using System;
using System.Collections.Generic;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Controllers.Pools
{
    public abstract class PoolBase<T> :
        MonoBehaviour, IFactory<T>, IPool<T>
        where T : MonoBehaviour
    {

        protected Dictionary<Type, Stack<T>> pool;
        [SerializeField] protected List<T> prefabs;
        protected Dictionary<Type, int> typeIdxDict;
        protected Transform root;

        private void Start()
        {
            Init();
        }

        protected abstract void Init();

        public T Create(Type type, Transform parent = null)
        {
            T ret = null;
            if (typeIdxDict.ContainsKey(type))
                ret = Instantiate(prefabs[typeIdxDict[type]], parent);
            return ret;
        }
        public T Get(Type type)
        {
            T ret = null;
            if (pool.ContainsKey(type))
            {
                if (pool[type].Count > 0)
                {
                    ret = pool[type].Pop();
                    ret.gameObject.SetActive(true);
                }
                else
                {
                    ret = Create(type, root);
                }
            }
            else
            {
                pool[type] = new Stack<T>();
                ret = Create(type, root);
            }

            return ret;
        }
        public void Collect(T obj)
        {
            obj.gameObject.SetActive(false);
            pool[obj.GetType()].Push(obj);
        }
    }
}
