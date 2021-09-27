using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIStaticPractice : MonoBehaviour
{
    private int count;



    private void Start()
    {
        #region ���o �R�A�ݩ�
        // �Ҧ���v���ƶq
        count = Camera.allCamerasCount;
        print("�Ҧ���v���ƶq�G" + count);
        // 2D �����O�j�p
        Vector2 gravity = Physics2D.gravity;
        print("2D�����O�j�p�G" + gravity);
        // ��P�v
        float p = Mathf.PI;
        print("��P�v = " + p.ToString("0.000"));
        #endregion
        #region �]�w �R�A�ݩ�
        // 2D �����O�j�p�]�w�� Y -20
        Physics2D.gravity = new Vector2(0, -20);
        print("2D�����O�j�p�G" + gravity);
        // �ɶ��j�p�]�w�� 0.5 (�C�ʧ@)
        Time.timeScale = 0.5f;
        float t = Time.timeScale;
        print("�C�ʧ@�ɶ��G" + p.ToString("0.000"));
        #endregion
        #region �I�s �R�A��k
        // �� 9.999 �h�p���I
        Debug.Log(Mathf.Floor(9.999F));
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
        print("�C���g�L�ɶ�" + Time.timeSinceLevelLoad);
        #endregion
        #region �I�s �R�A��k
        // �O�_���U���� (���w���ť���)
        print("���U�ť���" + Input.GetKey(KeyCode.Space));
        #endregion
    }
}
