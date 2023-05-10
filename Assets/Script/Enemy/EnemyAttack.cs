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
            enemyStatus.SetHp(hp);
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
                // 一人減らし、生きていた場合は攻撃のアクションをする
                if ( hp > 0 &&  PlayerUnitManager.UnitManager.DeathUnits(-playerDownCount))
				{
                    PlayerUnitManager.UnitManager.AnimationReplay();
				}
				else
				{
                    // 倒れている場合は敵の攻撃をしない
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
            // HPバーの更新処理など
            //UniTask.DelayFrame(1); // 1フレーム待機

            if (hp > 0)
            {
                //LogSystem.Log(" mada " + hp);
                //UniTask.DelayFrame(1); // 1フレーム待機
                //await UniTask.Delay(TimeSpan.FromSeconds(3f));
                //animator.SetBool("Attack", !animator.GetBool("Attack"));
                animator.Play("Punch", 0, 0);
                //LogSystem.Log(" 待機完了");
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

