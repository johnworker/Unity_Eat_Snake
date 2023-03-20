using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Lancelot
{
    public class SnakeMovement : MonoBehaviour
    {

        public List<Transform> BodyParts = new List<Transform>();
        [Header("身體之間的距離")]
        public float mindistance = 0.25f;
        [Header("除了頭之外的起始長度")]
        public int beginsize;

        [Header("移動速度")]
        public float speed = 1;
        [Header("旋轉速度")]
        public float rotationspeed = 50;

        [Header ("上次重試時間")]
        public float TimeFromLastRetry;

        public GameObject bodyprefab;

        public TextMeshProUGUI CurrentScore;
        public TextMeshProUGUI ScoreText;

        public GameObject DeadScreen;

        private float dis;
        private Transform curBodyPart;
        private Transform PrevBodyPart;

        public bool IsAlive;


        void Start()
        {
            StartLevel();
        }

        void Update()
        {
            if (IsAlive)
                Move();

            if (Input.GetKey(KeyCode.Q))
                AddBodyPart();
        }

        public void StartLevel()
        {
            TimeFromLastRetry = Time.time;

            DeadScreen.SetActive(false);

            for (int i = BodyParts.Count - 1; i > 1; i--)
            {
                Destroy(BodyParts[i].gameObject);

                BodyParts.Remove(BodyParts[i]);
            }

            BodyParts[0].position = new Vector3(0, 0.5f, 0);

            BodyParts[0].rotation = Quaternion.identity;

            CurrentScore.gameObject.SetActive(true);

            CurrentScore.text = "Score: 0";

            IsAlive = true;

            for (int i = 0; i < beginsize; i++)
            {
                AddBodyPart();
            }

            BodyParts[0].position = new Vector3(2, 0.5f, 0);
        }

        public void Move()
        {
            // 定義 當前速度 = 速度
            float curspeed = speed;

            // 判斷(按鍵↑) 當前速度變2倍
            if (Input.GetKey(KeyCode.UpArrow))
                curspeed *= 2;

            // 第一個身體部位.位移(第一個身體部位.前方 * 當前速度 * 時間.平滑增量時間, 空間.世界);
            BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

            // 判斷 按鍵.獲取坐標軸("水平座標" != 0)
            //  第一個身體部位.旋轉(3維向量.上方 * 旋轉速度 * 時間.間隔時間 * 按鍵.獲取坐標軸("水平座標"));
            if (Input.GetAxis("Horizontal") != 0)
                BodyParts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));

            // 迴圈(整數 變數=1; 變數<身體部位(複).數量; 變數增加) 
            for (int i = 1; i < BodyParts.Count; i++)
            {
                // 當前身體部位 = 身體部位(複)[變數];
                curBodyPart = BodyParts[i];
                // 前一個身體部位 = 身體部位(複)[變數 - 1];
                PrevBodyPart = BodyParts[i - 1];
                // 定義距離 = 3維向量.距離(前段身體部位.位置, 當前身體部位.位置)
                dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);

                // 3維向量 新位置 = 身體前一個部分.位置
                Vector3 newpos = PrevBodyPart.position;

                // 新位置.y = 身體第一個部位.位置.y軸
                newpos.y = BodyParts[0].position.y;

                float T = Time.deltaTime * dis / mindistance * curspeed;

                if (T > 0.5f)
                    T = 0.5f;
                curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
                curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
            }
        }

        public void AddBodyPart()
        {
            Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

            newpart.SetParent(transform);

            BodyParts.Add(newpart);

            CurrentScore.text = "Score: " + (BodyParts.Count - beginsize).ToString();
        }

        public void DIE()
        {
            IsAlive = false;

            ScoreText.text = "Your score was" + (BodyParts.Count - beginsize).ToString();

            CurrentScore.gameObject.SetActive(false);

            DeadScreen.SetActive(true);
        }
    }
}