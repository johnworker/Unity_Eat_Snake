﻿using UnityEngine;
using System.Collections;

namespace Lancelot
{
    public class SpawnObject : MonoBehaviour
    {

        public Vector3 center;
        public Vector3 size;

        public void SpawnFood()
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2),Random.Range(-size.z / 2, size.z / 2));
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(center, size);
        }

    }
}