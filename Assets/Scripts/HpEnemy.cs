using System;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
// System和UnityEngine都有隨機方法可以用 會衝突

namespace Mtaka
{
    /// <summary>
    /// 血量系統：敵人
    /// </summary>
    public class HpEnemy : HpSystem
    {
        public event EventHandler<float> onImbalance;

        [SerializeField, Header("群組_敵人血量介面")]
        private GameObject prefabHpUI;

        private StateMachine stateMachine;
        private CapsuleCollider capsuleCollider;
        /// <summary>
        /// 畫布主要介面
        /// </summary>
        private Transform canvas;
        private Image imgImbalance;
        private TMP_Text textImbalance;
        private float imbalance, imbalanceMax = 100;
        private string parImbalance = "觸發失衡";
        private DataHpEnemy dataHpEnemy;

        protected override void Awake()
        {
            base.Awake();
            dataHpEnemy = (DataHpEnemy)dataHp;
            stateMachine = GetComponent<StateMachine>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            canvas = GameObject.Find("畫布主要介面").transform;
            parDamages = new string[] { "觸發受傷1", "觸發受傷2" };
            SpawnUI();
        }

        protected override void Damage(float damage, float imbalance = 0)
        {
            base.Damage(damage);
            string parDamage = parDamages[Random.Range(0, parDamages.Length)];
            // 如果不是失衡 就 執行受傷動畫
            if (!isImbalancing) ani.SetTrigger(parDamage);
            Imbalance(imbalance);
        }

        protected override void Dead()
        {
            base.Dead();
            stateMachine.enabled = false;
            capsuleCollider.enabled = false;
        }

        private bool isImbalancing;

        /// <summary>
        /// 失衡：數值與判定
        /// </summary>
        /// <param name="imbalanceValue">要添加的失衡值</param>
        private void Imbalance(float imbalanceValue)
        {
            // 如果 失衡中 就跳出
            if (isImbalancing) return;
            imbalance += imbalanceValue;
            imbalance = Mathf.Clamp(imbalance, 0, imbalanceMax);
            imgImbalance.fillAmount = imbalance / imbalanceMax;
            textImbalance.text = imbalance.ToString();
            // 失衡值 大於等於 失衡值上限 就開始失衡
            if (imbalance >= imbalanceMax) StartImbalance();
        }

        /// <summary>
        /// 開始失衡
        /// </summary>
        private void StartImbalance()
        {
            isImbalancing = true;
            ani.SetTrigger(parImbalance);
            onImbalance?.Invoke(this, dataHpEnemy.imbalanceTime);
            StartCoroutine(Imbalancing(dataHpEnemy.imbalanceTime));
        }

        /// <summary>
        /// 失衡中
        /// </summary>
        private IEnumerator Imbalancing(float imbalanceTime)
        {
            float interval = 0.02f;
            float count = imbalanceTime / interval;
            float fill = 1 / count;

            while (count > 0)
            {
                // 等待一個影格
                yield return new WaitForSeconds(interval);
                count --;
                imgImbalance.fillAmount -= fill;
                // F0 是不要小數點的意思
                textImbalance.text = (imgImbalance.fillAmount * 100).ToString("F0");
            }

            ImbalanceFinish();
        }

        /// <summary>
        /// 失衡結束
        /// </summary>
        private void ImbalanceFinish()
        {
            imbalance = 0;
            isImbalancing = false;
        }

        /// <summary>
        /// 生成介面
        /// </summary>
        private void SpawnUI()
        {
            GameObject tempHpUI = Instantiate(prefabHpUI, canvas);
            tempHpUI.GetComponent<WorldToUI>().targetPoint = transform;
            imgHp = tempHpUI.transform.Find("圖片_血條").GetComponent<Image>();
            imgImbalance = tempHpUI.transform.Find("圖片_失衡").GetComponent<Image>();
            textImbalance = tempHpUI.transform.Find("文字_失衡").GetComponent<TMP_Text>();
        }
    }
}