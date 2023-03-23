using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class SnakeHead : MonoBehaviour
    {
        // ���} �D������(���O���) ����;
        public SnakeMovement movement;

        // ���} �ͦ�����(���O���) �ͦ�;
        public SpawnObject SpaO;

        // �p�H �i�J�I���ƥ�(�I�����O col)
        void OnCollisionEnter(Collision col)
        {
            // �p�G(col.�C������.���� �O "����")
            if(col.gameObject.tag == "Food")
            {
                // ����.�W�[���鳡���k�I�s();
                movement.AddBodyPart();

                // �R��(col.�C������)
                Destroy(col.gameObject);

                // �ͦ�.�ͦ�������k();
                SpaO.SpawnFood();
            }
            // �p�G���O
            else
            {
                // �p�G(col.�ഫ ������ ����.���鳡��(�Ƽ�)[��2��] �åB ����.����)
                if(col.transform != movement.BodyParts[1]&& movement.IsAlive)
                {
                    // �p�G(�ɶ����O.�ɶ� - ����.�W�����ծɶ� > 100) ����.�I�s���`��k();
                    if (Time.time - movement.TimeFromLastRetry > 100)
                        movement.DIE();
                }
            }
        }
    }
}
