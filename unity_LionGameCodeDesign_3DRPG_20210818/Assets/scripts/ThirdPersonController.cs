using UnityEngine;          // �ޥ� Unity API (�ܮw - ��ƻP�\��)
using UnityEngine.Video;    // �ޥ� �v�� API
/* �׹��� ���O ���O�W�� : �~�����O
MonoBehaviour Unity �����O�D�n���b����W�@�w�n�~��
�~�ӫ�ɦ������O������
�b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
�`�Φ��� �Q ��� Field �B �ݩ� Property (�ܼ�) �B ��k Method �B �ƥ� Event */
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
    [Header("���ʳt��"),Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"),Range(1,500)]

    public float Speed = 10.5f;

    #region Unity �������
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
    public AudioClip sound;     // ���� mp3,ogg,wav
    public VideoClip video;     // �v�� mp4
    public Sprite sprite;       // �Ϥ� png,jpeg
    public Material material;   // ����y







    #endregion


    #endregion
    #region �ݩ� Property





    #endregion
    #region ��k Method






    #endregion
    #region �ƥ� Event






    #endregion
}
