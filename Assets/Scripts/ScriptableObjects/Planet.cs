using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Planet", menuName = "Planet", order = 0)]
    public class Planet : ScriptableObject
    {
        public float surfaceGravity;
        public float radius;
        public Vector3 speed;
        public int offset;
    }
}