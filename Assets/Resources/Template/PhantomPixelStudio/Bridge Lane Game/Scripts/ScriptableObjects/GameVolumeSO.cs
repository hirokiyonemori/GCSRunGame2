using UnityEngine;

namespace LaneGame
{
    [CreateAssetMenu(fileName = "Game Volume", menuName = "Lane Game/Scriptable Objects/Game Volume")]
    public class GameVolumeSO : ScriptableObject
    {
        //scriptable object variable that lets us use a singular variable for our games volume from anywhere, main menu, options etc
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
    }
}