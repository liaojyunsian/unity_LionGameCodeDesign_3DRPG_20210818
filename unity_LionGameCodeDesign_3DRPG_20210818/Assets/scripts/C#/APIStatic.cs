using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{��API�R�A Static
/// </summary>
public class APIStatic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region �R�A�ݩ�
        // ���ݭn���骫��
        // ���ݭn���o���骫��
        // ���o Get
        // �y�k�G
        // ���O�W��.�R�A�ݩ�
        float r = Random.value;
        print("���o�R�A�ݩʡD�H���ȡG" + r);

        // �]�w Set
        // �y�k�G
        // ���O�W��.�R�A�ݩ� ���w ��;
        Cursor.visible = false;
        #endregion

        #region �R�A��k
        // �I�s�D�ѼơB�Ǧ^
        // ñ���G�ѼơB�Ǧ^
        // �y�k�G
        // ���O�W��.�R�A��k(�����޼�)
        float range = Random.Range(10.5f, 20.9f);
        print("�H���d�� 10.5 ~ 20.9�G" + range);

        // �� API �����ܭ��n�G�ϥξ�Ʈɤ��]�t�̤j��
        int rangeInt = Random.Range(1, 2);
        print("�H���d�� 1 ~ 2�G" + rangeInt);
        #endregion
    }

    private void Update()
    {
        #region �R�A�ݩ�
        // ���o Get
        // �y�k�G
        // ���O�W��.�R�A�ݩ�
        print("�g�L�h�[�G" + Time.timeSinceLevelLoad);// time �ɶ�
        #endregion

        #region �R�A��k
        float h = Input.GetAxis("Horizontal");
        print("�����ȡG" + h);
        #endregion
    }
}
