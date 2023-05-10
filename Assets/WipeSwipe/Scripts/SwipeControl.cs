using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    //Deals with swipe up control

    public static SwipeControl Instance;
    //Jump 
    public bool tap, swipeUp, swipeLeft, swipeRight, swipeDown;  
    public Vector2 startTouch, swipeDelta;
    public bool isDragging = false;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        tap = swipeUp = swipeDown = swipeLeft = swipeRight= false;
        #region StandaloneInputs 
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset(); //Reset Everything when finger is Up
        }
        #endregion

        #region MobileInputs
        if (Input.touches.Length > 0)//if there are any touches on the screen
        {
            if (Input.touches[0].phase == TouchPhase.Began) //looking at the first touch
            {
                isDragging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) //is the phase either ended or cancelled
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        //Calculate Distance as long as the finger is down
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
        //Did we cross the deadzone
        if (swipeDelta.magnitude > 125)
        {
            //which direction are we swiping in
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
            }
            else
            {
                //Up or down
                if (y > 0)
                {
                    swipeUp = true;
                }
                else
                {
                    swipeDown = true;
                }
                Reset();
            }

        }

    }
    
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
    //Jump Logic
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
   
}
