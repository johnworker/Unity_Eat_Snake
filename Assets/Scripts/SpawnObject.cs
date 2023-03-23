using UnityEngine;
using System.Collections;

namespace Lancelot
{
    public class SpawnObject : MonoBehaviour
    {
        [Header("食物預置物")]
        public GameObject Foodprefab;

        [Header("中心")]
        public Vector3 center;
        [Header("大小")]
        public Vector3 size;

        void Start()
        {
            // 呼叫生成食物方法
            SpawnFood();
        }

        private void Update()
        {
            // 如果 (按鍵盤Z)呼叫生成食物
            if (Input.GetKey(KeyCode.Z))
                SpawnFood();
        }

        // 生成食物方法
        public void SpawnFood()
        {
            // 3維向量 位置(變數) = 中心 + 新 3維向量(隨機範圍(負大小.x/2 , 大小.x/2), 隨機範圍(負大小.y/2 , 大小.y/2), 隨機範圍(負大小.z/2 , 大小.z/2));
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2),Random.Range(-size.z / 2, size.z / 2));

            Instantiate(Foodprefab, pos, Quaternion.identity);
        }

        // 畫選擇區域方法
        void OnDrawGizmosSelected()
        {
            // 區域.顏色 = 新 色彩(1, 0, 0, 0.5f);
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            // 區域.畫方形(中心, 大小);
            Gizmos.DrawCube(center, size);
        }

    }
}