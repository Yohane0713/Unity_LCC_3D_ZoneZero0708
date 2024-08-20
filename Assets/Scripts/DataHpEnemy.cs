using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 血量資料：敵人
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Hp Enemy", order = 2)]
    public class DataHpEnemy : DataHp
    {
        [Header("失衡時間"), Range(0, 1000)]
        public float imbalanceTime;
    }
}