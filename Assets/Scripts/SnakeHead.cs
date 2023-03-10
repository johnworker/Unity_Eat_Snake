using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class SnakeHead : MonoBehaviour
    {

        public SnakeMovement movement;

        public SpawnObject SpaO;

        void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.tag == "Food")
            {
                movement.AddBodyPart();

                Destroy(col.gameObject);

                // spawnfood
                SpaO.SpawnFood();
            }
            else
            {
                if(col.transform != movement.BodyParts[1]&& movement.IsAlive)
                {
                    if (Time.time - movement.TimeFromLastRetry > 100)
                        movement.DIE();
                }
            }
        }
    }
}
