using TMPro;
using UnityEngine;

namespace LaneGame
{
    public class HandleUnitCount : MonoBehaviour
    {
        [SerializeField] private FloatVariableSO unitCount;
        [SerializeField] private TextMeshProUGUI countText;

        private void Start()
        {
            unitCount.ResetVariableAtStart(); //reset value to the starting unit count of 1
        }

        private void Update()
        {
            //this simply updates our game text with the player object count...
            countText.text = unitCount.value.ToString();
        }
    }
}