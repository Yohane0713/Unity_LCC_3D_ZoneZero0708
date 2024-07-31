using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 血量資料
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Hp")]
    public class DataHp : ScriptableObject
    {
        [Header("血量"), Range(0, 5000)]
        public float hp;
    }
}