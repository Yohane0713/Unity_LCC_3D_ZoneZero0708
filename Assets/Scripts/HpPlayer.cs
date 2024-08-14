using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mtaka
{
    /// <summary>
    /// ��q�t�ΡG���a
    /// </summary>
    public class HpPlayer : HpSystem
    {
        protected TMP_Text textHp;
        private ThirdPersonController thirdPersonController;
        private ComboSystem comboSystem;

        protected override void Awake()
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
            comboSystem = GetComponent<ComboSystem>();
            base.Awake();
            imgHp = GameObject.Find("�Ϥ�_���").GetComponent<Image>();
            textHp = GameObject.Find("��r_��q").GetComponent<TMP_Text>();
            UpdateUI();
        }

        protected override void Dead()
        {
            base.Dead();
            thirdPersonController.enabled = false;
            comboSystem.enabled = false;
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
            textHp.text = $"{hp}/{hpMax}";
        }
    }
}