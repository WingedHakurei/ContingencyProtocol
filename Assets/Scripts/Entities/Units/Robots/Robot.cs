using System;
using System.Collections.Generic;
using KomeijiRai.ContingencyProtocol.Maps;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Robots
{
    public class Robot : UnitBase
    {
        [SerializeField] private Transform modelTrans;
        [SerializeField] private Transform turretsTrans;
        public NodeBase CurNode { get; set; }
        private List<Turret> turrets;
        private int turretIdx = 0;
        private Animator animator;
        private Rigidbody rb;
        private bool isMoving = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = modelTrans.GetComponent<Animator>();
            turrets = new List<Turret>(
                turretsTrans.GetComponentsInChildren<Turret>()
            );
            for (int i = 1; i < turrets.Count; ++i)
                turrets[i].gameObject.SetActive(false);
            curHP = data.hp;
        }
        public void Move(Vector3 dir)
        {
            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("IsMoving", true);
            }
            rb.velocity = dir * data.velocity;
            animator.SetFloat("InputX", dir.x);
            animator.SetFloat("InputY", dir.z);
        }
        public void Hold()
        {
            if (isMoving)
            {
                rb.velocity = Vector3.zero;
                isMoving = false;
                animator.SetBool("IsMoving", false);
            }
        }
        public void TurretLookAt(Vector3 target)
        {
            turrets[turretIdx].LookAt(target);
        }
        public void TurretFire()
        {
            turrets[turretIdx].Fire();
        }
        public void TurretHoldFire()
        {
            turrets[turretIdx].HoldFire();
        }
        public void SwitchTurret(bool isForward)
        {
            if (turrets.Count <= 1)
                return;
            turrets[turretIdx].gameObject.SetActive(false);
            if (isForward)
            {
                ++turretIdx;
                if (turretIdx == turrets.Count)
                    turretIdx = 0;
            }
            else
            {
                --turretIdx;
                if (turretIdx == -1)
                    turretIdx = turrets.Count - 1;
            }
            turrets[turretIdx].gameObject.SetActive(true);
        }

        public event Action OnRobotDeath;
        public override void TakeDamage(int val)
        {
            curHP -= val;
            if (curHP <= 0)
                Die();
        }
        private void Die()
        {
            OnRobotDeath.Invoke();
            gameObject.SetActive(false);
        }

    }
}
