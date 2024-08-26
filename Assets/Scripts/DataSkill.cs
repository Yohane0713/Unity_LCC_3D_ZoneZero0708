using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 技能資料
    /// </summary>
    [CreateAssetMenu(menuName = "Mtaka/Skill", order = 1)]
    public class DataSkill : DataAttack
    {
        [Header("技能按鍵、動畫參數、動畫時間")]
        public KeyCode skillKey;
        public string skillParameter;
        [Range(0, 10)]
        public float skillAnimationTime;
    }
}