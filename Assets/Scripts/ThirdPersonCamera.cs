using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Header("速度")]
        public float speed = 1;
        [Header("目標")]
        public Transform Target;
        [Header("相機鏡頭")]
        public Camera cam;

        void LateUpdate()
        {
            // 呼叫移動方法
            Move();
        }

        // 公開 移動方法()
        public void Move()
        {
            // 3維向量 直接 = (目標.位置 - 相機鏡頭.轉換.位置).歸一化;
            Vector3 direction = (Target.position - cam.transform.position).normalized;

            // 四元數 看向旋轉 = 四元數.看向旋轉(直接);
            Quaternion lookrotation = Quaternion.LookRotation(direction);

            // 看向旋轉.x = 轉換.旋轉.x;
            lookrotation.x = transform.rotation.x;
            // 看向旋轉.z = 轉換.旋轉.z;
            lookrotation.z = transform.rotation.z;

            // 轉換.旋轉 = 四元數..球形插值(轉換.旋轉, 看向旋轉, 時間類別.時間間隔 * 100)
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 100);

            // 轉換.位置 = 3維向量.球形插值(轉換.位置, 目標.位置, 時間類別.時間間隔 * 速度)
            transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime * speed);

        }
    }
}
