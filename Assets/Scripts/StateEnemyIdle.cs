using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Mtaka
{
    /// <summary>
    /// 狀態：移動和待機
    /// </summary>
    public class StateEnemyIdle : State
    {
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float moveSpeed = 2;
        [SerializeField, Header("停止距離"), Range(0, 10)]
        private float stopDistance = 1.5f;
        [SerializeField, Header("隨機遊走範圍"), Range(0, 10)]
        private float wanderRange = 2;
        [SerializeField, Header("隨機遊走顏色")]
        private Color wanderColor;
        [Header("隨機等待範圍")]
        [SerializeField, Range(0, 10)]
        private float idleMin, idleMax;
        [SerializeField, Header("追蹤玩家範圍"), Range(0, 50)]
        private float trackRange = 2;
        [SerializeField, Header("追蹤範圍")]
        private Color trackColor;
        [SerializeField, Header("追蹤圖層")]
        private LayerMask trackLayer = 1 << 3;
        [SerializeField, Header("追蹤狀態")]
        private State stateTrack;

        private Vector3 target;

        private void OnDrawGizmos()
        {
            Gizmos.color = wanderColor;
            Gizmos.DrawSphere(transform.position, wanderRange);
            Gizmos.color = trackColor;
            Gizmos.DrawSphere(transform.position, trackRange);
        }

        protected override void Awake()
        {
            base.Awake();
            agent.speed = moveSpeed;
            agent.stoppingDistance = stopDistance;
            target = RandomPointInRange();
        }

        public override void StateEnter()
        {
            base.StateEnter();
            print("<color=#6f6>進入待機狀態</color>");
        }

        public override void StateExit()
        {
            base.StateExit();
            print("<color=#6f6>離開待機狀態</color>");
            StopAllCoroutines();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            
            if (CheckPlayer()) return;
            // print("<color=#3f3>執行敵人狀態待機中</color>");
            // 如果停止就跳出
            if (agent.isStopped) return;
            // 設定代理器的目的地(目標物件的座標)
            agent.SetDestination(target);
            // 更新移動動畫
            ani.SetFloat(parMove, agent.velocity.magnitude / moveSpeed);
            // print($"<color=#923>與目標的剩餘距離：{agent.remainingDistance}</color>");
            // 如果剩餘距離小於停止距離就找到新的隨機座標
            if (agent.remainingDistance < stopDistance)
            {
                StartCoroutine(StopWander());
            }
        }

        private IEnumerator StopWander()
        {
            agent.isStopped = true;
            ani.SetFloat(parMove, 0);
            float randomTime = Random.Range(idleMin, idleMax);
            yield return new WaitForSeconds(randomTime);
            agent.isStopped = false;
            target = RandomPointInRange();
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

        private bool CheckPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, trackRange, trackLayer);
            if (hits.Length == 0) return false;
            print("<color=#f33>偵測到玩家</color>");
            // 轉換到追蹤狀態
            stateMachine.ChangeState(stateTrack);

            return hits.Length > 0;
        }
    }
}