using UnityEngine;

namespace LaneGame
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Lane Game/Scriptable Objects/Float Variable")]
    public class FloatVariableSO : ScriptableObject
    {
        //scriptable object variable that lets us use a singular variable for our unit count anywhere we want.
        public float value;
        public float resetValue;

        //In case we need to reset the variable at the start of the game
        public void ResetVariableAtStart()
        {
            value = resetValue;
        }
    }
}