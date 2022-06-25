using System.Linq;
using KomeijiRai.ContingencyProtocol.Controllers;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Enemies
{
    public class CapsuleEnemy : EnemyBase
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
            if (curNode == targetNode || targetNode.Neighbors.Contains(curNode))
                PlayerInputManager.Instance.PlayerRobot.TakeDamage(damage);
        }
    }
}
