using TMPro;
using UnityEngine;

namespace Mtaka
{
    /// <summary>
    /// ��q�t��
    /// </summary>
    public class HpSystem : MonoBehaviour
    {
        [SerializeField, Header("��q���")]
        protected DataHp dataHp;
        [SerializeField, Header("�y���ˮ`����")]
        private string damageObjectTag;
        [SerializeField, Header("��r�ˮ`�ȹw�s��")]
        private GameObject prefabTextDamage;
        [SerializeField, Header("��r�ˮ`�Ȧ첾"), Range(0, 500)]
        private float textDamageOffset;

        private Transform transformCanvas;
        protected float hp, hpMax;
        protected Animator ani;
        protected string[] parDamages;
        protected string parDead = "Ĳ�o���`";

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            transformCanvas = GameObject.Find("�e���D�n����").transform;
            hpMax = dataHp.hp;
            hp = hpMax;
        }

        // OTE Ĳ�o�ƥ�G�I��Ŀ� Is Trigger ����|����@��
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == damageObjectTag)
            {
                float attack = other.GetComponent<IAttack>().attack;
                print($"<color=#933>�y���ˮ`�G{attack}</color>");
                Damage(attack);
                SpawnTextDamageAndUpdateText(attack);
            }
        }

        protected virtual void Damage(float damage)
        {
            // �p�G ��q <= 0 �N���X
            if (hp <= 0) return;
            hp -= damage;
            hp = Mathf.Clamp(hp, 0, hpMax);
            string parDamage = parDamages[Random.Range(0, parDamages.Length)];
            ani.SetTrigger(parDamage);
            if (hp <= 0) Dead();
        }

        protected virtual void Dead()
        {
            ani.SetTrigger(parDead);
        }

        /// <summary>
        /// �ͦ���r�ˮ`�Ȩç�s��r
        /// </summary>
        /// <param name="damage">�ˮ`��</param>
        private void SpawnTextDamageAndUpdateText(float damage)
        {
            // �ͦ���r�ˮ`�ȹw�s���A�ë��w�����󬰡u�e���D�n�����v
            GameObject tempTextDamage = Instantiate(prefabTextDamage, transformCanvas);
            tempTextDamage.GetComponent<TMP_Text>().text = damage.ToString();
            // ��o�ˮ`�Ⱥ޲z���õ����y�лP�첾��T
            DamageManager tempDamage = tempTextDamage.GetComponent <DamageManager>();
            tempDamage.targetPoint = transform;
            tempDamage.offset = textDamageOffset;
            Destroy(tempDamage, 1.5f);
        }
    }
}