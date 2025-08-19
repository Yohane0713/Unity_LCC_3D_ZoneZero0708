using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 狀態：敵人追蹤
    /// </summary>
    public class StateEnemyTrack : State
    {
        public bool isImbalance;

        [SerializeField, Header("追蹤速度"), Range(0, 10)]
        private float trackSpeed = 5;
        [SerializeField, Header("攻擊狀態")]
        private StateEnemyAttack stateEnemyAttack;

        private bool canStartAttack => agent.remainingDistance < agent.stoppingDistance;

        public override void StateEnter()
        {
            if (isImbalance) return;

            base.StateEnter();
            print("<color=#36f>進入追蹤狀態</color>");
            agent.speed = trackSpeed;
            agent.isStopped = false;
            agent.SetDestination(playerPoint.position);
        }

        public override void StateExit()
        {
            base.StateExit();
            // print("<color=#36f>離開追蹤狀態</color>");
            agent.speed = 0;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            if (isImbalance) return;

            // 如果 可以攻擊 就切換到攻擊狀態 並 跳出
            if (canStartAttack)
            {
                stateMachine.ChangeState(stateEnemyAttack);
                return;
            }

            TrackPlayer();
        }

        private void TrackPlayer()
        {
            // 追蹤玩家
            agent.SetDestination(playerPoint.position);
            ani.SetFloat(parMove, agent.velocity.magnitude / trackSpeed);
        }


    }
}