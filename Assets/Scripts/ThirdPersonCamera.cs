using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Header("�t��")]
        public float speed = 1;
        [Header("�ؼ�")]
        public Transform Target;
        [Header("�۾����Y")]
        public Camera cam;

        void LateUpdate()
        {
            // �I�s���ʤ�k
            Move();
        }

        // ���} ���ʤ�k()
        public void Move()
        {
            // 3���V�q ���� = (�ؼ�.��m - �۾����Y.�ഫ.��m).�k�@��;
            Vector3 direction = (Target.position - cam.transform.position).normalized;

            // �|���� �ݦV���� = �|����.�ݦV����(����);
            Quaternion lookrotation = Quaternion.LookRotation(direction);

            // �ݦV����.x = �ഫ.����.x;
            lookrotation.x = transform.rotation.x;
            // �ݦV����.z = �ഫ.����.z;
            lookrotation.z = transform.rotation.z;

            // �ഫ.���� = �|����..�y�δ���(�ഫ.����, �ݦV����, �ɶ����O.�ɶ����j * 100)
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 100);

            // �ഫ.��m = 3���V�q.�y�δ���(�ഫ.��m, �ؼ�.��m, �ɶ����O.�ɶ����j * �t��)
            transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime * speed);

        }
    }
}
