using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ¤¶­±¡G§ðÀ»
    /// </summary>
    public interface IAttack
    {
        /// <summary>
        /// §ðÀ»¤O
        /// </summary>
        public float attack { get; }
        /// <summary>
        /// ¥¢¿Å
        /// </summary>
        public float imbalance { get; }
    }
}