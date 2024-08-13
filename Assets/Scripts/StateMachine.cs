using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 狀態機：管理狀態機
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        [SerializeField, Header("預設狀態")]
        private State stateDefault;

        private IState currentState;

        private void Awake()
        {
            currentState = stateDefault;
        }

        // 元件被關閉 (enabled = false) 時會執行一次
        private void OnDisable()
        {
            currentState.StateExit();
            print("<color=#f36>狀態機停止</color>");
        }

        private void Update()
        {
            UpdateState();
        }

        /// <summary>
        /// 更新狀態機
        /// </summary>
        private void UpdateState()
        {
            currentState.StateUpdate();
        }

        /// <summary>
        /// 更變狀態機
        /// </summary>
        /// <param name="nextState">下一個狀態</param>
        public void ChangeState(IState nextState)
        {
            // 原本的狀態 離開
            currentState.StateExit();
            // 下一個狀態 進入
            nextState.StateEnter();
            // 更新目前狀態為下一個狀態
            currentState = nextState;
        }
    }
}