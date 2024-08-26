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
            // 代理器的速度歸零
            agent.speed = 0;
        }

        /// <summary>
        /// 失衡結束
        /// </summary>
        private IEnumerator ImbalanceFinish()
        {
            yield return waitImbalanceTime;
            print("失衡結束");
        }
    }
}