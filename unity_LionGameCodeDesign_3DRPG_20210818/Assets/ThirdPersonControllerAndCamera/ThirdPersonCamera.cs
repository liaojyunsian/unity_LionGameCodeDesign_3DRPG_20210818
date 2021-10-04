using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ĤT�H����v���t��
/// �l�ܫ��w�ؼ�
/// �åB�i�H���k�B�W�U����(����)
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    #region ���
    [Header("�ؼЪ���")]
    public Transform target;
    [Header("�l�ܳt��"), Range(0, 100)]
    public float speedTrack = 1.5f;
    [Header("���४�k�t��"), Range(0, 100)]
    public float speedTurnHorizontal = 5;
    [Header("����W�U�t��"), Range(0, 100)]
    public float speedTurnVertical = 5;
    [Header("X �b�W�U����ȭ��� : �̤p�P�̤j��")]
    public Vector2 limitAngleX = new Vector2(-0.2f, 0.2f);
    [Header("��v���b����e��W�U���୭�� : �̤p�P�̤j��")]
    public Vector2 limitAngleFromTarget = new Vector2(-0.2f, 0);
    /// <summary>
    /// ��v���e��y��
    /// </summary>
    private Vector3 _posForward;
    /// <summary>
    /// �e�誺����
    /// </summary>
    private float lengthForward = 3;
    #endregion

    #region �ݩ�
    /// <summary>
    /// ���o�ƹ������y��
    /// </summary>
    private float inputMouseX { get => Input.GetAxis("Mouse X"); }
    /// <summary>
    /// �����ƹ������y��
    /// </summary>
    private float inpetMouseY { get => Input.GetAxis("Mouse Y"); }

    /// <summary>
    /// ��v���e��y��
    /// </summary>
    public Vector3 posForward
    {
        get
        {
            _posForward = transform.position + transform.forward * lengthForward;
            _posForward.y = target.position.y;
            return _posForward;
        }
    }
    #endregion

    #region �ƥ�
    private void Update()
    {
        TurnCamera();
        LimitAngleX();
        FreezeAngleZ();
    }
    /// <summary>
    /// �bUpdate �����D�B�z��v���l�ܦ欰
    /// </summary>
    private void LateUpdate()
    {
        TrackTarget();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // �e��y�� = ����y�� + ������e�� * ����
        _posForward = transform.position + transform.forward * lengthForward;
        // �e��y��.y = �ؼ�.�y��.y(���e��y�Ъ����׻P�ؼЬۦP)
        _posForward.y = target.position.y;
        Gizmos.DrawSphere(_posForward, 0.15f);

    }
    #endregion

    #region ��k
    /// <summary>
    /// �l�ܥؼ�
    /// </summary>
    private void TrackTarget()
    {
        Vector3 posTarget = target.position;// ���o �ؼ� �y��
        Vector3 posCamera = transform.position;//���o ��v�� �y��

        //��v���y�� = ���� (�t�� * �@�V���ɶ�)
        posCamera = Vector3.Lerp(posCamera, posTarget, speedTrack * Time.deltaTime);//��v���y�� = ����
        transform.position = posCamera;//�����󪺮y�� = ��v���y��
    }
    /// <summary>
    /// ������v��
    /// </summary>
    private void TurnCamera()
    {
        transform.Rotate(
            inpetMouseY * Time.deltaTime * speedTurnVertical,
            inputMouseX * Time.deltaTime * speedTurnHorizontal,
            0);
    }
    /// <summary>
    /// ����� X �b �P �b�ؼЫe�誺 Z �b
    /// </summary>
    private void LimitAngleX()
    {
        //print("��v�������׭� : " + transform.rotation);//0.3 0.3
        Quaternion angle = transform.rotation;// ���o�|�줸����
        angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);// ������X�b
        angle.z = Mathf.Clamp(angle.z, limitAngleFromTarget.x, limitAngleFromTarget.y);
        transform.rotation = angle;// ��s���󨤫�
    }
    /// <summary>
    /// �ᵲ���� Z �b���s
    /// </summary>
    private void FreezeAngleZ()
    {
        Vector3 angle = transform.eulerAngles;// ���o�T������
        angle.z = 0;// �ᵲ Z �b���s
        transform.eulerAngles = angle;// ��s���󨤫�
    }
    #endregion

}
