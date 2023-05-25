using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace LaneGame
{
    public class FinishLine : MonoBehaviour
    {

        public event UnityAction OnFinishLineTouch = delegate { };

        // delegate�̐錾
        delegate void OnBattleEvent(int hp);



        public PlayerMovement playerMovement;

        private void Start()
        {

            LogSystem.isdebug = true;
            LogSystem.Log(" GetStage " + GameManager.Instance.GetStage());

        }
        private void Update()
        {

        }

        private void OnTriggerEnter(Collider collision)
        {

            //LogSystem.Log(" collision.gameObject.tag " + collision.gameObject.tag);
            //LogSystem.Log(" PlayerUnitManager.isBattle " + PlayerUnitManager.isBattle);

            if ((collision.gameObject.tag == "Fellow" || collision.gameObject.tag == "Player") && !PlayerUnitManager.isBattle)
            {
                PlayerUnitManager.isBattle = true;

                playerMovement.characterMovement.StartMoving();

            }
        }




        /*
        private void OnCollisionStay(Collision collision)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                animator.SetBool("Attack", true);
                enemyStatus.SetHp(enemyStatus.GetHp() - 20);
                if (enemyStatus.GetHp() <= 0)
                {
                    OnFinishLineTouch();
                }
            }
        }*/
    }
}