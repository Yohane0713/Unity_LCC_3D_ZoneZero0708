using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ��q���
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Hp", order = 2)]
    public class DataHp : ScriptableObject
    {
        [Header("��q"), Range(0, 5000)]
        public float hp;
    }
}