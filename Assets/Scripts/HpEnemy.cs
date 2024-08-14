using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ��q�t�ΡG�ĤH
    /// </summary>
    public class HpEnemy : HpSystem
    {
        [SerializeField, Header("�s��_�ĤH��q����")]
        private GameObject prefabHpUI;

        private StateMachine stateMachine;
        private CapsuleCollider capsuleCollider;
        /// <summary>
        /// �e���D�n����
        /// </summary>
        private Transform canvas;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = GetComponent<StateMachine>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            canvas = GameObject.Find("�e���D�n����").transform;
            parDamages = new string[] { "Ĳ�o����1", "Ĳ�o����2" };
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
        /// �ͦ�����
        /// </summary>
        private void SpawnUI()
        {
            GameObject tempHpUI = Instantiate(prefabHpUI, canvas);
            tempHpUI.GetComponent<WorldToUI>().targetPoint = transform;
        }
    }
}