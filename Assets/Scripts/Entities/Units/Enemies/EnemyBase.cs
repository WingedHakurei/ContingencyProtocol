using System;
using System.Collections;
using System.Collections.Generic;
using KomeijiRai.ContingencyProtocol.Controllers;
using KomeijiRai.ContingencyProtocol.Controllers.Pools;
using KomeijiRai.ContingencyProtocol.Maps;
using KomeijiRai.ContingencyProtocol.Utils;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Entities.Units.Enemies
{
    public abstract class EnemyBase : UnitBase
    {
        protected Rigidbody rb;
        public virtual void Init(NodeBase curNode)
        {
            this.curNode = curNode;
            this.targetNode = null;
            transform.position = this.curNode.Coord.RealPosition;
            curHP = data.hp;
            hasReachedNext = true;
            path = null;
            state = State.Moving;
        }
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            displacementClip = data.velocity / 100f;
            animator = GetComponent<Animator>();
        }

        protected virtual void FixedUpdate()
        {
            if (state == State.Idle)
                return;
            if (hasReachedNext)
            {
                var playerNode = PlayerInputManager.Instance.PlayerRobot.CurNode;
                if (curNode != playerNode)
                {
                    animator.SetTrigger("Moving");
                    state = State.Moving;
                    Navigate(playerNode);
                }
                else
                {
                    state = State.Attacking;
                    Attack();
                }
            }

        }
        protected State state = State.Moving;
        protected Animator animator;
        public void DoOnPlayerDeath()
        {
            state = State.Idle;
            animator.SetTrigger("Idle");
        }
        public void DoOnPlayerReborn()
        {
            state = State.Moving;
        }
        [SerializeField] protected int damage;
        protected abstract void Attack();

        #region Pathfinding
        protected NodeBase curNode;
        protected NodeBase targetNode;
        protected bool hasReachedNext = true;
        protected float displacementClip;
        protected Queue<NodeBase> path;
        protected void Navigate(NodeBase targetNode)
        {
            if (this.targetNode != targetNode)
            {
                path = new Queue<NodeBase>(AStarPathfinding.FindPath(curNode, targetNode));
                this.targetNode = targetNode;
            }

            if (path.Count > 0)
            {
                hasReachedNext = false;
                if (path.Count > 0)
                {
                    var nextNode = path.Dequeue();
                    StartCoroutine(CRTMove(nextNode));
                }
                else
                {
                    hasReachedNext = true;
                }
            }
        }
        protected IEnumerator CRTMove(NodeBase nextNode)
        {
            if (!hasReachedNext)
            {
                for (float percent = 0f; percent < 1f; percent += displacementClip)
                {
                    rb.position = Vector3.Lerp(
                        curNode.Coord.RealPosition,
                        nextNode.Coord.RealPosition,
                        percent
                    );
                    yield return ConstUtils.WAIT_FOR_10_MS;
                }
                rb.position = nextNode.Coord.RealPosition;
                curNode = nextNode;
                yield return ConstUtils.WAIT_FOR_500_MS;
                hasReachedNext = true;
            }
        }
        #endregion
        public override void TakeDamage(int val)
        {
            curHP -= val;
            if (curHP <= 0)
                Die();
        }
        protected event Action OnEnemyDeath;
        protected void Die()
        {
            StopAllCoroutines();
            OnEnemyDeath.Invoke();
            animator.Play(0, 0, 0);
            animator.Update(0f);
            EnemyPool.Instance.Collect(this);
        }
        protected void OnEnable()
        {
            OnEnemyDeath += EnemySpawner.Instance.DoOnEnemyDeath;
            PlayerInputManager.Instance.PlayerRobot.OnRobotDeath += DoOnPlayerDeath;
        }
        protected void OnDisable()
        {
            OnEnemyDeath -= EnemySpawner.Instance.DoOnEnemyDeath;
            PlayerInputManager.Instance.PlayerRobot.OnRobotDeath -= DoOnPlayerDeath;
        }



        public enum State
        {
            Idle,
            Moving,
            Attacking
        }
    }
}
