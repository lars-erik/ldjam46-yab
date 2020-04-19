using UnityEngine;

namespace Assets.Scripts
{
    public enum Alignment
    {
        Good,
        Evil,
        Neutral
    }

    public class FoodSpecs : MonoBehaviour
    {
        public int Health;
        public Alignment Alignment;
    }
}
