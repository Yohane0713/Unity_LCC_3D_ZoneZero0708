using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ��q��ơG�ĤH
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Hp Enemy", order = 3)]
    public class DataHpEnemy : DataHp
    {
        [Header("���Ůɶ�"), Range(0, 1000)]
        public float imbalanceTime;
    }
}