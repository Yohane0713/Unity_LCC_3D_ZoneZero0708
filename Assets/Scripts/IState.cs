using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 狀態介面
    /// </summary>
    public interface IState
    {
        public void StateEnter();
        public void StateExit();
        public void StateUpdate();
    }
}
