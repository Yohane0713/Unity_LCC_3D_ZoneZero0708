using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 攻擊資料：玩家
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Attack", order = 0)]
    public class DataAttack : ScriptableObject
    {
        [Header("攻擊力"), Range(0, 1000)]
        public float attack;
        [Header("攻擊力浮動"), Range(0, 1)]
        public float attackFloat;
        [Header("攻擊失衡"), Range(0, 100)]
        public float imbalance;
        [Header("攻擊失衡浮動"), Range(0, 1)]
        public float imbalanceFloat;
    }
}