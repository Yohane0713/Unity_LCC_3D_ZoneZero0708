using UnityEngine;

namespace Mtaka
{
    
    public class State : MonoBehaviour, IState
    {
        private StateMachine stateMachine;
        private Animator ani;

        protected virtual void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            ani = GetComponent<Animator>();
        }

        public virtual void StateEnter()
        {
            
        }

        public virtual void StateExit()
        {
           
        }

        public virtual void StateUpdate()
        {
            
        }
    }
}