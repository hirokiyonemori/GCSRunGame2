using UnityEngine;
using UnityEngine.AI;

namespace LaneGame.AI
{
    public class Enemy : MonoBehaviour
    {
        //how far the enemy can 'see' the player
        public float detectionRadius = 100f;

        private SphereCollider sphereCollider;
        private Vector3 playerPos;
        private GameObject player;
        private NavMeshAgent agent;
        private bool seesPlayer;

        private void Start()
        {
            //references
            agent = GetComponent<NavMeshAgent>();
            sphereCollider = GetComponent<SphereCollider>();
            player = GameObject.Find("Player Root");                                                        //if you change the name of the player root object, be sure to update its change here too
            if (player == null)
                Debug.LogError("Enemy unable to find player object...");            //here we check if the player was Not found. If this is firing, we can check tags and object names
            sphereCollider.radius = detectionRadius;                                                        //we double check the sphere colliders radius matches our detection radius
        }

        private void Update()
        {
            if (seesPlayer && player != null)                                                                   //every frame, we check if we can see player and check to make sure the player is still alive
            {
                playerPos = player.transform.position;                                                      //update the navmesh agents destination with the new player position
                agent.SetDestination(playerPos);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            //this is for the sphere collider. This tracks when the player enters its collider/trigger and changes our seesPlayer bool to true when it does
            if (other.CompareTag("Player"))
            {
                seesPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //this is also for the sphere collider, in case you want the enemy to stop following if the player makes it outside of its detection radius
            if (other.CompareTag("Player"))
            {
                seesPlayer = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            //a helper method for selecting the enemy in the Editor, you can see how far its detection radius is with this method
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}