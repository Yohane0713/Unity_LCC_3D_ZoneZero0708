using UnityEngine;

namespace Mtaka
{
    public class AttackAreaObject : MonoBehaviour, IAttack
    {
        public float attack
        {
            get => _attack + Mathf.Ceil(Random.Range(0, _attack * attackFloat));
        }

        [SerializeField, Header("§ğÀ»¤O"), Range(0, 1000)]
        private float _attack;
        [SerializeField, Header("§ğÀ»¤O¯B°Ê"), Range(0, 1)]
        private float attackFloat;
    }
}