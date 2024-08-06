using UnityEngine;
using UnityEngine.AI;

namespace Mtaka
{
    /// <summary>
    /// 狀態：移動和待機
    /// </summary>
    public class StateEnemyIdle : State
    {
        public Transform target;

        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float moveSpeed = 2;
        [SerializeField, Header("停止距離"), Range(0, 10)]
        private float stopDistance = 1.5f;
        [SerializeField, Header("隨機遊走範圍"), Range(0, 10)]
        private float wanderRange = 2;
        [SerializeField, Header("隨機遊走顏色")]
        private Color wanderColor;

        private NavMeshAgent agent;

        private void OnDrawGizmos()
        {
            Gizmos.color = wanderColor;
            Gizmos.DrawSphere(transform.position, wanderRange);
        }

        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            agent.stoppingDistance = stopDistance;

            target.position = RandomPointInRange();
        }

        public override void StateEnter()
        {
            base.StateEnter();
        }

        public override void StateExit()
        {
            base.StateExit();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            // print("<color=#3f3>執行敵人狀態待機中</color>");
            // 設定代理器的目的地(目標物件的座標)
            agent.SetDestination(target.position);
            print($"<color=#923>與目標的剩餘距離：{agent.remainingDistance}</color>");
        }

        /// <summary>
        /// 獲得範圍內的隨機座標
        /// </summary>
        private Vector3 RandomPointInRange()
        {
            // 獲得 角色為中心點 遊走範圍內的 隨機座標
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRange;

            NavMeshHit hit;

            while(!NavMesh.SamplePosition(randomPoint, out hit, wanderRange, NavMesh.AllAreas))
            {
                randomPoint = transform.position + Random.insideUnitSphere * wanderRange;
            }

            randomPoint = hit.position;

            return randomPoint;
        }
    }
}