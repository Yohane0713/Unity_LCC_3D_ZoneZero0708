using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 血量系統：敵人
    /// </summary>
    public class HpEnemy : HpSystem
    {
        [SerializeField, Header("群組_敵人血量介面")]
        private GameObject prefabHpUI;

        private StateMachine stateMachine;
        private CapsuleCollider capsuleCollider;
        /// <summary>
        /// 畫布主要介面
        /// </summary>
        private Transform canvas;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = GetComponent<StateMachine>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            canvas = GameObject.Find("畫布主要介面").transform;
            parDamages = new string[] { "觸發受傷1", "觸發受傷2" };
            SpawnUI();
        }

        protected override void Damage(float damage)
        {
            base.Damage(damage);
            string parDamage = parDamages[Random.Range(0, parDamages.Length)];
            ani.SetTrigger(parDamage);
        }

        protected override void Dead()
        {
            base.Dead();
            stateMachine.enabled = false;
            capsuleCollider.enabled = false;
        }

        /// <summary>
        /// 生成介面
        /// </summary>
        private void SpawnUI()
        {
            GameObject tempHpUI = Instantiate(prefabHpUI, canvas);
            tempHpUI.GetComponent<WorldToUI>().targetPoint = transform;
        }
    }
}