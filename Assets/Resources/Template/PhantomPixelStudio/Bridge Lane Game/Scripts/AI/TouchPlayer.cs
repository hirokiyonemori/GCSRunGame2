using UnityEngine;

namespace LaneGame.AI
{
    public class TouchPlayer : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //if any enemy touches a player object ,we'll call the unit Manager to subtract 1 from the player count, then we destroy the enemy
                PlayerUnitManager.UnitManager.HandleUnits(-1);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}