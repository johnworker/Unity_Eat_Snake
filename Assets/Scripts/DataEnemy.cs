using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lancelot
{
    [CreateAssetMenu(menuName = "Lancelot/Data Enemy", fileName = "Data Enemy")]
    public class DataEnemy : ScriptableObject
    {
        [Header("¦å¶q"), Range(0, 10000)]
        public float hp;
        [Header("¶Ë®`"), Range(0, 10000)]
        public float damage;
    }
}
