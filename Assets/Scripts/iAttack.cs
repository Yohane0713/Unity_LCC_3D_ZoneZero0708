using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// �����G����
    /// </summary>
    public interface IAttack
    {
        /// <summary>
        /// �����O
        /// </summary>
        public float attack { get; }
        /// <summary>
        /// ����
        /// </summary>
        public float imbalance { get; }
    }
}