using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIStaticPractice : MonoBehaviour
{
    private void Start()
    {
        #region ���o �R�A�ݩ�
        // �Ҧ���v���ƶq
        /*
        int count = Camera.allCamerasCount;
        print("�Ҧ���v���ƶq�G" + count);
        /**/
        print("�Ҧ���v���ƶq�G" + Camera.allCamerasCount);
        // 2D �����O�j�p
        /*
        Vector2 gravity = Physics2D.gravity;
        print("2D�����O�j�p�G" + gravity);
        */
        print("2D�����O�j�p�G" + Physics2D.gravity);
        // ��P�v
        /*
        float p = Mathf.PI;
        print("��P�v = " + p.ToString("0.000"));
        */
        print("��P�v = "+Mathf.PI);
        #endregion

        #region �]�w �R�A�ݩ�
        // 2D �����O�j�p�]�w�� Y -20
        Physics2D.gravity = new Vector2(0, -20);
        print("2D�����O�j�p�G" + Physics2D.gravity);
        // �ɶ��j�p�]�w�� 0.5 (�C�ʧ@)
        Time.timeScale = 0.5f;
        float t = Time.timeScale;
        print("�C�ʧ@�ɶ��G" + t.ToString("0.000"));
        #endregion

        #region �I�s �R�A��k
        // �� 9.999 �h�p���I
        Debug.Log(Mathf.Round(9.999F));
        //���o���I���Z�� new Vector3(1, 1, 1) new Vector3(22, 22, 22)
        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b = new Vector3(22, 22, 22);
        float distance = Vector3.Distance(a, b);
        print("���I���Z���G" + distance);
        //�}�ҳs�� https://unity.com/
        Application.OpenURL("https://unity.com/");
        #endregion
    }
    private void Update()
    {
        #region ���o �R�A�ݩ�
        // �O�_��J���N��
        print("�O�_��J���N��" + Input.anyKey);
        // �C���g�L�ɶ�
        print("�C���g�L�ɶ�" + Time.time);
        #endregion

        #region �I�s �R�A��k
        // �O�_���U���� (���w���ť���)
        print("���U�ť���" + Input.GetKey(KeyCode.Space));
        #endregion
    }
}
