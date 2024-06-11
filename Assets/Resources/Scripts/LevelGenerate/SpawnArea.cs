using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField]
        private float sizeX;
        [SerializeField]
        private float sizeY;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, sizeY, 0));
        }

        public Vector3 GetRandomPosition()
        {
            return transform.TransformPoint(new Vector3(UnityEngine.Random.Range(-sizeX / 2, sizeX / 2), 
                UnityEngine.Random.Range(-sizeY / 2, sizeY / 2), 0));
        }

        public Vector3 GetCenter()
        {
            return transform.TransformPoint(new Vector3(0, 0, 0));
        }
    }
}
