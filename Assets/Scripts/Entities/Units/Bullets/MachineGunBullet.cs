using KomeijiRai.ContingencyProtocol.Controllers.Pools;
using KomeijiRai.ContingencyProtocol.Entities.Units.Enemies;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Bullets
{
    public class MachineGunBullet : BulletBase
    {
        public override void TakeDamage(int val = 0)
        {
            BulletPool.Instance.Collect(this);
        }
        protected override void Move()
        {
            rb.linearVelocity = transform.forward * data.velocity;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                TakeDamage();
            }
            else if (other.CompareTag("Enemy"))
            {
                TakeDamage();
                EnemyBase enemy = other.GetComponent<EnemyBase>();
                enemy.TakeDamage(damage);
            }
        }
    }
}
