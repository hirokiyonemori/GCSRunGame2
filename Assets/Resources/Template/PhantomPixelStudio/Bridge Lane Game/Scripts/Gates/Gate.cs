using TMPro;
using UnityEngine;

namespace LaneGame.Gates
{
    public enum GateSide
    {
        Left,
        Right
    }
    public abstract class Gate : MonoBehaviour
    {
        public float gateValue;
        public GateSide gate;
        private GateTrigger trigger;
        private TextMeshProUGUI gateText;

        private void Awake()
        {
            trigger = GetComponentInChildren<GateTrigger>();
            gateText = GetComponentInChildren<TextMeshProUGUI>();

            if (trigger != null)
                trigger.GateTriggered += ActivateGate;
            else
            {
                Debug.LogWarning($"{gate} gate trigger not found!");
            }

            gateText.text = string.Format(gateValue >= 0 ? $"+{gateValue}" : $"{gateValue}");
        }

        private void ActivateGate()
        {
            LogSystem.Log($"{gate} gate activated!");
            //DestroyGates();                   //you can uncomment this line if you prefer the gates are destroyed after use. I chose not too.
            PlayerUnitManager.UnitManager.HandleUnits(gateValue);
        }

        private void DestroyGates()
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}