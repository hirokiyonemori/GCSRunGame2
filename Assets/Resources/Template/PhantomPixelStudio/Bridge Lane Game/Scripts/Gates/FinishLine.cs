using UnityEngine;
using UnityEngine.Events;

namespace LaneGame
{
        public class FinishLine : MonoBehaviour
        {
                public event UnityAction OnFinishLineTouch = delegate { };
                private void OnCollisionEnter(Collision collision)
                {
                        if (collision.gameObject.CompareTag("Player"))
                        {
                                OnFinishLineTouch();
                        }
                }
        }
}