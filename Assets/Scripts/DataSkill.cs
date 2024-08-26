using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// �ޯ���
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Skill", order = 1)]
    public class DataSkill : DataAttack
    {
        [Header("�ޯ����B�ʵe�ѼơB�ʵe�ɶ�")]
        public KeyCode skillKey;
        public string skillParameter;
        [Range(0, 10)]
        public float skillAnimationTime;
    }
}