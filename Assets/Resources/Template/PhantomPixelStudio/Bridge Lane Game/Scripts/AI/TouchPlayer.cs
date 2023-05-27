using UnityEngine;

namespace LaneGame.AI
{
    public class TouchPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            // �v���C���[�ƏՓ˂����ꍇ
            if (collision.gameObject.tag == "Player")
            {
                // �v���C���[���j�b�g�}�l�[�W���[����v���C���[�̐���1���炷
                PlayerUnitManager.UnitManager.HandleUnits(-1);
                //PlayerUnitManager.UnitManager.RemoveUnit(collision.gameObject);
                // �G�̃I�u�W�F�N�g��"Enemy"�^�O�������Ă���ꍇ�́A�G�̐e�I�u�W�F�N�g���폜����
                AudioManager.Instance.PlaySE(AudioManager.Instance.waterSe);

                // 実装予定だった
                // if (this.gameObject.CompareTag("Enemy"))
                // {
                //     Destroy(gameObject.transform.parent.gameObject);
                // }

            }
            //LogSystem.Log(" collision.gameObject.tag " + collision.gameObject.tag);
            // �����ƏՓ˂����ꍇ
            if (collision.gameObject.tag == "Fellow")
            {
                // �����̃I�u�W�F�N�g���폜����
                // �G�̃I�u�W�F�N�g��"Enemy"�^�O�������Ă���ꍇ�́A�G�̐e�I�u�W�F�N�g���폜����
                PlayerUnitManager.UnitManager.RemoveUnit(collision.gameObject);
                AudioManager.Instance.PlaySE(AudioManager.Instance.waterSe);
                //// 実装予定だった
                // if (this.gameObject.CompareTag("Enemy"))
                // {
                //     Destroy(gameObject.transform.parent.gameObject);
                // }

            }
        }
    }
}