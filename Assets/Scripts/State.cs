using UnityEngine;
using UnityEngine.AI;

namespace Mtaka
{
    
    public class State : MonoBehaviour, IState
    {
        protected StateMachine stateMachine;
        protected Animator ani;
        protected string parMove = "�B�I�Ʋ���";
        protected string playerName = "���k";
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