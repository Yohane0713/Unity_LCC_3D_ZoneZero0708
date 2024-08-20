using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
// System�MUnityEngine�����H����k�i�H�� �|�Ĭ�

namespace Mtaka
{
    /// <summary>
    /// ��q�t�ΡG�ĤH
    /// </summary>
    public class HpEnemy : HpSystem
    {
        public event EventHandler onImbalance;

        [SerializeField, Header("�s��_�ĤH��q����")]
        private GameObject prefabHpUI;

        private StateMachine stateMachine;
        private CapsuleCollider capsuleCollider;
        /// <summary>
        /// �e���D�n����
        /// </summary>
        private Transform canvas;
        private Image imgImbalance;
        private TMP_Text textImbalance;
        private float imbalance, imbalanceMax = 100;
        private string parImbalance = "Ĳ�o����";
        private DataHpEnemy dataHpEnemy;

        protected override void Awake()
        {
            base.Awake();
            dataHpEnemy = (DataHpEnemy)dataHp;
            stateMachine = GetComponent<StateMachine>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            canvas = GameObject.Find("�e���D�n����").transform;
            parDamages = new string[] { "Ĳ�o����1", "Ĳ�o����2" };
            SpawnUI();
        }

        protected override void Damage(float damage, float imbalance = 0)
        {
            base.Damage(damage);
            string parDamage = parDamages[Random.Range(0, parDamages.Length)];
            ani.SetTrigger(parDamage);
            Imbalance(imbalance);
        }

        protected override void Dead()
        {
            base.Dead();
            stateMachine.enabled = false;
            capsuleCollider.enabled = false;
        }

        /// <summary>
        /// ���šG�ƭȻP�P�w
        /// </summary>
        /// <param name="imbalanceValue">�n�K�[�����ŭ�</param>
        private void Imbalance(float imbalanceValue)
        {
            imbalance += imbalanceValue;
            imbalance = Mathf.Clamp(imbalance, 0, imbalanceMax);
            imgImbalance.fillAmount = imbalance / imbalanceMax;
            textImbalance.text = imbalance.ToString();
            // ���ŭ� �j�󵥩� ���ŭȤW�� �N�}�l����
            if (imbalance >= imbalanceMax) StartImbalance();
        }

        /// <summary>
        /// �}�l����
        /// </summary>
        private void StartImbalance()
        {
            ani.SetTrigger(parImbalance);
            onImbalance?.Invoke(this, null);
        }

        /// <summary>
        /// �ͦ�����
        /// </summary>
        private void SpawnUI()
        {
            GameObject tempHpUI = Instantiate(prefabHpUI, canvas);
            tempHpUI.GetComponent<WorldToUI>().targetPoint = transform;
            imgHp = tempHpUI.transform.Find("�Ϥ�_���").GetComponent<Image>();
            imgImbalance = tempHpUI.transform.Find("�Ϥ�_����").GetComponent<Image>();
            textImbalance = tempHpUI.transform.Find("��r_����").GetComponent<TMP_Text>();
        }
    }
}