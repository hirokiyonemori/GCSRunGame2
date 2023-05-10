using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWPFollowScript : MonoBehaviour
{
    //This class takes care of player movement  path from start to end
    public static PlayerWPFollowScript Instance;
    public GameObject[] waypoints;
    [HideInInspector]public int currentWp = 0;
    float speed = 10.0f;
    float accuracy = 1.0f;
    float rotspeed = 3f;
    public bool run;
    void Start()
    {
        Instance = this;
        run = false;
    }
   
    //Multipe waypoints are placed throughout the scene and players moves from one way point to another and finally reaches the last one
    void LateUpdate()
    {
        if (waypoints.Length == 0 || run==false || PlayerController.Instance.gameOver)
        {
            return;
        }

        Vector3 LootAtGoal = new Vector3(waypoints[currentWp].transform.position.x,
                                         this.transform.position.y,
                                          waypoints[currentWp].transform.position.z);
        Vector3 direction = LootAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotspeed);
        if (direction.magnitude < accuracy)
        {
            currentWp++;
            if (currentWp >= waypoints.Length)
            {
                run = false;
                PlayerController.Instance.anim.SetTrigger("Win");
                GameManager.Instance.levelCompleted = true;
                FindObjectOfType<AudioManager>().Play("win");
                FindObjectOfType<AudioManager>().Stop("background");
                PlayerController.Instance.confettiPs.SetActive(true);
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
