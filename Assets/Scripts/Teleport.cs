using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class Teleport : MonoBehaviour
    {

        public void Start()
        {
            Teleport teleportScript = GetComponent<Teleport>();
            teleportScript.TeleportObject(transform);
        }

        public void TeleportObject(Transform objTransform)
        {
            Vector3 newPosition = objTransform.position;
            newPosition.x = -newPosition.x;
            // 可根據需要設置其他軸向
            // newPosition.y = -newPosition.y;
             newPosition.z = -newPosition.z;

            Quaternion newRotation = objTransform.rotation;
            newRotation.y = -newRotation.y;
            // 可根據需要設置其他軸向
            // newRotation.x = -newRotation.x;
            newRotation.z = -newRotation.z;

            objTransform.position = newPosition;
            objTransform.rotation = newRotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                Transform objTransform = transform;
                TeleportObject(objTransform);
            }
            if (other.CompareTag("Cross"))
            {
                Transform objTransform = transform;
                TeleportObject(objTransform);
            }

        }
    }
}
