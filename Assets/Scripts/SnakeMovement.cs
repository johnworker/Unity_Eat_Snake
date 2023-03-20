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
        [Header("���餧�����Z��")]
        public float mindistance = 0.25f;
        [Header("���F�Y���~���_�l����")]
        public int beginsize;

        [Header("���ʳt��")]
        public float speed = 1;
        [Header("����t��")]
        public float rotationspeed = 50;

        [Header ("�W�����ծɶ�")]
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
            // �w�q ��e�t�� = �t��
            float curspeed = speed;

            // �P�_(�����) ��e�t����2��
            if (Input.GetKey(KeyCode.UpArrow))
                curspeed *= 2;

            // �Ĥ@�Ө��鳡��.�첾(�Ĥ@�Ө��鳡��.�e�� * ��e�t�� * �ɶ�.���ƼW�q�ɶ�, �Ŷ�.�@��);
            BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

            // �P�_ ����.������жb("�����y��" != 0)
            //  �Ĥ@�Ө��鳡��.����(3���V�q.�W�� * ����t�� * �ɶ�.���j�ɶ� * ����.������жb("�����y��"));
            if (Input.GetAxis("Horizontal") != 0)
                BodyParts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));

            // �j��(��� �ܼ�=1; �ܼ�<���鳡��(��).�ƶq; �ܼƼW�[) 
            for (int i = 1; i < BodyParts.Count; i++)
            {
                // ��e���鳡�� = ���鳡��(��)[�ܼ�];
                curBodyPart = BodyParts[i];
                // �e�@�Ө��鳡�� = ���鳡��(��)[�ܼ� - 1];
                PrevBodyPart = BodyParts[i - 1];
                // �w�q�Z�� = 3���V�q.�Z��(�e�q���鳡��.��m, ��e���鳡��.��m)
                dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);

                // 3���V�q �s��m = ����e�@�ӳ���.��m
                Vector3 newpos = PrevBodyPart.position;

                // �s��m.y = ����Ĥ@�ӳ���.��m.y�b
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