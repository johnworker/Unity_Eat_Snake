using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class SnakeHead : MonoBehaviour
    {

        public SnakeMovement movement;

        void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.tag == "Food")
            {
                movement.AddBodyPart();

                Destroy(col.gameObject);

                // spawnfood
            }
        }
    }
}
