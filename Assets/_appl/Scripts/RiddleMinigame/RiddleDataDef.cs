using UnityEngine;

namespace RiddleMinigame
{
    [CreateAssetMenu(fileName = "RiddleDataDef", menuName = "Scriptable Objects/RiddleDataDef")]
    public class RiddleDataDef : ScriptableObject
    {
        public string riddleWorld;
        public Sprite riddleImage;
    }
}