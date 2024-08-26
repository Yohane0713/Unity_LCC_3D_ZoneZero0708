using System.Collections;
using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ���A���G�޲z���A��
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        [SerializeField, Header("�w�]���A")]
        private State stateDefault;

        private IState currentState;
        private HpEnemy hpEnemy;
        private StateEnemyImbalance stateEnemyImbalance;
        private StateEnemyTrack stateEnemyTrack;
        
        private void Awake()
        {
            currentState = stateDefault;
            hpEnemy = GetComponent<HpEnemy>();
            stateEnemyImbalance = GetComponent<StateEnemyImbalance>();
            stateEnemyTrack = GetComponent<StateEnemyTrack>();
            hpEnemy.onImbalance += Imbalancing;
        }

        /// <summary>
        /// ���Ť�
        /// </summary>
        private void Imbalancing(object sender, float imbalanceTime)
        {
            // ���Ū��A�� ���Ůɶ� = �ƥ󱵦��쪺���Ůɶ�
            stateEnemyImbalance.imbalanceTime = imbalanceTime;
            stateEnemyTrack.isImbalance = true;
            // �ܧ󬰥��Ū��A
            ChangeState(stateEnemyImbalance);
        }

        // ����Q���� (enabled = false) �ɷ|����@��
        private void OnDisable()
        {
            currentState.StateExit();
            print("<color=#f36>���A������</color>");
        }

        private void Update()
        {
            UpdateState();
        }

        /// <summary>
        /// ��s���A��
        /// </summary>
        private void UpdateState()
        {
            currentState.StateUpdate();
        }

        /// <summary>
        /// ���ܪ��A��
        /// </summary>
        /// <param name="nextState">�U�@�Ӫ��A</param>
        public void ChangeState(IState nextState)
        {
            // �쥻�����A ���}
            currentState.StateExit();
            // �U�@�Ӫ��A �i�J
            nextState.StateEnter();
            // ��s�ثe���A���U�@�Ӫ��A
            currentState = nextState;
        }
    }
}