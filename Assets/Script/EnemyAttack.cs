using Cysharp.Threading.Tasks;
using System;
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


        public float attackDelay = 5.0f;
      
        private void Start()
        {
            enemyStatus.SetHp(100);
        }

        public async UniTask BattleStart()
		{
            await AttackPlayer();
        }

        private async UniTask AttackPlayer()
        //private IEnumerator AttackPlayer()
        {

            int hp = enemyStatus.GetHp();
            OnFinishLineTouch();
            while (hp > 0)
            {
                BattleStart(10);
                PlayerUnitManager.UnitManager.HandleUnits(-1);
                PlayerUnitManager.UnitManager.AnimationReplay();
                await UniTask.Delay(TimeSpan.FromSeconds(attackDelay));
                hp = enemyStatus.GetHp();
            }
          

        }


        private void BattleStart(int damage)
        {

            int hp = enemyStatus.GetHp();
            hp -= damage;
            enemyStatus.SetHp(hp);
            LogSystem.Log(" hp " + hp);
            // HPバーの更新処理など
            //UniTask.DelayFrame(1); // 1フレーム待機

            if (hp > 0)
            {
                LogSystem.Log(" mada " + hp);
                //UniTask.DelayFrame(1); // 1フレーム待機
                //await UniTask.Delay(TimeSpan.FromSeconds(3f));
                //animator.SetBool("Attack", !animator.GetBool("Attack"));
                animator.Play("Punch", 0, 0);
                LogSystem.Log(" 待機完了");
                //BattleStart(10);
            }
            else
            {
                animator.Play("Death", 0, 0);
            }

        }
    }
}

