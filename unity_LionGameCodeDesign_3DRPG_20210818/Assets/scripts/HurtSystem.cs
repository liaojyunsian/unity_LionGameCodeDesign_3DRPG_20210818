using UnityEngine;
using UnityEngine.Events;

namespace Sky
{
    /// <summary>
    /// ���˨t��
    /// �B�z��q�B���˻P���`
    /// </summary>
    public class HurtSystem : MonoBehaviour
    {
        #region ���Q���}
        [Header("��q"), Range(0, 5000)]
        public float hp = 100;
        [Header("���˨ƥ�")]
        public UnityEvent onHurt;
        [Header("���`�ƥ�")]
        public UnityEvent onDead;
        [Header("�ʵe�ѼơQ���˻P���`")]
        public string parameterHurt = "����Ĳ�o";
        public string parameterDead = "���`�}��";
        #endregion

        #region ���Q�p�H �P�O�@
        private Animator ani;
        //private �p�H �����\�b�l���O�s��
        //public ���} ���\�Ҧ����O�s��
        //protected �O�@ �ȭ��l���O�s��
        protected float hpMax;
        #endregion

        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            hpMax = hp;
        }
        #endregion

        #region ��k�Q���}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage">�����쪺�ˮ`</param>
        //�����n�Q�l���O�Ƽg�����[�W virtual ����
        public virtual bool Hurt(float damage)
        {
            //�p�G ���`�ѼƤw�g�Ŀ� �N���X
            if (ani.GetBool(parameterDead))
            {
                return true;
            }
            hp -= damage;
            ani.SetTrigger(parameterHurt);
            onHurt.Invoke();
            if (hp <= 0)
            {
                Dead();
                return true;
            }
            else return false;
        }
        #endregion

        #region ��k�Q�p�H
        private void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }
        #endregion

    }
}