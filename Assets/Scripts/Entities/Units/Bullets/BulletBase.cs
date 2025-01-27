using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Bullets
{
    public abstract class BulletBase : UnitBase
    {
        protected Rigidbody rb;
        [SerializeField] protected float range;
        [SerializeField] protected int damage;
        private float startTime;
        private float lifeTime;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            lifeTime = range / data.velocity;
        }
        private void OnEnable()
        {
            startTime = Time.time;
            if (rb != null)
                rb.linearVelocity = Vector3.zero;

        }

        private void Update()
        {
            if (Time.time - startTime >= lifeTime)
                TakeDamage(curHP);
        }
        private void FixedUpdate()
        {
            Move();
        }

        public void Init(Vector3 position, Vector3 forward)
        {
            transform.position = position;
            transform.forward = forward;
            curHP = data.hp;
        }

        protected abstract void Move();
    }
}
