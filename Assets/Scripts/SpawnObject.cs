using UnityEngine;
using System.Collections;

namespace Lancelot
{
    public class SpawnObject : MonoBehaviour
    {

        public GameObject Foodprefab;

        public Vector3 center;
        public Vector3 size;

        void Start()
        {
            SpawnFood();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
                SpawnFood();
        }

        public void SpawnFood()
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2),Random.Range(-size.z / 2, size.z / 2));

            Instantiate(Foodprefab, pos, Quaternion.identity);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(center, size);
        }

    }
}