using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APINotStatic : MonoBehaviour
{
    public Transform tra1;
    public Camera cam;
    public Light lig;

    private void Start()
    {
        #region �D�R�A�ݩ�
        // �P�R�A�t��
        // 1.�ݭn���骫��
        // 2.�ݭn���o���骫�� - �w�q���ñN�n�s��������s�J���
        // 3.�C������B���󥲶��n�s�b������
        // ���o Get
        // �y�k�G
        // ���W��.�D�R�A�ݩ�
        print("��v�����y�СG" + tra1.position);
        print("��v�����`�סG" + cam.depth);

        // �]�w set
        // �y�k�G
        // ���W��.�D�R�A�ݩ� ���w ��;
        tra1.position = new Vector3(99, 99, 99);
        cam.depth = 7;

        #endregion

        #region �D�R�A��k
        // �I�s
        // �y�k�G
        // ���W��.�D�R�A�ݩ�
        lig.Reset();
        #endregion
    }
}
