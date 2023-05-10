using UnityEngine;

namespace LaneGame.AI
{
    public class TouchPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            // プレイヤーと衝突した場合
            if (collision.gameObject.tag == "Player")
            {
                // プレイヤーユニットマネージャーからプレイヤーの数を1減らす
                PlayerUnitManager.UnitManager.HandleUnits(-1);
                //PlayerUnitManager.UnitManager.RemoveUnit(collision.gameObject);
                // 敵のオブジェクトが"Enemy"タグを持っている場合は、敵の親オブジェクトを削除する

                if (this.gameObject.CompareTag("Enemy")){
                    Destroy(gameObject.transform.parent.gameObject);
                }
           
            }
            //LogSystem.Log(" collision.gameObject.tag " + collision.gameObject.tag);
            // 味方と衝突した場合
            if (collision.gameObject.tag == "Fellow" )
            {
                // 味方のオブジェクトを削除する
                // 敵のオブジェクトが"Enemy"タグを持っている場合は、敵の親オブジェクトを削除する
                PlayerUnitManager.UnitManager.RemoveUnit(collision.gameObject);
                if (this.gameObject.CompareTag("Enemy"))
                {
                    Destroy(gameObject.transform.parent.gameObject);
                }

            }
        }
    }
}