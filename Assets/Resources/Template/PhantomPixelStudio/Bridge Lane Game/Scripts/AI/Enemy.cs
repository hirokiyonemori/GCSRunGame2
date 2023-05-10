using UnityEngine;
using UnityEngine.AI;

namespace LaneGame.AI
{
    public class Enemy : MonoBehaviour
    {
        //敵がプレイヤーを '見る'ことができる範囲
        public float detectionRadius = 100f;

        private SphereCollider sphereCollider;
        private Vector3 playerPos;
        private GameObject player;
        private NavMeshAgent agent;
        private bool seesPlayer;

        private void Start()
        {
            //各種参照を取得
            agent = GetComponent<NavMeshAgent>();
            sphereCollider = GetComponent<SphereCollider>();
            player = GameObject.Find("Player Root"); //プレイヤールートオブジェクトの名前が変更された場合は、ここで変更する必要があります。
            if (player == null)
                Debug.LogError("Enemy unable to find player object..."); //プレイヤーが見つからなかった場合のエラー処理。タグとオブジェクト名を確認してください。
            sphereCollider.radius = detectionRadius; //Sphere Colliderの半径を検出半径と一致させる
        }

        private void Update()
        {
            if (seesPlayer && player != null) //毎フレーム、プレイヤーを見ることができるかどうかをチェックし、プレイヤーがまだ生きているかどうかを確認する
            {
                playerPos = player.transform.position; //ナビメッシュエージェントの目的地を新しいプレイヤーの位置で更新
                agent.SetDestination(playerPos);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //スフィアコライダーに対するトリガーを処理し、プレイヤーがコライダーに入った場合、seesPlayerブールをtrueに変更します
            if (other.CompareTag("Player"))
            {
                seesPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //スフィアコライダーに対するトリガーを処理し、プレイヤーが検出範囲から出た場合、seesPlayerブールをfalseに変更します
            if (other.CompareTag("Player"))
            {
                seesPlayer = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            //エディタで敵を選択するためのヘルパーメソッド。このメソッドを使用すると、検出半径を確認できます
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}