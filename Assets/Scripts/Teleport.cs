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
            // �i�ھڻݭn�]�m��L�b�V
            // newPosition.y = -newPosition.y;
             newPosition.z = -newPosition.z;

            Quaternion newRotation = objTransform.rotation;
            newRotation.y = -newRotation.y;
            // �i�ھڻݭn�]�m��L�b�V
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
