using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LaneGame
{
    public class PlayerUnitManager : MonoBehaviour
    {
        public static PlayerUnitManager UnitManager { get; private set; }
        public float spawnRadius;
        public Transform playerObject;
        public GameObject mainPlayerObject;
        [SerializeField] private FloatVariableSO playerUnitCount;
        [SerializeField] private  List<GameObject> playerObjectList = new List<GameObject>();
        
        public event UnityAction GameOver = delegate { };
        [SerializeField] private Animator playerAnimator;
        public static bool canStart = false;
        public static bool isBattle = false;
        public static bool hasBattleStarted = false;

        private void Awake()
        {
            canStart = false;
            //isBattle = false;
            //the following sets up our singleton pattern
            if (UnitManager != null && UnitManager != this)
                Destroy(this);
            else
                UnitManager = this;
        }
        
        public void HandleUnits(float value)
        {
            //this checks its given value and if its positive or zero, we add that many units. If its negative, we remove that many units
            if (value >= 0)
                AddUnits(value);
            else
                RemoveUnits(value);
        }
        private void AddUnits(float value)
        {
            for (var i = 0; i < value; i++) //starting from 0, we add a copy of our main unit until we reach our value number
            {
                //you could inline these variables, I chose not to as I am designing this to be beginner friendly and felt this is easier to read and understand
                var playerClone = mainPlayerObject; //the object we are cloning
                var spawnPos = GetRandomPositionAroundObject(playerObject); //a random position around our main object
                var rotation = Quaternion.identity; //the rotation of our clone when we spawn it
                var parentObj = playerObject; //the object we'll use to parent the clones onto, to keep our hierarchy organized
                
                var objToSpawn = Instantiate(playerClone, spawnPos, rotation, parentObj);
                playerObjectList.Add(objToSpawn); //add our clone to the list
                playerUnitCount.value++; //increment our unit count
            }
        }
        
        private void RemoveUnits(float value)
        {
            if (playerObjectList.Count > 1)                                                                             //check if we have clones
            {
                if (playerObjectList.Count > (value *-1f))                                                      //if we have clones, do we have more clones than the gate value is taking away? (since we're negative, we multiply by -1 to make it positive to check our count
                { 
                    //we do have clones so we'll iterate through our value
                    for (var i = 0; i > value; i--)
                    {
                        var index = Random.Range(0, playerObjectList.Count -1);         //we'll grab a random unit from our list. we subtract 1 here due to Unity's starting number of 0
                        var obj = playerObjectList[index];                                  //we save a variable of the object we chose so we can still access it after we remove it from the list
                        playerObjectList.RemoveAt(index);                                                       //we remove the object from the list
                        Destroy(obj);                                                                                           //then using our saved variable, we destroy it
                        //playerObjectList[i].GetComponentInChildren<Animator>().SetTrigger("Death");
                        playerUnitCount.value--;                                                                        //subtract from our unit count
                    }
                }
                else
                {
                    //since our clone count is less than our value, we're dead, destroy our main and send the game over signal
                    Destroy(mainPlayerObject.transform.parent.gameObject);
                    GameOver();
                }
            }
            else
            {
                //we have no clones at all, so we're dead, destroy our main and send the game over signal
                Destroy(mainPlayerObject.transform.parent.gameObject);
                GameOver();
            }
        }

        private IEnumerator DownStart()
        {


            while (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            //gameObject.SetActive(false);

            //we have no clones at all, so we're dead, destroy our main and send the game over signal
            Destroy(mainPlayerObject.transform.parent.gameObject);
            GameOver();
        }



        public void RemoveUnit(GameObject playerObject)
        {
            if (playerObjectList.Count > 1)
            {
                
                var index = playerObjectList.IndexOf(playerObject);    // プレイヤーの GameObject がリストの何番目にあるかを調べる
                //LogSystem.Log("  index" + index);
                if (index >= 0)
                {
                    playerObjectList.RemoveAt(index);               // プレイヤーの GameObject をリストから削除する
                    Destroy(playerObject);                           // プレイヤーの GameObject を破棄する
                    playerUnitCount.value--;                         // プレイヤーのユニット数を減らす
                }
            }
            else
            {
                Destroy(mainPlayerObject.transform.parent.gameObject);
                GameOver();
            }
        }





        public void AnimationPlay()
        {
            playerAnimator.SetBool("Attack",true);
            isBattle = true; 
            if (playerObjectList.Count > 1)                                                                             //check if we have clones
            {
                
                //we do have clones so we'll iterate through our value
                for (var i = 0; i < playerObjectList.Count; i++)
                {
                    playerObjectList[i].GetComponentInChildren<Animator>().SetBool("Attack",true);
                }
                
            }
        }

        public bool DeathUnits(float value)
        {
            if (playerUnitCount.value > 1)                                                                             //check if we have clones
            {
                LogSystem.Log(" value * -1f " + value * -1f);
                if (playerUnitCount.value > (value * -1f))                                                      //if we have clones, do we have more clones than the gate value is taking away? (since we're negative, we multiply by -1 to make it positive to check our count
                {
                    //we do have clones so we'll iterate through our value
                    for (var i = 0; i > value; i--)
                    {
                        int index = (int)playerUnitCount.value - 2 ;         //we'll grab a random unit from our list. we subtract 1 here due to Unity's starting number of 0
                        LogSystem.Log(" index " + index);
                        LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
                        var obj = playerObjectList[index];                                  //we save a variable of the object we chose so we can still access it after we remove it from the list
                        //playerObjectList.RemoveAt(index);                                                       //we remove the object from the list
                        //Destroy(obj);                                                                                           //then using our saved variable, we destroy it
                        
                        playerObjectList[index].GetComponentInChildren<Animator>().SetBool("Death",true);
                        //playerObjectList[i].GetComponentInChildren<Animator>().Play("Death", 0, 0); ;
                        //playerObjectList[i].GetComponentInChildren<Animator>().Play("Death");
                        playerUnitCount.value--;                                                                        //subtract from our unit count
                    }
                    return true;
                }
                else
                {
                    playerAnimator.Play("Death");
                    LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
                    LogSystem.Log(" 脂肪しました " + value * -1f);
                    StartCoroutine(DownStart());
                    return false;
                }
            }
            else
            {
                playerAnimator.Play("Death");
                LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
                LogSystem.Log(" 脂肪しました　DownStart " + value * -1f);
                // playerAnimator.SetBool("Death", true);
                StartCoroutine(DownStart());
                return false;

            }
        }
        public int GetUnitCount()
		{
            return (int)playerUnitCount.value-2;

        }

        public void AnimationReplay()
        {
            playerAnimator.Play("Punch", 0, 0); ;
            if (playerObjectList.Count > 1)                                                                             //check if we have clones
            {

                //we do have clones so we'll iterate through our value
                for (var i = 0; i < playerUnitCount.value; i++)
                {
                    playerObjectList[i].GetComponentInChildren<Animator>().Play("Punch", 0, 0);
                }

            }
        }


        
        private Vector3 GetRandomPositionAroundObject(Transform t)
        {
            Vector3 offset = Random.insideUnitSphere * spawnRadius; //get a random position within our spawn radius value in a sphere around our transform
            offset.y = transform.position.y; //set the y value to 0 so we're always on the ground
            Vector3 spawnPos = t.position + offset; //add the offset to the position
            return spawnPos; //return our new position
        }
    }
}