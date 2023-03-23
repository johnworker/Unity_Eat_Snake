using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class SnakeHead : MonoBehaviour
    {
        // 公開 蛇的移動(類別資料) 移動;
        public SnakeMovement movement;

        // 公開 生成物件(類別資料) 生成;
        public SpawnObject SpaO;

        // 私人 進入碰撞事件(碰撞類別 col)
        void OnCollisionEnter(Collision col)
        {
            // 如果(col.遊戲物件.標籤 是 "食物")
            if(col.gameObject.tag == "Food")
            {
                // 移動.增加身體部位方法呼叫();
                movement.AddBodyPart();

                // 摧毀(col.遊戲物件)
                Destroy(col.gameObject);

                // 生成.生成食物方法();
                SpaO.SpawnFood();
            }
            // 如果不是
            else
            {
                // 如果(col.轉換 不等於 移動.身體部分(複數)[第2位] 並且 移動.活著)
                if(col.transform != movement.BodyParts[1]&& movement.IsAlive)
                {
                    // 如果(時間類別.時間 - 移動.上次重試時間 > 100) 移動.呼叫死亡方法();
                    if (Time.time - movement.TimeFromLastRetry > 100)
                        movement.DIE();
                }
            }
        }
    }
}
