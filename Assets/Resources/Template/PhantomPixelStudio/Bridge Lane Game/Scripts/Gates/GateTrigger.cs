using UnityEngine;
using UnityEngine.Events;

namespace LaneGame.Gates
{
    public class GateTrigger : MonoBehaviour
    {
        public event UnityAction GateTriggered = delegate { };

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Gate signal sent... ");
                GateTriggered();
            }
        }
    }
}