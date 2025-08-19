using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mtaka
{
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<SkillManager>();
                return _instance;
            }
        }
        private static SkillManager _instance;

        public event EventHandler onSkill;

        [SerializeField, Header("技能資料")]
        private DataSkill dataSkill;
        [SerializeField, Header("恢復魔力間隔"), Range(0, 2)]
        private float mpCureInterval = 1;

        private Animator ani;
        private Image imgMp;
        private float mp, mpMax = 100;
        private WaitForSeconds waitMpCureInterval;
        private WaitForSeconds waitSkillAnimationTime;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            imgMp = GameObject.Find("圖片_魔力").GetComponent<Image>();
            waitMpCureInterval = new WaitForSeconds(mpCureInterval);
            waitSkillAnimationTime = new WaitForSeconds(dataSkill.skillAnimationTime);
            UpdateUI();
            StartCoroutine(CureMp());
        }

        private void Update()
        {
            SkillInput();
        }

        /// <summary>
        /// 技能輸入
        /// </summary>
        private void SkillInput()
        {
            // 如果 魔力小於魔力最大值就跳出
            if (mp < mpMax) return;

            if (Input.GetKeyDown(dataSkill.skillKey)) StartCoroutine(Skill());
        }

        private IEnumerator Skill()
        {
            mp = 0;
            UpdateUI();
            ani.applyRootMotion = true;
            ani.SetTrigger(dataSkill.skillParameter);
            yield return waitSkillAnimationTime;
            ani.applyRootMotion = false;
        }

        private IEnumerator CureMp()
        {
            // 如果等於魔力最大值就跳出
            if (mp == mpMax) yield break;
            // 如果小於魔力最大值就恢復
            while (mp < mpMax)
            {
                mp++;
                UpdateUI();
                yield return waitMpCureInterval;
            }
        }

        private void UpdateUI()
        {
            imgMp.fillAmount = mp / mpMax;
        }
    }
}