using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Mtaka
{
    /// <summary>
    /// ���A�G���ʩM�ݾ�
    /// </summary>
    public class StateEnemyIdle : State
    {
        [SerializeField, Header("���ʳt��"), Range(0, 10)]
        private float moveSpeed = 2;
        [SerializeField, Header("����Z��"), Range(0, 10)]
        private float stopDistance = 1.5f;
        [SerializeField, Header("�H���C���d��"), Range(0, 10)]
        private float wanderRange = 2;
        [SerializeField, Header("�H���C���C��")]
        private Color wanderColor;
        [Header("�H�����ݽd��")]
        [SerializeField, Range(0, 10)]
        private float idleMin, idleMax;
        [SerializeField, Header("�l�ܪ��a�d��"), Range(0, 50)]
        private float trackRange = 2;
        [SerializeField, Header("�l�ܽd��")]
        private Color trackColor;
        [SerializeField, Header("�l�ܹϼh")]
        private LayerMask trackLayer = 1 << 3;
        [SerializeField, Header("�l�ܪ��A")]
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
            print("<color=#6f6>�i�J�ݾ����A</color>");
        }

        public override void StateExit()
        {
            base.StateExit();
            print("<color=#6f6>���}�ݾ����A</color>");
            StopAllCoroutines();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            
            if (CheckPlayer()) return;
            // print("<color=#3f3>����ĤH���A�ݾ���</color>");
            // �p�G����N���X
            if (agent.isStopped) return;
            // �]�w�N�z�����ت��a(�ؼЪ��󪺮y��)
            agent.SetDestination(target);
            // ��s���ʰʵe
            ani.SetFloat(parMove, agent.velocity.magnitude / moveSpeed);
            // print($"<color=#923>�P�ؼЪ��Ѿl�Z���G{agent.remainingDistance}</color>");
            // �p�G�Ѿl�Z���p�󰱤�Z���N���s���H���y��
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

        private bool CheckPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, trackRange, trackLayer);
            if (hits.Length == 0) return false;
            print("<color=#f33>�����쪱�a</color>");
            // �ഫ��l�ܪ��A
            stateMachine.ChangeState(stateTrack);

            return hits.Length > 0;
        }
    }
}