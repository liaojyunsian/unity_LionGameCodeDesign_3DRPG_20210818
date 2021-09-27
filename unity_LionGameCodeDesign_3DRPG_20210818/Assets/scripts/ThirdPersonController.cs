using UnityEngine;          // �ޥ� Unity API (�ܮw - ��ƻP�\��)
using UnityEngine.Video;    // �ޥ� �v�� API
/* �׹��� ���O ���O�W�� : �~�����O
// MonoBehaviour Unity �����O�D�n���b����W�@�w�n�~��
//�~�ӫ�ɦ������O������
//�b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
//�`�Φ��� �Q ��� Field �B �ݩ� Property (�ܼ�) �B ��k Method �B �ƥ� Event 
/**/
/// <summary>
/// Sky 2021.0906
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region ��� Field
    // �x�s�C����ơD�Ҧp�Q���ʳt�סB���D����...
    // �`�Υ|�j�����Q��� int �B�B�I�� float�B�r�� strint�B���L�� bool
    // ���y�k�Q�׹��� ������� ���W�� (���w �w�]��) ����
    // �׹����Q
    // 1. ���} public  -���\��L���O�s�� - ��ܦb�ݩʭ��O - �ݭn�վ㪺��Ƴ]�w���}
    // 2. �p�H private -�T���L���O�s�� - ���æb�ݩʭ��O - �w�]��
    // �� Unity �H�ݩʭ��O��Ƭ��D
    // �� ��_�{���w�]�ȽЫ� ... > Reset
    // ����ݩʡQ���U�����
    // ����ݩʻy�k�Q[�ݩʦW��(�ݩʭ�)]
    // Header ���D
    // Tooltip ���ܡQ�ƹ����d�b���W�٤W�|��ܼu�X����
    // Range �d��Q�i�ϥΦb�ƭ�������ƤW�A�Ҧp�Q int, float

    [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(1, 500)]
    public float speed = 10.5f;
    [Header("���D����"), Tooltip("�Ψӽվ㨤����D����"), Range(0, 1000)]
    public int jump = 100;

    [Header("�O�_���b�a�O�W"), Tooltip("�ΨӽT�w����O�_���b�a�O�W")]
    public bool isGrounded;
    [Header("�ˬd�a�O�첾(�T���V�q)")]
    public Vector3 v3CheckGroundOffset;
    [Header("�ˬd�a�O�b�|"), Range(0, 3)]
    public float checkGroundRadius = 0.2f;

    [Header("���D����")]
    public AudioClip soundJump;
    [Header("���a����")]
    public AudioClip soundGround;

    [Header("�ʵe�Ѽƨ����}��")]
    public string animatorParWalk = "�����}��";
    public string animatorParRun = "�]�B�}��";
    public string animatorParHurt = "����Ĳ�o";
    public string animatorParDead = "���`�}��";

    [Header("���� ���Ĩӷ�")]
    private AudioSource aud;
    [Header("���� ����")]
    private Rigidbody rig;
    [Header("�ʵe���")]
    private Animator ani;



    #region Unity �������
    /** �m�� Unity �������
    //�C�� Color
    public Color Color;
    public Color white = Color.white;                       // �����C��
    public Color yellow = Color.yellow;
    public Color Color1 = new Color(0.5f, 0.5f, 0);         // �ۭq�C��R,G,B, 
    public Color Color2 = new Color(0, 0.5f, 0.5f, 0.5f);   // �ۭq�C��R,G,B,A,

    // �y�� Vector 2 - 4
    public Vector2 v2;
    public Vector2 v2Right = Vector2.right;
    public Vector2 v2Up = Vector2.up;
    public Vector2 v2One = Vector2.one;
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector4 v4 = new Vector4(1, 2, 3, 4);

    // ���� �C�|��� enum
    public KeyCode Key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    // �C���������
    // �s�� Project �M�פ������
    public AudioClip sound;     // ���� mp3,ogg,wav
    public VideoClip video;     // �v�� mp4
    public Sprite sprite;       // �Ϥ� png,jpeg
    public Material material;   // ����y

    [Header("����")]
    // ���� Component�Q�ݩʭ��O�W�i���|��
    public Transform tra;
    public Animation aniOld;
    public Animator aniNew;
    public Light lig;
    public Camera cam;

    // ���L�C
    // 1.��ĳ���n�ϥΦ��W�� 
    // 2.�ϥιL�ɪ� API
    /**/
    #endregion

    #endregion

    #region �ݩ� Property
    /**�ݩʽm��
    // �ݩʤ��|��ܦb���O�W
    // �x�s��ơA�P���ۦP
    // �t���b��Q�i�H�]�w�s���v�� Get Set
    // �ݩʻy�k�Q�׹��� ������� �ݩʦW��{ ��; �s; }
    public int readAndWrite { get; set; }
    // �߿W�ݩʡQ�u����o get
    public int read { get; }
    // �߿W�ݩʡQ�z�L get �]�w�w�]�ȡD����r return �Ǧ^��
    public int readValue
    {
        get
        {
            return 77;
        }

    }
    // �߼g�ݩʡQ�T��D�����n��get
    // public int write { set; }
    // value �����O���w����
    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    /**/

    public KeyCode keyJump { get; }


    #endregion

    #region ��k Method
    #region �ۭq��k
    /** �ۭq��k
    // �w�q�P��@�������{�����϶��D�\��
    // ��k�y�k�Q�׹��� �Ǧ^������� ��k�W�� (�Ѽ�1, ... �Ѽ�N) {�{���϶�}
    // �`�ζǦ^�����Q�L�Ǧ^ void - ����k�S���Ǧ^���
    // �榡�ơQ Ctrl + K D //�ƪ�
    // �ۭq��k�Q
    // �W���C�⬰�H���� - �S���Q�I�s
    // �W���C�⬰�H���� - ���Q�I�s
    private void Test()
    {
        print("�ڬO�ۭq��k~");
    }

    private int ReturnJump()
    {
        return 999;
    }
    /**/
    #endregion

    #region �ۭq �Ѽ� ��k
    // �Ѽƻy�k�Q������� �ѼƦW��
    // ���w�]�Ȫ��Ѽƥi�H����J�޼ơD��񦡰Ѽ�
    // �� ��񦡥u���b()�k��
    /** ��񦡰Ѽ� �d��
    private void Skill(int damage, string effect = "�ǹЯS��", string sound = "�ǹǹ�")
    {
        print("�Ѽƪ��� - �ˮ`�ȡQ" + damage);
        print("�Ѽƪ��� - �ޯ�S�ġQ" + effect);
        print("�Ѽƪ��� - ���ġQ" + sound);
    }
    /**/
    /**��ӲաQ���ϥΰѼ�
    // ���C���@�P�X�R��
    private void Skill_100()
    {
        print("�ˮ`�� �Q " + 100);
        print("�ޯ�S��");
    }

    private void Skill_150()
    {
        print("�ˮ`�� �Q " + 150);
        print("�ޯ�S��");
    }

    private void Skill_200()
    {
        print("�ˮ`�� �Q " + 200);
        print("�ޯ�S��");
    }
    /**/
    #endregion

    #region BMI ��k �P �K�n�ϥνd��
    // ���O�����n�A���O�ܭ��n
    // BMI = �魫 / ���� * ���� (����)
    /// <summary>
    /// �p��BMI����k
    /// </summary>
    /// <param name="weight">�魫�A��쬰����</param>
    /// <param name="height">�����A��쬰����</param>
    /// <param name="name">�W�r</param>
    /// <returns></returns>
    /** BMI�{��
    private float BMI(float weight, float height, string name = "����")
    {
        print(name + " �� BMI");

        return weight / (height * height);
    }
    /**/
    #endregion

    #region �����k�w�q
    private void Move(float speedMove)
    {

    }

    private float MoveInput()
    {
        return 0f;
    }

    private bool CheckGround()
    {
        return false;
    }

    private void Jump()
    {

    }

    private void UpdateAnimation()
    {

    }






    #endregion

    #endregion

    public GameObject playerObject;

    #region �ƥ� Event
    // �S�w�ɶ��I�|���檺��k�D�{�����J�f Start ���� Console Main
    // �}�l�ƥ� Start�Q�C���}�l�ɰ���@�� - �B�z��l�ơD���o��Ƶ���
    private void Start()
    {
        #region ��X ��k
        /** ��X ��k
        print("���ơD�U�w ~ ");
        Debug.Log("�@��T��");
        Debug.LogWarning("ĵ�i�T��");
        Debug.LogError("���~�T��");
        */
        #endregion

        #region �ݩʽm��
        /**�ݩʽm��
        print("����� - ���ʳt�� �Q " + Speed);
        print("�ݩʸ�� - Ū�g�ݩ� �Q " + readAndWrite);
        Speed = 20.5f;
        readAndWrite = 90;
        print("�ק�᪺���");
        print("����� - ���ʳt�� �Q " + Speed);
        print("�ݩʸ�� - Ū�g�ݩ� �Q " + readAndWrite);

        // �߿W�ݩ�
        // read = 7;    //�߿W�ݩʤ���]�w set
        print("�߿W�ݩ� �Q " + read);
        print("�߿W�ݩ� �Q " + readValue);

        //�ݩʦs���m��
        print("HP �Q " + hp);
        hp = 100;
        print("HP �Q " + hp);
        /**/
        #endregion

        #region �I�s��k�y�k�Ϊk
        /**
        // �I�s�ۭq��k�y�k�Q��k�W��()�Q
        Test();
        Test();
        // �I�s���Ǧ^�Ȫ���k
        // 1.�ϰ��ܼƫ��w�Ǧ^�� - �ϰ��ܼƶȯ�b�����c (�j�A��) ���s��
        int j = ReturnJump();
        print("���D�� �Q " + j);
        // 2.�N�Ǧ^��k���Ȩϥ�
        print("���D�ȡD��Ȩϥ� �Q " + (ReturnJump() + 1));
        
        //��Ӳ� ���ϥΰѼ�
        Skill_200();
        Skill_150();
        Skill_100();
        
        // �I�s���Ѽƪ���k�D������J�������޼�
        Skill(100);
        Skill(999, "�z���S��");
        // �ݨD�Q�ˮ`�� 500�D�S�Ĺw�]�D���Ĵ��� ������
        // ���h�ӿ�񦡰ѼƮɥi�ϥΫ��W�Ѽƻy�k�Q�ѼƦW��: ��
        Skill(500, sound: "������");

        print(BMI(61, 1.68f, "000"));
        /**/
        #endregion

        // �n���o�}�����C������i�H�ϥ�����r gameObject

        // ���o���󪺤覡
        // 1.�������W��.���o����(����(��������)) ��@ ��������
        aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
        // 2.���}���C������.���o����<�x��>();
        rig = gameObject.GetComponent<Rigidbody>();
        // 3.���o����<�x��>();
        // ���O�i�H�ϥ��~�����O(�����O)�������D���}�ΫO�@ ���B�ݩʻP��k
        ani = GetComponent<Animator>();
    }


    // ��s�ƥ� Update�Q�@������� 60 �� �D 60 FPS - Frame Per Second
    private void Update()
    {
        #region ����
        /** YOYOYO~
        print("YOYOYO ~ ");
        /**/
        #endregion




    }


    #endregion

}
