using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �D�R�A�m��
/// </summary>
public class APINotStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer spriteRenderer;

    [Header("�Ϥ��@")]
    public SpriteRenderer spriteRenderer_1;
    public Transform transform;
    [Header("�Ϥ��G")]
    public SpriteRenderer spriteRenderer_2;
    public Rigidbody2D rigidbody2D;

    private void Start()
    {
        #region �D�R�A�ݩ�
        // ���o Get
        //��v���`�� (Depth)
        print("��v�����`�סG" + cam.depth);
        //�Ϥ����C��
        print("�Ϥ����C��G" + spriteRenderer.color);

        // �]�w set
        // ��v�����I���C�� (�H���C��)
        cam.backgroundColor = Random.ColorHSV();
        //�Ϥ����W�U½��
        spriteRenderer.flipY = true;
        #endregion
    }

    private void Update()
    {
        //���Ĥ@�i�Ϥ��������
        transform.Rotate(0.0f, 9.0f, 0.0f);
        //���ĤG�i�Ϥ��i�H���W��
        rigidbody2D.AddForce(new Vector2(0, 30));
    }


}
