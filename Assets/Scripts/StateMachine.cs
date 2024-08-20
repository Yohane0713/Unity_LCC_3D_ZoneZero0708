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
        private bool isImbalancing;

        private void Awake()
        {
            currentState = stateDefault;
            hpEnemy = GetComponent<HpEnemy>();
            hpEnemy.onImbalance += Imbalancing;
        }

        /// <summary>
        /// ���Ť�
        /// </summary>
        private void Imbalancing(object sender, System.EventArgs e)
        {
            isImbalancing = true;
            currentState = stateDefault;
        }

        // ����Q���� (enabled = false) �ɷ|����@��
        private void OnDisable()
        {
            currentState.StateExit();
            print("<color=#f36>���A������</color>");
        }

        private void Update()
        {
            if (isImbalancing) return;
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