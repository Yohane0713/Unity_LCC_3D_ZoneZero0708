using System.Collections;
using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 狀態：敵人攻擊
    /// </summary>
    public class StateEnemyAttack : State
    {
        [SerializeField, Header("攻擊時間"), Range(0, 10)]
        private float attackTime = 3;
        [SerializeField, Header("追蹤狀態")]
        private StateEnemyTrack stateEnemyTrack;

        private string parAttack = "觸發攻擊_斧頭";
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
            print("<color=#f44>進入攻擊狀態</color>");
        }

        public override void StateExit()
        {
            base.StateExit();
            print("<color=#f11>離開攻擊狀態</color>");
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            if (isAttacking) yield break;
            // 攻擊時面向玩家處理，先獲得玩家座標
            Vector3 target = playerPoint.position;
            // 避免高度不同導致旋轉，將目標座標的高度設定為此物件的高度
            target.y = transform.position.y;
            // 面向座標目標
            transform.LookAt(target);
            isAttacking = true;
            ani.SetFloat(parMove, 0);
            ani.SetTrigger(parAttack);
            yield return waitAttackTime;
            stateMachine.ChangeState(stateEnemyTrack);
            isAttacking = false;
        }
    }
}