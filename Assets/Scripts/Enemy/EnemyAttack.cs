using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LaneGame
{
    public class EnemyAttack : MonoBehaviour
    {

        //public async UniTaskVoid AttackPlayer()
        public Animator animator;
        public EnemyStatus enemyStatus;
        public event UnityAction OnFinishLineTouch = delegate { };


        private float attackDelay = 0.5f;
        private float downDelay = 2.5f;
        private int damage = 10;
        private int hp = 100;
        private int playerDownCount = 1;

        private void Start()
        {
            if (GameManager.Instance.HardMode)
            {
                hp = 200;
            }
            else
            {
                hp = 100;
            }
            enemyStatus.StartUp(hp);
        }

        private IEnumerator BattleStart()
        {
            yield return AttackPlayer();
        }

        public IEnumerator AttackPlayer()
        {
            int hp = enemyStatus.GetHp();

            while (hp > 0)
            {
                yield return BattleStart(damage);
                hp = enemyStatus.GetHp();
                // ��l���炵�A�����Ă����ꍇ�͍U���̃A�N�V����������
                if (hp > 0 && PlayerUnitManager.UnitManager.DeathUnits(-playerDownCount))
                {
                    PlayerUnitManager.UnitManager.AnimationReplay();
                }
                else
                {
                    // �|��Ă���ꍇ�͓G�̍U�������Ȃ�
                    break;
                }


                yield return new WaitForSeconds(attackDelay);

            }
        }


        private IEnumerator BattleStart(int damage)
        {

            int hp = enemyStatus.GetHp();
            hp -= damage;
            enemyStatus.SetHp(hp);
            //LogSystem.Log(" hp " + hp);
            // HP�o�[�̍X�V�����Ȃ�
            //UniTask.DelayFrame(1); // 1�t���[���ҋ@

            if (hp > 0)
            {
                //LogSystem.Log(" mada " + hp);
                //UniTask.DelayFrame(1); // 1�t���[���ҋ@
                //await UniTask.Delay(TimeSpan.FromSeconds(3f));
                //animator.SetBool("Attack", !animator.GetBool("Attack"));
                animator.Play("Punch", 0, 0);
                //LogSystem.Log(" �ҋ@����");
                //BattleStart(10);
            }
            else
            {
                animator.Play("Death", 0, 0);
                yield return new WaitForSeconds(downDelay);

                OnFinishLineTouch();
            }

        }
    }
}

