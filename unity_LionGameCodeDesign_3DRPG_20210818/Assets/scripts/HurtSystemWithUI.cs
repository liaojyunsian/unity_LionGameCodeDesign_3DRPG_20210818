using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sky
{
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("要更新的血條")]
        public Image imgHp;

        /// <summary>
        /// 血條效果專用的扣血前血量
        /// </summary>
        private float hpBarEffectOriginal;
        //複寫父類別成員 override
        public override bool Hurt(float damage)
        {
            hpBarEffectOriginal = hp;

            //該成員的父類別基底 父類別內的內容
            base.Hurt(damage);

            StartCoroutine(HpBarEffect());

            return hp <= 0;
        }

        /// <summary>
        /// 血條效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator HpBarEffect()
        {
            while (hpBarEffectOriginal != hp)//當扣血前血亮度等於血量
            {
                hpBarEffectOriginal--;//遞減
                imgHp.fillAmount = hpBarEffectOriginal / hpMax;//更新血條
                yield return new WaitForSeconds(0.01f);//等待
            }
        }
    }
}
