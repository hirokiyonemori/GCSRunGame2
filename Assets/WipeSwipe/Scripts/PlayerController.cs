using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //This class handles player

    public static PlayerController Instance ;

    private Rigidbody2D rb;
    public Animator anim;

    public GameObject confettiPs;
    public GameObject cam;

    public bool holding = false;
    public bool start = false;
    public bool gameOver = false;
    private bool isGrounded = true;
    public bool run;

    public float jumpForce;

    public Button jumpButton;

    public GameObject stopButton;

    public GameObject runButton;


    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        Time.timeScale = 1;
        run = false;
        jumpButton.onClick.AddListener(() => Jump());

        stopButton.GetComponent<Button>().onClick.AddListener(() => Stop());
        runButton.GetComponent<Button>().onClick.AddListener(() => RunPlayer());
        runButton.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }
        

        //Making the player run for first time on click on screen
        if (Input.GetMouseButtonDown(0) && run == false  && start==false && PlayerPrefs.GetInt("firstTime") == 1)    {
            RunPlayer();
            start = true;
            GameManager.Instance.taptoStartText.SetActive(false);

        }
        //making player run every frame if these conditions are true
        else if (run == false && holding == false && start==true) 
        {
            RunPlayer();
        }

        //スワイプ処理
        //if (SwipeControl.Instance.swipeUp)
        if (SwipeControl.Instance.tap)
        {
//            Jump();
        }



    }
    //when Tapped to run player this method is called
   public void RunPlayer()
    {
        anim.SetTrigger("run");
        PlayerWPFollowScript.Instance.run = true;
        run = true;
        //走るボタンを日活性にする
        runButton.SetActive(false);
        stopButton.SetActive(true);
    }
    //Called on swipe up jump
    public void Jump()
    {
        if (isGrounded && !gameOver)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            anim.SetTrigger("jump");
            isGrounded = false;
            Invoke("RunAfterJump", 1f);
            FindObjectOfType<AudioManager>().Play("jump");

        }
       
    }
    //Called on player hold btn
    public void Stop()
    {
        if (!gameOver && run==true)
        {
            anim.SetTrigger("idle");
            PlayerWPFollowScript.Instance.run = false;
            //走るボタンをボタンを表示する
            runButton.SetActive(true);
            stopButton.SetActive(false);
        }
    }

    public void RunAfterJump()
    {
         isGrounded = true;
    }

    //When player hits and obstacle
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("obstacle") && gameOver==false)
        {
            HitbyCollider(0);
        }
    }
    //When player falls in the empty space/pit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("obstacle") && gameOver == false)
        {
             HitbyCollider(1);
        }
    }
    
    //When player dies eiter hittng by obstacle or falling in pit
    public void HitbyCollider(int val)
    {
        if (val == 0)
        {
            cam.transform.parent = null;
        }
   
        anim.SetTrigger("die");
        run = false;
        gameOver = true;
        PlayerWPFollowScript.Instance.run = false;
        FindObjectOfType<AudioManager>().Play("bump");
        FindObjectOfType<AudioManager>().Stop("background");

        FindObjectOfType<AudioManager>().Play("gameover");
        Invoke("GameOverLate", 3.0f);
        GameManager.Instance.gameOver = true;
    }

    //Pause the game after some time when game is over
    public void GameOverLate()
    {
        Time.timeScale = 0;
    }

  

   
}
