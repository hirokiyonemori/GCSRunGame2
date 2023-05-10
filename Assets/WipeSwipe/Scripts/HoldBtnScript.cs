using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
public class HoldBtnScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //This class handles the event of pointer up and down of hold button  
   public void OnPointerDown(PointerEventData eventData)
    {
      PlayerController.Instance.Stop();
        PlayerController.Instance.holding = true;
        PlayerController.Instance.run = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        PlayerController.Instance.holding = false;
    }
}