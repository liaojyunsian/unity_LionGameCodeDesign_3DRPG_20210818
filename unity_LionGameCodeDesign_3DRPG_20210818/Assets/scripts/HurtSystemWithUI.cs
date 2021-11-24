using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sky
{
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("�n��s�����")]
        public Image imgHp;

        /// <summary>
        /// ����ĪG�M�Ϊ�����e��q
        /// </summary>
        private float hpBarEffectOriginal;
        //�Ƽg�����O���� override
        public override void Hurt(float damage)
        {
            hpBarEffectOriginal = hp;

            //�Ӧ����������O�� �����O�������e
            base.Hurt(damage);

            StartCoroutine(HpBarEffect());
        }

        /// <summary>
        /// ����ĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator HpBarEffect()
        {
            while (hpBarEffectOriginal != hp)//����e��G�׵����q
            {
                hpBarEffectOriginal--;//����
                imgHp.fillAmount = hpBarEffectOriginal / hpMax;//��s���
                yield return new WaitForSeconds(0.01f);//����
            }
        }
    }
}
