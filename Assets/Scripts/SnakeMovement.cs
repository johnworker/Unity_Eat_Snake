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
        public float speed = 3;
        [Header("旋轉速度")]
        public float rotationspeed = 50;

        [Header ("上次重試時間")]
        public float TimeFromLastRetry;

        [Header("身體物件欲置物")]
        public GameObject bodyprefab;

        [Header("當前分數")]
        public TextMeshProUGUI CurrentScore;
        [Header("分數文字")]
        public TextMeshProUGUI ScoreText;

        [Header("死亡畫面")]
        public GameObject DeadScreen;

        [Header("福點數 距離")]
        private float dis;
        [Header("當前身體部位")]
        private Transform curBodyPart;
        [Header("前一個身體部位")]
        private Transform PrevBodyPart;

        [Header("是否存活")]
        public bool IsAlive;


        void Start()
        {
            StartLevel();
        }

        void Update()
        {
            // 如果(活著)呼叫移動方法
            if (IsAlive)
                Move();

            // 如果(按鍵盤Q)增加身體方法
            if (Input.GetKey(KeyCode.Q))
                AddBodyPart();
        }

        // 開始等級方法
        public void StartLevel()
        {
            // 上次重試時間 = 時間類別.時間
            TimeFromLastRetry = Time.time;

            // 死亡畫面.設置(關閉);
            DeadScreen.SetActive(false);

            // 迴圈(整數 i(變數) = 身體部分(複數).計數 - 1; i(變數) > 1; i(變數)--)
            for (int i = BodyParts.Count - 1; i > 1; i--)
            {
                // 摧毀(身體部分(複數)[i(變數)].遊戲物件);
                Destroy(BodyParts[i].gameObject);

                // 身體部分(複數).移除(身體部分(複數)[i(變數)]);
                BodyParts.Remove(BodyParts[i]);
            }

            // 身體部分(複數)[第一位].位置 = 新 3維向量(座標);
            BodyParts[0].position = new Vector3(0, 0.5f, 0);

            // 身體部分(複數)[第一位].旋轉 = 四元數.零度角;
            BodyParts[0].rotation = Quaternion.identity;

            // 當前分數.遊戲物件.設置(開啟);
            CurrentScore.gameObject.SetActive(true);

            // 當前分數.文字 = "分數: 0";
            CurrentScore.text = "Score: 0";

            // 存活 = 是;
            IsAlive = true;

            // 迴圈(整數 i(變數) = 0; i(變數) < 起始身體大小; i(變數)++)
            for (int i = 0; i < beginsize; i++)
            {
                // 呼叫增加身體部分方法();
                AddBodyPart();
            }

            // 身體部分(複數)[第一位].位置 = 新 3維向量(座標);
            BodyParts[0].position = new Vector3(2, 0.5f, 0);
        }

        // 移動方法
        public void Move()
        {
            // 定義 當前速度 = 速度
            float curspeed = speed;

            // 判斷(按鍵↑) 當前速度變2倍
            if (Input.GetKey(KeyCode.UpArrow))
                curspeed *= 2;

            // 第一個身體部位.位移(第一個身體部位.前方 * 當前速度 * 時間.平滑增量時間, 空間.世界 Space.World);
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

                // 福點數 T(變數) = 時間(類別).時間間隔 * 定義距離 / 身體之間的距離 * 定義當前速度
                float T = Time.deltaTime * dis / mindistance * curspeed;

                // 如果 (T(變數) > 0.5f)
                // T(變數) = 0.5f;
                if (T > 0.5f)
                    T = 0.5f;

                // 當前身體部位.位置 = 3維向量.球形插值(當前身體部位.位置, 新位置, T(變數))
                curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
                // 當前身體部位.旋轉 = 四元數.球形插值(當前身體部位.旋轉, 前一個身體部位.旋轉, T(變數))
                curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);

                Physics.IgnoreLayerCollision(6, 6);
            }
        }

        // 增加身體部分方法
        public void AddBodyPart()
        {
            // 轉換 新部分(變數) = (生成 (身體欲置物, 身體部分(複數)[身體部分(複數).計數 - 1].位置,  身體部分(複數)[身體部分(複數).計數 - 1].旋轉) 作為 遊戲物件).轉換;
            Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
            // 新部分(變數).設置父物件(轉換);
            newpart.SetParent(transform);

            // 身體部分(複數).增加(新部分(變數));
            BodyParts.Add(newpart);

            // 當前分數.文字 = "分數:" + (身體部分(複數).計數 - 起始身體大小).轉換字串();
            CurrentScore.text = "Score: " + (BodyParts.Count - beginsize).ToString();
        }

        // 死亡方法
        public void DIE()
        {
            // 存活 = 否
            IsAlive = false;

            // 分數文字.文字 = "你的分數是" + (身體部分(複數).計數 - 起始身體大小).轉換字串();
            ScoreText.text = "Your score was" + (BodyParts.Count - beginsize).ToString();

            // 當前分數.遊戲物件.設置(關閉);
            CurrentScore.gameObject.SetActive(false);

            // 死亡畫面.設置(開啟);
            DeadScreen.SetActive(true);
        }
    }
}