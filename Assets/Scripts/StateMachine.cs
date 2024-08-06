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

        private void Awake()
        {
            currentState = stateDefault;
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
        private void ChangeState(IState nextState)
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