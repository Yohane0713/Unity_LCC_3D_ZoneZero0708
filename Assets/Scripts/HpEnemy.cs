using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ��q�t�ΡG�ĤH
    /// </summary>
    public class HpEnemy : HpSystem
    {
        protected override void Awake()
        {
            base.Awake();
            parDamages = new string[] { "Ĳ�o����1", "Ĳ�o����2" };
        }
    }
}