using UnityEngine;
using UnityEngine.AI;

namespace Mtaka
{
    /// <summary>
    /// ���A�G���ʩM�ݾ�
    /// </summary>
    public class StateEnemyIdle : State
    {
        public Transform target;

        [SerializeField, Header("���ʳt��"), Range(0, 10)]
        private float moveSpeed = 2;
        [SerializeField, Header("����Z��"), Range(0, 10)]
        private float stopDistance = 1.5f;
        [SerializeField, Header("�H���C���d��"), Range(0, 10)]
        private float wanderRange = 2;
        [SerializeField, Header("�H���C���C��")]
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
            // print("<color=#3f3>����ĤH���A�ݾ���</color>");
            // �]�w�N�z�����ت��a(�ؼЪ��󪺮y��)
            agent.SetDestination(target.position);
            print($"<color=#923>�P�ؼЪ��Ѿl�Z���G{agent.remainingDistance}</color>");
        }

        /// <summary>
        /// ��o�d�򤺪��H���y��
        /// </summary>
        private Vector3 RandomPointInRange()
        {
            // ��o ���⬰�����I �C���d�򤺪� �H���y��
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