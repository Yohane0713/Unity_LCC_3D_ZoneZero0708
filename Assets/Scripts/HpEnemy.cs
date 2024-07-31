using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 血量系統：敵人
    /// </summary>
    public class HpEnemy : HpSystem
    {
        protected override void Awake()
        {
            base.Awake();
            parDamages = new string[] { "觸發受傷1", "觸發受傷2" };
        }
    }
}