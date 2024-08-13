using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ��q�t�ΡG�ĤH
    /// </summary>
    public class HpEnemy : HpSystem
    {
        private StateMachine stateMachine;
        private CapsuleCollider capsuleCollider;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = GetComponent<StateMachine>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            parDamages = new string[] { "Ĳ�o����1", "Ĳ�o����2" };
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
    }
}