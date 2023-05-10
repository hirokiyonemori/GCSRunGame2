using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //This Script contains logic for the progress bar and also calculates the progress 
    
    public static ProgressBar Instance;

    GameObject player;
    public Image mask;
    public int maximum;  //Max progress can be
    public int current =0;
    float totalSidtance;
    float currentdist;
    
    public void Start()
    {
        Instance = this;
        player= GameObject.FindGameObjectWithTag("Player");
        //calculates the total distance between player and destination
        totalSidtance = Vector3.Distance(player.GetComponent<PlayerWPFollowScript>().waypoints[0].transform.position, player.transform.position);

        for(int i=0; i< player.GetComponent<PlayerWPFollowScript>().waypoints.Length-1; i++)
        {
            totalSidtance = totalSidtance + Vector3.Distance(player.GetComponent<PlayerWPFollowScript>().waypoints[i + 1].transform.position,
              player.GetComponent<PlayerWPFollowScript>().waypoints[i].transform.position);
            
        }
       
    }
    void Update()
    {
        //calculates current distance between player and destination
        
          if(player.GetComponent<PlayerWPFollowScript>().currentWp != player.GetComponent<PlayerWPFollowScript>().waypoints.Length)
            {
                currentdist = Vector3.Distance(player.GetComponent<PlayerWPFollowScript>().waypoints[player.GetComponent<PlayerWPFollowScript>().currentWp].transform.position,
               player.transform.position);
                for (int i = player.GetComponent<PlayerWPFollowScript>().currentWp; i < player.GetComponent<PlayerWPFollowScript>().waypoints.Length - 1; i++)
                {
                    currentdist = currentdist + Vector3.Distance(player.GetComponent<PlayerWPFollowScript>().waypoints[i + 1].transform.position,
                     player.GetComponent<PlayerWPFollowScript>().waypoints[i].transform.position);

                }
            }
       
    //finds percent value of current distance out of total distance
        float percentage= (totalSidtance - currentdist) * 100 / totalSidtance;
        //assigning percentage to the current value
        current =  (int)(percentage);
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        //updating the fill Amount of the progress bar
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
       
    }
}
