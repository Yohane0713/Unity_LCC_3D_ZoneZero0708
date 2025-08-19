using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 介面：攻擊
    /// </summary>
    public interface IAttack
    {
        /// <summary>
        /// 攻擊力
        /// </summary>
        public float attack { get; }
        /// <summary>
        /// 失衡
        /// </summary>
        public float imbalance { get; }
    }
}