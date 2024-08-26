using System.Collections;
using UnityEngine;

namespace Mtaka
{
    public class StateEnemyImbalance : State
    {
        public float imbalanceTime;

        private WaitForSeconds waitImbalanceTime;

        protected override void Awake()
        {
            base.Awake();
            waitImbalanceTime = new WaitForSeconds(imbalanceTime);
        }

        public override void StateEnter()
        {
            base.StateEnter();
            waitImbalanceTime = new WaitForSeconds(imbalanceTime);
            StartCoroutine(ImbalanceFinish());
        }

        public override void StateExit()
        {
            base.StateExit();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            // �N�z�����t���k�s
            agent.speed = 0;
        }

        /// <summary>
        /// ���ŵ���
        /// </summary>
        private IEnumerator ImbalanceFinish()
        {
            yield return waitImbalanceTime;
            print("���ŵ���");
        }
    }
}