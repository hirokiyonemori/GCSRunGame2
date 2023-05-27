﻿using Controls;
using UnityEngine;

namespace LaneGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed; //the speed we move forward
        [SerializeField] private float lateralSpeed; //the speed we move side to side
        [SerializeField] private Vector2 moveInput; //this is serialized just so we can see it in the inspector and ensure our controls are being captured
        [SerializeField] private HandleCountdown countdown;
        //private Rigidbody rb;
        private PlayerControls controls;

        public float movableRange = 3.0f;
        private Rigidbody m_Rigidbody;
        private float turnForce = 500.0f;

        public CharacterMovement characterMovement;

        [SerializeField]
        private EnemyAttack enemyAttack;

        [SerializeField]
        private GameObject battleStartUI;

        public float slideThreshold = 50f;

        private bool isSliding = false;
        private Vector2 previousPos;
        private Vector2 currentPos;
        const float LOAD_WIDTH = 6f;

        private void Awake()
        {
            controls = new PlayerControls(); //this can be replaced with your own control scheme
            m_Rigidbody = GetComponent<Rigidbody>();
            countdown.StartGame += GivePlayerSpeed;
            PlayerUnitManager.hasBattleStarted = false;
            PlayerUnitManager.isBattle = false;
            if (ES3.KeyExists("PlayerUnit"))
            {
                int playerStartUnit = ES3.Load<int>("PlayerUnit");
                if (playerStartUnit > 0)
                    PlayerUnitManager.UnitManager.HandleUnits(playerStartUnit);
            }

        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void FixedUpdate()
        {
            // まだスタートしていない
            if (PlayerUnitManager.canStart)
            {
                // 移動中
                if (!PlayerUnitManager.isBattle)
                {
                    HandleLateralMovement();
                    HandleForwardMovement();
                }
                else
                {

                    if (!characterMovement.GetIsMoving() && !PlayerUnitManager.hasBattleStarted)
                    {
                        PlayerUnitManager.UnitManager.AnimationPlay();
                        //await enemyAttack.BattleStart();
                        StartCoroutine(enemyAttack.AttackPlayer());
                        battleStartUI.SetActive(true);
                        AudioClip audio = AudioManager.Instance.seExplosion;
                        AudioManager.Instance.PlaySE(audio);
                        PlayerUnitManager.hasBattleStarted = true;
                        // バトル中は移動を止める

                    }
                    this.m_Rigidbody.velocity = Vector3.zero;
                }

            }

        }

        private void GivePlayerSpeed()
        {
            PlayerUnitManager.canStart = true;
        }
        private void HandleForwardMovement()
        {
            //transform.Translate(Vector3.forward * forwardSpeed * Time.fixedDeltaTime);
        }

        private void HandleLateralMovement()
        {
            //if you look at my controls, I purposefully left the up and down keys unbound and only capture left and right movement.

            //moveInput = controls.Player.Movement.ReadValue<Vector2>();      //if using your own controls, make sure you update the path here
            //moveInput.y = 0; //set the y to 0 to make sure we aren't going up or down on the lane, only left and right
            // m_Rigidbody.velocity = new Vector3((moveInput.x * lateralSpeed), 0f, m_Rigidbody.velocity.z);

            //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


            if ((Input.GetKey(KeyCode.LeftArrow)) && -this.movableRange < this.transform.position.x)
            {
                //LogSystem.Log(" left " + this.transform.position.x );
                //this.rb.AddForce(-this.turnForce, 2, 0);
                this.m_Rigidbody.AddForce(-this.turnForce, 2, 0);
                //this.m_Rigidbody.velocity = transform.forward * forwardSpeed - transform.right * this.turnForce;

            }
            else if ((Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
            {
                //LogSystem.Log(" right " + this.transform.position.x);
                //this.m_Rigidbody.velocity = transform.forward * forwardSpeed + transform.right * this.turnForce;
                //this.rb.AddForce(this.turnForce, 2, 0);
                this.m_Rigidbody.AddForce(this.turnForce, 2, 0);
            }
            else
            {
                //this.m_Rigidbody.velocity = transform.forward * forwardSpeed;
                //m_Rigidbody.MovePosition(transform.position + transform.forward * forwardSpeed * Time.fixedDeltaTime);

                // 移動方向の計算
                // Vector3 moveDirection = transform.forward * forwardSpeed;

                // // 力を加える
                // m_Rigidbody.AddForce(moveDirection, ForceMode.VelocityChange);

                Vector3 movement = new Vector3(0f, 0f, forwardSpeed).normalized * forwardSpeed;

                // キャラクタを前方向に移動させる
                transform.Translate(movement * Time.deltaTime, Space.World);

            }
            if (Input.GetKey(KeyCode.Space))
            {
                //LogSystem.Log(" right " + this.transform.position.x);

                //this.m_Rigidbody.AddForce(new Vector3(0, 2, 0));
            }
            //this.rb.velocity = new Vector3(0, 0, forwardSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                previousPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                // �X���C�v�ɂ��ړ��������擾
                currentPos = Input.mousePosition;
                float diffDistance = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;

                // ���̃��[�J��x���W��ݒ� �����̊O�ɂłȂ��悤��
                float newX = Mathf.Clamp(this.transform.localPosition.x + diffDistance, -movableRange, movableRange);
                //LogSystem.Log(" this.transform.localPosition.x " + this.transform.localPosition.x);
                //LogSystem.Log(" diffDistance " + diffDistance);

                this.transform.localPosition = new Vector3(newX, this.transform.localPosition.y, this.transform.localPosition.z);

                // �^�b�v�ʒu���X�V
                previousPos = currentPos;
            }


        }
    }
}