using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ���A�G�ĤH�l��
    /// </summary>
    public class StateEnemyTrack : State
    {
        [SerializeField, Header("�l�ܳt��"), Range(0, 10)]
        private float trackSpeed = 5;
        [SerializeField, Header("�������A")]
        private StateEnemyAttack stateEnemyAttack;

        private bool canStartAttack => agent.remainingDistance < agent.stoppingDistance;

        public override void StateEnter()
        {
            base.StateEnter();
            print("<color=#36f>�i�J�l�ܪ��A</color>");
            agent.speed = trackSpeed;
            agent.isStopped = false;
            agent.SetDestination(playerPoint.position);
        }

        public override void StateExit()
        {
            base.StateExit();
            print("<color=#36f>���}�l�ܪ��A</color>");
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            // �p�G �i�H���� �N������������A �� ���X
            if (canStartAttack)
            {
                stateMachine.ChangeState(stateEnemyAttack);
                return;
            }

            TrackPlayer();
        }

        private void TrackPlayer()
        {
            // �l�ܪ��a
            agent.SetDestination(playerPoint.position);
            ani.SetFloat(parMove, agent.velocity.magnitude / trackSpeed);
        }


    }
}