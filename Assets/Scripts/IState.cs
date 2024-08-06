using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ���A����
    /// </summary>
    public interface IState
    {
        public void StateEnter();
        public void StateExit();
        public void StateUpdate();
    }
}
