using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mtaka
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class HpSystem : MonoBehaviour
    {
        [SerializeField, Header("血量資料")]
        protected DataHp dataHp;
        [SerializeField, Header("造成傷害標籤")]
        private string damageObjectTag;
        [SerializeField, Header("文字傷害值預製物")]
        private GameObject prefabTextDamage;
        [SerializeField, Header("文字傷害值位移"), Range(0, 500)]
        private float textDamageOffset;

        private Transform transformCanvas;
        protected float hp, hpMax;
        protected Animator ani;
        protected string[] parDamages;
        protected string parDead = "觸發死亡";
        protected Image imgHp;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            transformCanvas = GameObject.Find("畫布主要介面").transform;
            hpMax = dataHp.hp;
            hp = hpMax;
        }

        // OTE 觸發事件：碰到勾選 Is Trigger 物件會執行一次
        private void OnTriggerEnter(Collider other)
        {
            if (hp <= 0) return;
            // 如果 碰到物件的標籤 等於 造成傷害標籤就受傷
            if (other.tag == damageObjectTag)
            {
                float attack = other.GetComponent<IAttack>().attack;
                float imbalance = other.GetComponent<IAttack>().imbalance;
                Damage(attack, imbalance);
                SpawnTextDamageAndUpdateText(attack);
            }
        }

        protected virtual void Damage(float damage, float imbalance = 0)
        {
            // 如果 血量 <= 0 就跳出
            if (hp <= 0) return;
            hp -= damage;
            hp = Mathf.Clamp(hp, 0, hpMax);   
            UpdateUI();
            if (hp <= 0) Dead();
        }

        /// <summary>
        /// 更新介面
        /// </summary>
        protected virtual void UpdateUI()
        {
            imgHp.fillAmount = hp / hpMax;
        }

        protected virtual void Dead()
        {
            ani.SetTrigger(parDead);
        }

        /// <summary>
        /// 生成文字傷害值並更新文字
        /// </summary>
        /// <param name="damage">傷害值</param>
        private void SpawnTextDamageAndUpdateText(float damage)
        {
            // 生成文字傷害值預製物，並指定父物件為「畫布主要介面」
            GameObject tempTextDamage = Instantiate(prefabTextDamage, transformCanvas);
            tempTextDamage.GetComponent<TMP_Text>().text = damage.ToString();
            // 獲得傷害值管理器並給予座標與位移資訊
            WorldToUI tempDamage = tempTextDamage.GetComponent <WorldToUI>();
            tempDamage.targetPoint = transform;
            tempDamage.offset = textDamageOffset;
            Destroy(tempDamage, 1.5f);
        }
    }
}