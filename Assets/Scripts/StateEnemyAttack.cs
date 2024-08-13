using System.Collections;
using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ���A�G�ĤH����
    /// </summary>
    public class StateEnemyAttack : State
    {
        [SerializeField, Header("�����ɶ�"), Range(0, 10)]
        private float attackTime = 3;
        [SerializeField, Header("�l�ܪ��A")]
        private StateEnemyTrack stateEnemyTrack;

        private string parAttack = "Ĳ�o����_���Y";
        private bool isAttacking;
        private WaitForSeconds waitAttackTime;

        protected override void Awake()
        {
            base.Awake();
            waitAttackTime = new WaitForSeconds(attackTime);
        }

        public override void StateEnter()
        {
            base.StateEnter();
            print("<color=#f44>�i�J�������A</color>");
        }

        public override void StateExit()
        {
            base.StateExit();
            print("<color=#f11>���}�������A</color>");
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            if (isAttacking) yield break;
            isAttacking = true;
            ani.SetFloat(parMove, 0);
            ani.SetTrigger(parAttack);
            yield return waitAttackTime;
            stateMachine.ChangeState(stateEnemyTrack);
            isAttacking = false;
        }
    }
}