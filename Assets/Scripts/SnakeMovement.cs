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
        public float speed = 3;
        [Header("����t��")]
        public float rotationspeed = 50;

        [Header ("�W�����ծɶ�")]
        public float TimeFromLastRetry;

        [Header("���骫����m��")]
        public GameObject bodyprefab;

        [Header("��e����")]
        public TextMeshProUGUI CurrentScore;
        [Header("���Ƥ�r")]
        public TextMeshProUGUI ScoreText;

        [Header("���`�e��")]
        public GameObject DeadScreen;

        [Header("���I�� �Z��")]
        private float dis;
        [Header("��e���鳡��")]
        private Transform curBodyPart;
        [Header("�e�@�Ө��鳡��")]
        private Transform PrevBodyPart;

        [Header("�O�_�s��")]
        public bool IsAlive;


        void Start()
        {
            StartLevel();
        }

        void Update()
        {
            // �p�G(����)�I�s���ʤ�k
            if (IsAlive)
                Move();

            // �p�G(����LQ)�W�[�����k
            if (Input.GetKey(KeyCode.Q))
                AddBodyPart();
        }

        // �}�l���Ť�k
        public void StartLevel()
        {
            // �W�����ծɶ� = �ɶ����O.�ɶ�
            TimeFromLastRetry = Time.time;

            // ���`�e��.�]�m(����);
            DeadScreen.SetActive(false);

            // �j��(��� i(�ܼ�) = ���鳡��(�Ƽ�).�p�� - 1; i(�ܼ�) > 1; i(�ܼ�)--)
            for (int i = BodyParts.Count - 1; i > 1; i--)
            {
                // �R��(���鳡��(�Ƽ�)[i(�ܼ�)].�C������);
                Destroy(BodyParts[i].gameObject);

                // ���鳡��(�Ƽ�).����(���鳡��(�Ƽ�)[i(�ܼ�)]);
                BodyParts.Remove(BodyParts[i]);
            }

            // ���鳡��(�Ƽ�)[�Ĥ@��].��m = �s 3���V�q(�y��);
            BodyParts[0].position = new Vector3(0, 0.5f, 0);

            // ���鳡��(�Ƽ�)[�Ĥ@��].���� = �|����.�s�ר�;
            BodyParts[0].rotation = Quaternion.identity;

            // ��e����.�C������.�]�m(�}��);
            CurrentScore.gameObject.SetActive(true);

            // ��e����.��r = "����: 0";
            CurrentScore.text = "Score: 0";

            // �s�� = �O;
            IsAlive = true;

            // �j��(��� i(�ܼ�) = 0; i(�ܼ�) < �_�l����j�p; i(�ܼ�)++)
            for (int i = 0; i < beginsize; i++)
            {
                // �I�s�W�[���鳡����k();
                AddBodyPart();
            }

            // ���鳡��(�Ƽ�)[�Ĥ@��].��m = �s 3���V�q(�y��);
            BodyParts[0].position = new Vector3(2, 0.5f, 0);
        }

        // ���ʤ�k
        public void Move()
        {
            // �w�q ��e�t�� = �t��
            float curspeed = speed;

            // �P�_(�����) ��e�t����2��
            if (Input.GetKey(KeyCode.UpArrow))
                curspeed *= 2;

            // �Ĥ@�Ө��鳡��.�첾(�Ĥ@�Ө��鳡��.�e�� * ��e�t�� * �ɶ�.���ƼW�q�ɶ�, �Ŷ�.�@�� Space.World);
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

                // ���I�� T(�ܼ�) = �ɶ�(���O).�ɶ����j * �w�q�Z�� / ���餧�����Z�� * �w�q��e�t��
                float T = Time.deltaTime * dis / mindistance * curspeed;

                // �p�G (T(�ܼ�) > 0.5f)
                // T(�ܼ�) = 0.5f;
                if (T > 0.5f)
                    T = 0.5f;

                // ��e���鳡��.��m = 3���V�q.�y�δ���(��e���鳡��.��m, �s��m, T(�ܼ�))
                curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
                // ��e���鳡��.���� = �|����.�y�δ���(��e���鳡��.����, �e�@�Ө��鳡��.����, T(�ܼ�))
                curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);

                Physics.IgnoreLayerCollision(6, 6);
            }
        }

        // �W�[���鳡����k
        public void AddBodyPart()
        {
            // �ഫ �s����(�ܼ�) = (�ͦ� (������m��, ���鳡��(�Ƽ�)[���鳡��(�Ƽ�).�p�� - 1].��m,  ���鳡��(�Ƽ�)[���鳡��(�Ƽ�).�p�� - 1].����) �@�� �C������).�ഫ;
            Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
            // �s����(�ܼ�).�]�m������(�ഫ);
            newpart.SetParent(transform);

            // ���鳡��(�Ƽ�).�W�[(�s����(�ܼ�));
            BodyParts.Add(newpart);

            // ��e����.��r = "����:" + (���鳡��(�Ƽ�).�p�� - �_�l����j�p).�ഫ�r��();
            CurrentScore.text = "Score: " + (BodyParts.Count - beginsize).ToString();
        }

        // ���`��k
        public void DIE()
        {
            // �s�� = �_
            IsAlive = false;

            // ���Ƥ�r.��r = "�A�����ƬO" + (���鳡��(�Ƽ�).�p�� - �_�l����j�p).�ഫ�r��();
            ScoreText.text = "Your score was" + (BodyParts.Count - beginsize).ToString();

            // ��e����.�C������.�]�m(����);
            CurrentScore.gameObject.SetActive(false);

            // ���`�e��.�]�m(�}��);
            DeadScreen.SetActive(true);
        }
    }
}