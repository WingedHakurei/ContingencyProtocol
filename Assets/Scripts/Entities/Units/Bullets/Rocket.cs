using KomeijiRai.ContingencyProtocol.Controllers.Pools;
using KomeijiRai.ContingencyProtocol.Entities.Units.Enemies;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Bullets
{
    public class Rocket : BulletBase
    {
        public override void TakeDamage(int val)
        {
            curHP -= val;
            if (curHP <= 0)
                BulletPool.Instance.Collect(this);
        }
        protected override void Move()
        {
            rb.AddForce(transform.forward * data.velocity);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                TakeDamage(1);
            }
            else if (other.CompareTag("Enemy"))
            {
                TakeDamage(1);
                EnemyBase enemy = other.GetComponent<EnemyBase>();
                enemy.TakeDamage(damage);
            }
        }
    }
}
