using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// 攻擊區域物件
    /// </summary>
    public class AttackAreaObject : MonoBehaviour, IAttack
    {
        public float attack
        {
            get => 
                dataAttack.attack +
                Mathf.Ceil(Random.Range(0, dataAttack.attack * dataAttack.attackFloat));
        }

        public float imbalance
        {
            get =>
                dataAttack.imbalance +
                Mathf.Ceil(Random.Range(0, dataAttack.imbalance * dataAttack.imbalanceFloat));
        }

        [SerializeField, Header("攻擊資料")]
        private DataAttack dataAttack;
    }
}