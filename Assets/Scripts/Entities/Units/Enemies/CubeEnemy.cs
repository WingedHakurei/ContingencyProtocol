using System.Linq;
using KomeijiRai.ContingencyProtocol.Controllers;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Enemies
{
    public class CubeEnemy : EnemyBase
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Attack()
        {
            animator.SetTrigger("Attacking");
        }
        private void RealAttack()
        {
            if (targetNode == curNode || targetNode.Neighbors.Contains(curNode))
                PlayerInputManager.Instance.PlayerRobot.TakeDamage(damage);
            TakeDamage(curHP);
        }

    }
}
