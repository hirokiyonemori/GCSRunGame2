using Controls;
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
        private bool canStart = false;
        public float movableRange = 3.0f;
        public Rigidbody m_Rigidbody;
        private float turnForce = 500.0f;

        private void Awake()
        {
            controls = new PlayerControls(); //this can be replaced with your own control scheme
          //  rb = GetComponent<Rigidbody>();
            countdown.StartGame += GivePlayerSpeed;
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
            if (canStart)
            {
                HandleLateralMovement();
                HandleForwardMovement();
            }
            
        }

        private void GivePlayerSpeed()
        {
            canStart = true;
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
                Debug.Log(" left " + this.transform.position.x );
                this.m_Rigidbody.AddForce(-this.turnForce, 0, 0);

            }
            else if ((Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
            {
                Debug.Log(" right " + this.transform.position.x);
                this.m_Rigidbody.AddForce(this.turnForce, 0, 0);
            }
            this.m_Rigidbody.velocity = new Vector3(0, 0, forwardSpeed);

            

        }
    }
}