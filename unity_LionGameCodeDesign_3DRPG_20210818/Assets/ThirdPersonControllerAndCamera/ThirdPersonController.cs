using UnityEngine;          // �ޥ� Unity API (�ܮw - ��ƻP�\��)
using UnityEngine.Video;    // �ޥ� �v�� API

/// <summary>
/// Sky 2021.0906
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region ��� Field
    [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(1, 500)]
    public float speed = 10.5f;
    [Header("���D����"), Tooltip("�Ψӽվ㨤����D����"), Range(0, 1000)]
    public int jump = 100;

    [Header("�O�_���b�a�O�W"), Tooltip("�ΨӽT�w����O�_���b�a�O�W")]
    public bool isGrounded;
    [Header("�ˬd�a�O�첾(�T���V�q)")]
    public Vector3 v3CheckGroundOffset;
    [Header("�ˬd�a�O�b�|"), Range(0, 3)]
    public float checkGroundRadius = 0.1f;

    [Header("���D����")]
    public AudioClip soundJump;
    [Header("���a����")]
    public AudioClip soundGround;

    [Header("�ʵe�Ѽƨ����}��")]
    public string animatorParWalk = "�����}��";
    public string animatorParRun = "�]�B�}��";
    public string animatorParHurt = "����Ĳ�o";
    public string animatorParDead = "���`�}��";
    public string animatorParjump = "���DĲ�o";
    public string animatorParIsGrounded = "�O�_�b�a�O�W";

    [Header("���a����")]
    public GameObject playerObject;

    [Header("���� ���Ĩӷ�")]
    private AudioSource aud;
    [Header("���� ����")]
    private Rigidbody rig;
    [Header("�ʵe���")]
    private Animator ani;
    #endregion

    #region �ݩ� Property
    // C# 6.0 �s���l �i�H�ϥ� Lambda => �B��l
    // �y�k get => {�{���϶�} - ���i�ٲ��j�A��
    private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
    private bool keyMove { get => MoveInput("Vertical") != 0 | MoveInput("Horizontal") != 0; }

    private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
    #endregion

    #region ��k Method
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="speedMove">���ʳt��</param>
    private void Move(float speedMove)
    {
        // �Ш��� animator �ݩ� Apply Root Motion �G�Ŀ�ɨϥΰʵe�첾��T
        // ����.�[�t�� = �T���V�q - �[�t�ץΨӱ����sa��T�Ӷb�V���B�ʳt��
        // �e��(forward) * ��J��(Vertical) * ����(speedMove)
        // �ϥΫe�ᥪ�k�b�V�B�ʨëO���쥻�a�ߤޤO
        // Vector3.forward �@�ɮy�� �� �e�� (����)
        // transform.forward ������ �� �e�� (�ϰ�)
        rig.velocity =
            transform.forward * MoveInput("Vertical") * speedMove +
            transform.right * MoveInput("Horizontal") * speedMove +
            Vector3.up * rig.velocity.y;
    }

    /// <summary>
    /// ���ʫ����J
    /// </summary>
    /// <param name="axisName">�n���o���b�V�W��</param>>
    /// <returns></returns>
    private float MoveInput(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    /// <summary>
    /// �ˬd�a�O
    /// </summary>
    /// <returns>�O�_�I��a�O</returns>
    private bool CheckGround()
    {
        // ���z.�л\�y��(�����I�D�b�|�D�ϼh)
        Collider[] hits = Physics.OverlapSphere
            (
            transform.position +                        // �⪫��y�Сר���y��
            transform.right * v3CheckGroundOffset.x +   // �վ�y��X
            transform.up * v3CheckGroundOffset.y +      // �վ�y��y
            transform.forward * v3CheckGroundOffset.z,  // �վ�y��z
            checkGroundRadius,                          // �]�w�j�p
            1 << 3
            );
        // print("�y��I�쪺�Ĥ@�ӳ���:" + hits[0].name);

        if (!isGrounded && hits.Length > 0) { aud.PlayOneShot(soundGround, volumeRandom); }

        isGrounded = hits.Length > 0;

        // �Ǧ^ �I���}�C�ƶq > 0 - �u�n�I����w�ϼh����N�N��b�a�O�W
        return hits.Length > 0;
    }

    /// <summary>
    /// ���D
    /// </summary>
    private void Jump()
    {
        // print("�O�_�b�a���W:" + CheckGround());

        // &&
        // �p�G �b�a���W �åB ���U�ť��� �N���D
        if (CheckGround() && keyJump)
        {
            // ����.�K�[���O(�����󪺤W��*���D)
            rig.AddForce(transform.up * jump);

            aud.PlayOneShot(soundJump, volumeRandom);
        }
    }

    /// <summary>
    /// ��s�ʵe
    /// </summary>
    private void UpdateAnimation()
    {
        /* 
         * ��
         * �w�����G
         * ���U�e�Ϋ�� �N���L�ȳ]�� truu
         * �S������ �N���L�ȳ]�� false
         * Input
         * if
         * != �B == ����B��l(��ܱ���)
         * /**/

        ani.SetBool(animatorParWalk, keyMove);
        //�]�w�O�_�b�a�O�W �ʵe�Ѽ�
        ani.SetBool(animatorParIsGrounded, isGrounded);
        // �p�G ���U���D�� �N �]�w ���DĲ�o�Ѽ�
        // �P�_�� �u���@��ԭz(�u���@�Ӥ���) �i�H�ٲ� �j�A��
        if (keyJump) ani.SetTrigger(animatorParjump);

    }

    [Header("���۳t��"), Range(0, 50)]
    public float speedLookAt = 2;

    /// <summary>
    /// ���V�e��G���V��v���e���m
    /// </summary>
    private void LookAtForward()
    {
        // �����b�V ������� �� �j�� 0.1 �B�z ���V
        if (Mathf.Abs(MoveInput("Vertical")) > 0.1f)
        {
            // ���o�e�訤�� = �|��.���ۨ���(�e��y�� - �����y��)
            Quaternion angle = Quaternion.LookRotation(thirdPersonCamera.posForward - transform.position);
            // �����󪺨��� = �|��.����
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
        }
    }
    #endregion

    /// <summary>
    /// ��v�����O
    /// </summary>
    private ThirdPersonCamera thirdPersonCamera;

    #region �ƥ� Event
    // �S�w�ɶ��I�|���檺��k�D�{�����J�f Start ���� Console Main
    // �}�l�ƥ� Start�Q�C���}�l�ɰ���@�� - �B�z��l�ơD���o��Ƶ���
    private void Start()
    {
        // �n���o�}�����C������i�H�ϥ�����r gameObject

        // ���o���󪺤覡
        // 1.�������W��.���o����(����(��������)) ��@ ��������
        aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
        // 2.���}���C������.���o����<�x��>();
        rig = gameObject.GetComponent<Rigidbody>();
        // 3.���o����<�x��>();
        // ���O�i�H�ϥ��~�����O(�����O)�������D���}�ΫO�@ ���B�ݩʻP��k
        ani = GetComponent<Animator>();

        // ��v�����O = �z�L�����j�M����<�x��>();
        // FindObjectOfType ���n��b Update ���ϥη|�y���j�q�į�t��
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();


    }
    // ��s�ƥ� Update�Q�@������� 60 �� �D 60 FPS - Frame Per Second
    private void Update()
    {
        //CheckGround();
        Jump();
        UpdateAnimation();
        LookAtForward();
    }
    // �T�w��s�ƥ�G�T�w 0.02�����@��
    // �B�z���z�欰�D�Ҧp�Grigidbody API
    private void FixedUpdate()
    {
        Move(speed);
    }
    // ø�s�ϥܨƥ�G
    // �b Unity Editor ��ø�s�ϥܻ��U�}�o�D�o����|�۰�����
    private void OnDrawGizmos()
    {
        // 1.ø�s�C��
        // 2.ø�s�ϧ�
        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);

        // transform(�n�p�g) �P���}���b�P���h�� Transform ���� 
        Gizmos.DrawSphere
            (
            transform.position +                        // �⪫��]�w�b���⨭�W
            transform.right * v3CheckGroundOffset.x +   // �վ�y��X
            transform.up * v3CheckGroundOffset.y +      // �վ�y��y
            transform.forward * v3CheckGroundOffset.z,  // �վ�y��z
            checkGroundRadius                           // �]�w�j�p
            );
    }
    #endregion
}
