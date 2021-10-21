using UnityEngine;          // 引用 Unity API (倉庫 - 資料與功能)
using UnityEngine.Video;    // 引用 影片 API

/// <summary>
/// Sky 2021.0906
/// 第三人稱控制器
/// 移動、跳躍
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region 欄位 Field
    [Header("移動速度"), Tooltip("用來調整角色移動速度"), Range(1, 500)]
    public float speed = 10.5f;
    [Header("跳躍高度"), Tooltip("用來調整角色跳躍高度"), Range(0, 1000)]
    public int jump = 100;

    [Header("是否站在地板上"), Tooltip("用來確定角色是否站在地板上")]
    public bool isGrounded;
    [Header("檢查地板位移(三維向量)")]
    public Vector3 v3CheckGroundOffset;
    [Header("檢查地板半徑"), Range(0, 3)]
    public float checkGroundRadius = 0.1f;

    [Header("跳躍音效")]
    public AudioClip soundJump;
    [Header("落地音效")]
    public AudioClip soundGround;

    [Header("動畫參數走路開關")]
    public string animatorParWalk = "走路開關";
    public string animatorParRun = "跑步開關";
    public string animatorParHurt = "受傷觸發";
    public string animatorParDead = "死亡開關";
    public string animatorParjump = "跳躍觸發";
    public string animatorParIsGrounded = "是否在地板上";

    [Header("玩家物件")]
    public GameObject playerObject;

    [Header("元件 音效來源")]
    private AudioSource aud;
    [Header("元件 剛體")]
    private Rigidbody rig;
    [Header("動畫控制器")]
    private Animator ani;
    #endregion

    #region 屬性 Property
    // C# 6.0 存取子 可以使用 Lambda => 運算子
    // 語法 get => {程式區塊} - 單行可省略大括號
    private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
    private bool keyMove { get => MoveInput("Vertical") != 0 | MoveInput("Horizontal") != 0; }

    private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
    #endregion

    #region 方法 Method
    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="speedMove">移動速度</param>
    private void Move(float speedMove)
    {
        // 請取消 animator 屬性 Apply Root Motion ：勾選時使用動畫位移資訊
        // 剛體.加速度 = 三維向量 - 加速度用來控制剛sa體三個軸向的運動速度
        // 前方(forward) * 輸入值(Vertical) * 移動(speedMove)
        // 使用前後左右軸向運動並保持原本地心引力
        // Vector3.forward 世界座標 的 前方 (全域)
        // transform.forward 此物件 的 前方 (區域)
        rig.velocity =
            transform.forward * MoveInput("Vertical") * speedMove +
            transform.right * MoveInput("Horizontal") * speedMove +
            Vector3.up * rig.velocity.y;
    }

    /// <summary>
    /// 移動按鍵輸入
    /// </summary>
    /// <param name="axisName">要取得的軸向名稱</param>>
    /// <returns></returns>
    private float MoveInput(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    /// <summary>
    /// 檢查地板
    /// </summary>
    /// <returns>是否碰到地板</returns>
    private bool CheckGround()
    {
        // 物理.覆蓋球體(中心點．半徑．圖層)
        Collider[] hits = Physics.OverlapSphere
            (
            transform.position +                        // 把物體座標＝角色座標
            transform.right * v3CheckGroundOffset.x +   // 調整座標X
            transform.up * v3CheckGroundOffset.y +      // 調整座標y
            transform.forward * v3CheckGroundOffset.z,  // 調整座標z
            checkGroundRadius,                          // 設定大小
            1 << 3
            );
        // print("球體碰到的第一個場所:" + hits[0].name);

        if (!isGrounded && hits.Length > 0) { aud.PlayOneShot(soundGround, volumeRandom); }

        isGrounded = hits.Length > 0;

        // 傳回 碰撞陣列數量 > 0 - 只要碰到指定圖層物件就代表在地板上
        return hits.Length > 0;
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        // print("是否在地面上:" + CheckGround());

        // &&
        // 如果 在地面上 並且 按下空白鍵 就跳躍
        if (CheckGround() && keyJump)
        {
            // 剛體.添加推力(此物件的上方*跳躍)
            rig.AddForce(transform.up * jump);

            aud.PlayOneShot(soundJump, volumeRandom);
        }
    }

    /// <summary>
    /// 更新動畫
    /// </summary>
    private void UpdateAnimation()
    {
        /* 
         * ※
         * 預期結果
         * 按下前或後時 將布林值設為 truu
         * 沒有按時 將布林值設為 false
         * Input
         * if
         * != 、 == 比較運算子(選擇條件)
         * /**/

        ani.SetBool(animatorParWalk, keyMove);
        //設定是否在地板上 動畫參數
        ani.SetBool(animatorParIsGrounded, isGrounded);
        // 如果 按下跳躍鍵 就 設定 跳躍觸發參數
        // 判斷式 只有一行敘述(只有一個分號) 可以省略 大括號
        if (keyJump) ani.SetTrigger(animatorParjump);

    }

    [Header("面相速度"), Range(0, 50)]
    public float speedLookAt = 2;

    /// <summary>
    /// 面向前方：面向攝影機前方位置
    /// </summary>
    private void LookAtForward()
    {
        // 垂直軸向 取絕對值 後 大於 0.1 處理 面向
        if (Mathf.Abs(MoveInput("Vertical")) > 0.1f)
        {
            // 取得前方角度 = 四元.面相角度(前方座標 - 本身座標)
            Quaternion angle = Quaternion.LookRotation(thirdPersonCamera.posForward - transform.position);
            // 此物件的角度 = 四元.插值
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
        }
    }
    #endregion

    /// <summary>
    /// 攝影機類別
    /// </summary>
    private ThirdPersonCamera thirdPersonCamera;

    #region 事件 Event
    // 特定時間點會執行的方法．程式的入口 Start 等於 Console Main
    // 開始事件 Start﹔遊戲開始時執行一次 - 處理初始化．取得資料等等
    private void Start()
    {
        // 要取得腳本的遊戲物件可以使用關鍵字 gameObject

        // 取得元件的方式
        // 1.物件欄位名稱.取得元件(類型(元件類型)) 當作 元件類型
        aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
        // 2.此腳本遊戲物件.取得元件<泛型>();
        rig = gameObject.GetComponent<Rigidbody>();
        // 3.取得元件<泛型>();
        // 類別可以使用繼承類別(父類別)的成員．公開或保護 欄位、屬性與方法
        ani = GetComponent<Animator>();

        // 攝影機類別 = 透過類型搜尋物件<泛型>();
        // FindObjectOfType 不要放在 Update 內使用會造成大量效能負擔
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();


    }
    // 更新事件 Update﹔一秒約執行 60 次 ． 60 FPS - Frame Per Second
    private void Update()
    {
        //CheckGround();
        Jump();
        UpdateAnimation();
        LookAtForward();
    }
    // 固定更新事件：固定 0.02秒執行一次
    // 處理物理行為．例如：rigidbody API
    private void FixedUpdate()
    {
        Move(speed);
    }
    // 繪製圖示事件：
    // 在 Unity Editor 內繪製圖示輔助開發．發布後會自動隱藏
    private void OnDrawGizmos()
    {
        // 1.繪製顏色
        // 2.繪製圖形
        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);

        // transform(要小寫) 與此腳本在同階層的 Transform 元件 
        Gizmos.DrawSphere
            (
            transform.position +                        // 把物體設定在角色身上
            transform.right * v3CheckGroundOffset.x +   // 調整座標X
            transform.up * v3CheckGroundOffset.y +      // 調整座標y
            transform.forward * v3CheckGroundOffset.z,  // 調整座標z
            checkGroundRadius                           // 設定大小
            );
    }
    #endregion
}
