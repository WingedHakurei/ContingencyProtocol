using System.Collections.Generic;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Robots
{
    public class Turret : MonoBehaviour
    {
        private Animator animator;
        private List<Gun> guns;
        private int gunIdx = 0;
        private void Start()
        {
            animator = GetComponent<Animator>();
            guns = new List<Gun>(GetComponentsInChildren<Gun>());
        }
        public void LookAt(Vector3 target)
        {
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        }

        public void Fire()
        {
            animator.SetBool("Fire", true);
        }
        public void HoldFire()
        {
            animator.SetBool("Fire", false);
        }
        public void GunFire()
        {
            guns[gunIdx].Fire();
            ++gunIdx;
            if (gunIdx == guns.Count)
                gunIdx = 0;
        }

    }
}
