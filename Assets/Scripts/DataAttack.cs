using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ������ơG���a
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Attack", order = 0)]
    public class DataAttack : ScriptableObject
    {
        [Header("�����O"), Range(0, 1000)]
        public float attack;
        [Header("�����O�B��"), Range(0, 1)]
        public float attackFloat;
        [Header("��������"), Range(0, 100)]
        public float imbalance;
        [Header("�������ůB��"), Range(0, 1)]
        public float imbalanceFloat;
    }
}