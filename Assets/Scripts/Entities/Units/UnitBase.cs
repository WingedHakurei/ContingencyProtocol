using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units
{
    public abstract class UnitBase : MonoBehaviour, IDamagable
    {
        [System.Serializable]
        public class Data
        {
            public float velocity;
            public int hp;
        }
        [SerializeField] protected Data data;
        public Data GetData => data;
        protected int curHP;
        public int CurHP => curHP;
        public abstract void TakeDamage(int val = 0);
    }
}
