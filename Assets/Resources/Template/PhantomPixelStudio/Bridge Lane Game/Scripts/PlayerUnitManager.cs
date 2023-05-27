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
        [SerializeField] private List<GameObject> playerObjectList = new List<GameObject>();

        public event UnityAction GameOver = delegate { };
        [SerializeField] private Animator playerAnimator;
        public static bool canStart = false;
        public static bool isBattle = false;
        public static bool hasBattleStarted = false;

        private void Awake()
        {
            canStart = false;
            //isBattle = false;
            //deathCnt = 0;
            playerUnitCount.value = 1;
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
                LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
            }
        }

        private void RemoveUnits(float value)
        {
            if (playerObjectList.Count > 1)                                                                             //check if we have clones
            {
                if (playerObjectList.Count > (value * -1f))                                                      //if we have clones, do we have more clones than the gate value is taking away? (since we're negative, we multiply by -1 to make it positive to check our count
                {
                    //we do have clones so we'll iterate through our value
                    for (var i = 0; i > value; i--)
                    {
                        var index = Random.Range(0, playerObjectList.Count - 1);         //we'll grab a random unit from our list. we subtract 1 here due to Unity's starting number of 0
                        var obj = playerObjectList[index];                                  //we save a variable of the object we chose so we can still access it after we remove it from the list
                        playerObjectList.RemoveAt(index);                                                       //we remove the object from the list
                        Destroy(obj);                                                                                           //then using our saved variable, we destroy it
                        //playerObjectList[i].GetComponentInChildren<Animator>().SetTrigger("Death");
                        playerUnitCount.value--;
                        LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);                                   //subtract from our unit count
                    }
                }
                else
                {
                    //since our clone count is less than our value, we're dead, destroy our main and send the game over signal
                    //Destroy(mainPlayerObject.transform.parent.gameObject);
                    GameOver();
                    foreach (var player in playerObjectList)
                    {
                        Destroy(player);                                                                                           //then using our saved variable, we destroy it
                    }
                    playerObjectList.Clear();
                    //deathCnt = 0;
                    DeleteChildObjects(playerObject.gameObject);

                }
            }
            else
            {
                //we have no clones at all, so we're dead, destroy our main and send the game over signal
                //Destroy(mainPlayerObject.transform.parent.gameObject);
                GameOver();
                foreach (var player in playerObjectList)
                {
                    Destroy(player);                                                                                           //then using our saved variable, we destroy it
                }
                playerObjectList.Clear();
                //deathCnt = 0;
                DeleteChildObjects(playerObject.gameObject);
            }
        }

        private IEnumerator DownStart()
        {


            while (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }
            AudioManager.Instance.PlayBGM(AudioManager.Instance.defeatBgm);
            //gameObject.SetActive(false);

            //we have no clones at all, so we're dead, destroy our main and send the game over signal
            //Destroy(mainPlayerObject.transform.parent.gameObject);
            GameOver();
            foreach (var player in playerObjectList)
            {
                Destroy(player);                                                                                           //then using our saved variable, we destroy it
            }
            playerObjectList.Clear();
            DeleteChildObjects(playerObject.gameObject);
            //            deathCnt = 0;

        }



        public void RemoveUnit(GameObject playerObject)
        {
            if (playerObjectList.Count > 0)
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
                //Destroy(gameObject);
                GameOver();
            }
        }





        public void AnimationPlay()
        {
            playerAnimator.SetBool("Attack", true);
            isBattle = true;
            //we do have clones so we'll iterate through our value
            for (var i = 0; i < playerObjectList.Count; i++)
            {
                playerObjectList[i].GetComponentInChildren<Animator>().SetBool("Attack", true);
            }
        }

        //private int deathCnt = 0;

        public bool DeathUnits(float value)
        {

            //LogSystem.Log(" deathCnt" + deathCnt);
            LogSystem.Log(" .Count " + playerObjectList.Count);
            if (playerObjectList.Count >= 0)                                                                             //check if we have clones
            {
                LogSystem.Log(" value * -1f " + value * -1f);
                //LogSystem.Log(" deathCnt" + deathCnt);
                //if (playerObjectList.Count > deathCnt)                                                      //if we have clones, do we have more clones than the gate value is taking away? (since we're negative, we multiply by -1 to make it positive to check our count
                if (playerObjectList.Count > 0)                                                      //if we have clones, do we have more clones than the gate value is taking away? (since we're negative, we multiply by -1 to make it positive to check our count
                {
                    //we do have clones so we'll iterate through our value
                    for (var i = 0; i > value; i--)
                    {
                        int index = (int)playerObjectList.Count - 1;
                        //int index = (int)playerObjectList.Count - 1 - deathCnt;
                        //deathCnt = deathCnt + (int)(value * -1f);       //we'll grab a random unit from our list. we subtract 1 here due to Unity's starting number of 0
                        LogSystem.Log(" index " + index);

                        LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
                        // LogSystem.Log(" .Count " + playerObjectList.Count);
                        if (index >= 0)
                        {

                            var obj = playerObjectList[index];                                  //we save a variable of the object we chose so we can still access it after we remove it from the list

                            playerObjectList[index].GetComponentInChildren<Animator>().SetBool("Death", true);
                            playerObjectList.RemoveAt(index);
                            //playerObjectList[i].GetComponentInChildren<Animator>().Play("Death", 0, 0); ;
                            //playerObjectList[i].GetComponentInChildren<Animator>().Play("Death");
                        }
                    }
                    return true;
                }
                else
                {
                    playerAnimator.Play("Death");

                    //LogSystem.Log(" .Count " + playerObjectList.Count);
                    LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
                    LogSystem.Log(" 死亡しました " + value * -1f);
                    StartCoroutine(DownStart());
                    return false;
                }
            }
            else
            {
                playerAnimator.Play("Death");
                LogSystem.Log(" playerUnitCount.value " + playerUnitCount.value);
                LogSystem.Log(" 死亡しました　DownStart " + value * -1f);
                // playerAnimator.SetBool("Death", true);
                StartCoroutine(DownStart());
                return false;

            }
        }
        public int GetUnitCount()
        {
            return (int)playerUnitCount.value - 2;

        }

        void DeleteChildObjects(GameObject parentObject)
        {
            foreach (Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void AnimationReplay()
        {
            //LogSystem.Log(" AnimationReplay deathCnt " + deathCnt);
            LogSystem.Log(" AnimationReplay deathCnt " + playerObjectList.Count);

            playerAnimator.Play("Punch", 0, 0); ;


            //we do have clones so we'll iterate through our value
            //for (var i = 0; i < playerObjectList.Count - (deathCnt + 1); i++)
            for (var i = 0; i < playerObjectList.Count; i++)
            {
                playerObjectList[i].GetComponentInChildren<Animator>().Play("Punch", 0, 0);
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