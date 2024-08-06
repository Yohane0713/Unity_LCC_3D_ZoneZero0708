using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ª¬ºA¤¶­±
    /// </summary>
    public interface IState
    {
        public void StateEnter();
        public void StateExit();
        public void StateUpdate();
    }
}
