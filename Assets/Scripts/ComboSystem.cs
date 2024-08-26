using StarterAssets;
using System;
using System.Collections;
using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 連段系統：處理攻擊連段
    /// </summary>
    public class ComboSystem : MonoBehaviour
    {
        [SerializeField, Header("最大攻擊連段數"), Range(0, 10)]
        private int comboMax;
        [SerializeField, Header("攻擊連段時間"), Range(0, 2)]
        private float breakComboTime = 1;

        /// <summary>
        /// 連段編號：-1 尚未開始 0 代表連段1
        /// </summary>
        private int comboIndex = -1;
        private string parAttack = "觸發攻擊";
        private string parComboValue = "攻擊連段數值";
        private Animator ani;
        private WaitForSeconds waitBreakComboTime;
        private float[] animationsLength = { 1.33f, 1.58f, 1.66f, 2.23f };
        private WaitForSeconds[] waitAnimationsTime;
        private bool isAttacking;
        private ThirdPersonController thirdPersonController;
        private bool usingSkill;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            thirdPersonController = GetComponent<ThirdPersonController>();

            // 最佳化：初始化等待時間
            waitBreakComboTime = new WaitForSeconds(breakComboTime);

            waitAnimationsTime = new WaitForSeconds[comboMax];
            for (int i = 0; i < comboMax; i++)
            {
                waitAnimationsTime[i] = new WaitForSeconds(animationsLength[i]);
            }

            SkillManager.instance.onSkill += OnSkill;
        }

        private void OnSkill(object sender, EventArgs e)
        {
            usingSkill = true;
        }

        private void Update()
        {
            ComboIndexHandle();
        }

        /// <summary>
        /// 連段編號處理
        /// </summary>
        private void ComboIndexHandle()
        {
            // 如果 使用技能中 就 跳出
            if (usingSkill) return;
            // 如果 不在地板上 就 跳出
            if (!thirdPersonController.Grounded) return;
            // 如果 攻擊中 就 跳出
            if (isAttacking) return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // 編號處理
                comboIndex++;
                // 如果連段編號 等於 連段最大值 就歸零
                if (comboIndex == comboMax) comboIndex = 0;
                print($"<color=#f36>連段編號：{comboIndex}</color>");
                UpdateAnimation();
                // 避免重複中斷，停止所有協同程序
                StopAllCoroutines();
                StartCoroutine(AttackTimeHandle());
            }
        }

        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnimation()
        {
            ani.SetTrigger(parAttack);
            ani.SetFloat(parComboValue, comboIndex);
        }

        private IEnumerator AttackTimeHandle()
        {
            // 進入攻擊中
            isAttacking = true;
            thirdPersonController.isAttacking = true;
            // 等待當前攻擊動畫結束 (動畫長度)
            yield return waitAnimationsTime[comboIndex];
            // 攻擊結束
            isAttacking = false;
            thirdPersonController.isAttacking = false;

            yield return waitBreakComboTime;
            comboIndex = -1;
            print($"<color=#f33>中斷連擊：{comboIndex}</color>");
        }
    }
}