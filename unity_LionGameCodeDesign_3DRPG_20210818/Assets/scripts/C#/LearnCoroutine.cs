using System.Collections;//�ޥ� �t��.���X �R�W�Ŷ� ��P�{�� API
using System.Collections.Generic;
using UnityEngine;

namespace Sky.Practice
{
    /// <summary>
    /// �{�Ѩ�P�{�Ǥ�k
    /// </summary>
    public class LearnCoroutine : MonoBehaviour
    {
        //�w�q��P�{�Ǥ�k
        //IEnumerator ����P�{�ǶǦ^�ȡD�i�Ǧ^�ɶ�
        //yield ���B
        //new WaitForSeconds(�B�I��) - ���ݮɶ�
        private IEnumerator TestCoroutine()
        {
            print("��P�{�Ƕ}�l����");
            yield return new WaitForSeconds(2f);
            print("��P�{�ǵ��ݨ���}�l���榹��");

        }

        public Transform sphere;

        private IEnumerator SphereScale()
        {
            yield return new WaitForSeconds(1);
            sphere.localScale += Vector3.one;
        }

        private void Start()
        {
            //�Ұʨ�P�{��
            StartCoroutine(TestCoroutine());
            StartCoroutine(SphereScale());
        }
    }
}