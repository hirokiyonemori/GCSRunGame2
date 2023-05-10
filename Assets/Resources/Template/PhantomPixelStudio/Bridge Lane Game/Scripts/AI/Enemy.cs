using UnityEngine;
using UnityEngine.AI;

namespace LaneGame.AI
{
    public class Enemy : MonoBehaviour
    {
        //�G���v���C���[�� '����'���Ƃ��ł���͈�
        public float detectionRadius = 100f;

        private SphereCollider sphereCollider;
        private Vector3 playerPos;
        private GameObject player;
        private NavMeshAgent agent;
        private bool seesPlayer;

        private void Start()
        {
            //�e��Q�Ƃ��擾
            agent = GetComponent<NavMeshAgent>();
            sphereCollider = GetComponent<SphereCollider>();
            player = GameObject.Find("Player Root"); //�v���C���[���[�g�I�u�W�F�N�g�̖��O���ύX���ꂽ�ꍇ�́A�����ŕύX����K�v������܂��B
            if (player == null)
                Debug.LogError("Enemy unable to find player object..."); //�v���C���[��������Ȃ������ꍇ�̃G���[�����B�^�O�ƃI�u�W�F�N�g�����m�F���Ă��������B
            sphereCollider.radius = detectionRadius; //Sphere Collider�̔��a�����o���a�ƈ�v������
        }

        private void Update()
        {
            if (seesPlayer && player != null) //���t���[���A�v���C���[�����邱�Ƃ��ł��邩�ǂ������`�F�b�N���A�v���C���[���܂������Ă��邩�ǂ������m�F����
            {
                playerPos = player.transform.position; //�i�r���b�V���G�[�W�F���g�̖ړI�n��V�����v���C���[�̈ʒu�ōX�V
                agent.SetDestination(playerPos);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //�X�t�B�A�R���C�_�[�ɑ΂���g���K�[���������A�v���C���[���R���C�_�[�ɓ������ꍇ�AseesPlayer�u�[����true�ɕύX���܂�
            if (other.CompareTag("Player"))
            {
                seesPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //�X�t�B�A�R���C�_�[�ɑ΂���g���K�[���������A�v���C���[�����o�͈͂���o���ꍇ�AseesPlayer�u�[����false�ɕύX���܂�
            if (other.CompareTag("Player"))
            {
                seesPlayer = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            //�G�f�B�^�œG��I�����邽�߂̃w���p�[���\�b�h�B���̃��\�b�h���g�p����ƁA���o���a���m�F�ł��܂�
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}