using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mtaka
{
    /// <summary>
    /// �ޯ�޲z��
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

        [SerializeField, Header("�ޯ���")]
        private DataSkill dataSkill;
        [SerializeField, Header("��_�]�O���j"), Range(0, 2)]
        private float mpCureInterval = 1;

        private Animator ani;
        private Image imgMp;
        private float mp, mpMax = 100;
        private WaitForSeconds waitMpCureInterval;
        private WaitForSeconds waitSkillAnimationTime;

        private void Awake()
        {
            ani = GetComponent<Animator>();
            imgMp = GameObject.Find("�Ϥ�_�]�O").GetComponent<Image>();
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
        /// �ޯ��J
        /// </summary>
        private void SkillInput()
        {
            // �p�G �]�O�p���]�O�̤j�ȴN���X
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
            // �p�G�����]�O�̤j�ȴN���X
            if (mp == mpMax) yield break;
            // �p�G�p���]�O�̤j�ȴN��_
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