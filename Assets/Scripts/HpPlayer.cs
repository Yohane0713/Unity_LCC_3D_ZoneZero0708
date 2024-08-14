using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mtaka
{
    /// <summary>
    /// 血量系統：玩家
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
            imgHp = GameObject.Find("圖片_血條").GetComponent<Image>();
            textHp = GameObject.Find("文字_血量").GetComponent<TMP_Text>();
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