using UnityEngine;
using UnityEngine.AI;

namespace Mtaka
{
    
    public class State : MonoBehaviour, IState
    {
        protected StateMachine stateMachine;
        protected Animator ani;
        protected string parMove = "浮點數移動";
        protected string playerName = "雪女";
        protected Transform playerPoint;
        protected NavMeshAgent agent;

        protected virtual void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            ani = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            playerPoint = GameObject.Find(playerName).transform;
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