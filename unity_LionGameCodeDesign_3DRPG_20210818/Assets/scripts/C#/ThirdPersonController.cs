using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practice
{
    /* 修飾詞 類別 類別名稱 : 繼承類別
    // MonoBehaviour Unity 基底類別．要掛在物件上一定要繼承
    //繼承後享有該類別的成員
    //在類別以及成員上方添加三條斜線會添加摘要
    //常用成員 ﹔ 欄位 Field 、 屬性 Property (變數) 、 方法 Method 、 事件 Event 
    /**/
    /// <summary>
    /// Sky 2021.0906
    /// 第三人稱控制器
    /// 移動、跳躍
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 欄位 Field
        // 儲存遊戲資料．例如﹔移動速度、跳躍高度...
        // 常用四大類型﹔整數 int 、浮點數 float、字串 strint、布林值 bool
        // 欄位語法﹔修飾詞 資料類型 欄位名稱 (指定 預設值) 結尾
        // 修飾詞﹔
        // 1. 公開 public  -允許其他類別存取 - 顯示在屬性面板 - 需要調整的資料設定公開
        // 2. 私人 private -禁止其他類別存取 - 隱藏在屬性面板 - 預設值
        // ※ Unity 以屬性面板資料為主
        // ※ 恢復程式預設值請按 ... > Reset
        // 欄位屬性﹔輔助欄位資料
        // 欄位屬性語法﹔[屬性名稱(屬性值)]
        // Header 標題
        // Tooltip 提示﹔滑鼠停留在欄位名稱上會顯示彈出視窗
        // Range 範圍﹔可使用在數值類型資料上，例如﹔ int, float

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



        #region Unity 資料類型
        /* 練習 Unity 資料類型
        //顏色 Color
        public Color Color;
        public Color white = Color.white;                       // 內建顏色
        public Color yellow = Color.yellow;
        public Color Color1 = new Color(0.5f, 0.5f, 0);         // 自訂顏色R,G,B, 
        public Color Color2 = new Color(0, 0.5f, 0.5f, 0.5f);   // 自訂顏色R,G,B,A,

        // 座標 Vector 2 - 4
        public Vector2 v2;
        public Vector2 v2Right = Vector2.right;
        public Vector2 v2Up = Vector2.up;
        public Vector2 v2One = Vector2.one;
        public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
        public Vector3 v3 = new Vector3(1, 2, 3);
        public Vector4 v4 = new Vector4(1, 2, 3, 4);

        // 按鍵 列舉資料 enum
        public KeyCode Key;
        public KeyCode move = KeyCode.W;
        public KeyCode jump = KeyCode.Space;

        // 遊戲資料類型
        // 存放 Project 專案內的資料
        public AudioClip sound;     // 音效 mp3,ogg,wav
        public VideoClip video;     // 影片 mp4
        public Sprite sprite;       // 圖片 png,jpeg
        public Material material;   // 材質球

        [Header("元件")]
        // 元件 Component﹔屬性面板上可折疊的
        public Transform tra;
        public Animation aniOld;
        public Animator aniNew;
        public Light lig;
        public Camera cam;

        // 綠色蚯蚓
        // 1.建議不要使用此名稱 
        // 2.使用過時的 API
        /**/
        #endregion

        #endregion

        #region 屬性 Property
        /*屬性練習
        // 屬性不會顯示在面板上
        // 儲存資料，與欄位相同
        // 差異在於﹔可以設定存取權限 Get Set
        // 屬性語法﹔修飾詞 資料類型 屬性名稱{ 取; 存; }
        public int readAndWrite { get; set; }
        // 唯獨屬性﹔只能取得 get
        public int read { get; }
        // 唯獨屬性﹔透過 get 設定預設值．關鍵字 return 傳回值
        public int readValue
        {
            get
            {
                return 77;
            }

        }
        // 唯寫屬性﹔禁止．必須要有get
        // public int write { set; }
        // value 指的是指定的值
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
        // C# 6.0 存取子 可以使用 Lambda => 運算子
        // 語法 get => {程式區塊} - 單行可省略大括號
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private bool keyMove { get => MoveInput("Vertical") != 0 | MoveInput("Horizontal") != 0; }

        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
        #endregion

        #region 方法 Method
        #region 自訂方法
        /* 自訂方法
        // 定義與實作較複雜程式的區塊．功能
        // 方法語法﹔修飾詞 傳回資料類型 方法名稱 (參數1, ... 參數N) {程式區塊}
        // 常用傳回類型﹔無傳回 void - 此方法沒有傳回資料
        // 格式化﹔ Ctrl + K D //排版
        // 自訂方法﹔
        // 名稱顏色為淡黃色 - 沒有被呼叫
        // 名稱顏色為淡黃色 - 有被呼叫
        private void Test()
        {
            print("我是自訂方法~");
        }

        private int ReturnJump()
        {
            return 999;
        }
        /**/
        #endregion

        #region 自訂 參數 方法
        // 參數語法﹔資料類型 參數名稱
        // 有預設值的參數可以不輸入引數．選填式參數
        // ※ 選填式只能放在()右邊
        /* 選填式參數 範例
        private void Skill(int damage, string effect = "灰塵特效", string sound = "嘎嘎嘎")
        {
            print("參數版本 - 傷害值﹔" + damage);
            print("參數版本 - 技能特效﹔" + effect);
            print("參數版本 - 音效﹔" + sound);
        }
        /**/
        /*對照組﹔不使用參數
        // 降低維護與擴充性
        private void Skill_100()
        {
            print("傷害值 ﹔ " + 100);
            print("技能特效");
        }

        private void Skill_150()
        {
            print("傷害值 ﹔ " + 150);
            print("技能特效");
        }

        private void Skill_200()
        {
            print("傷害值 ﹔ " + 200);
            print("技能特效");
        }
        /**/
        #endregion

        #region BMI 算法 與 摘要使用範例
        /*
        // 不是必須要，但是很重要
        // BMI = 體重 / 身高 * 身高 (公尺)
        /// <summary>
        /// 計算BMI的方法
        /// </summary>
        /// <param name="weight">體重，單位為公斤</param>
        /// <param name="height">身高，單位為公分</param>
        /// <param name="name">名字</param>
        /// <returns></returns>
        /** BMI程式
        private float BMI(float weight, float height, string name = "測試")
        {
            print(name + " 的 BMI");

            return weight / (height * height);
        }
        /**/
        #endregion



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
            rig.velocity =
                Vector3.forward * MoveInput("Vertical") * speedMove +
                Vector3.right * MoveInput("Horizontal") * speedMove +
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
            print("是否在地面上:" + CheckGround());

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
        #endregion

        #region 事件 Event
        // 特定時間點會執行的方法．程式的入口 Start 等於 Console Main
        // 開始事件 Start﹔遊戲開始時執行一次 - 處理初始化．取得資料等等
        private void Start()
        {
            #region 輸出 方法
            /** 輸出 方法
            print("哈嘍．沃德 ~ ");
            Debug.Log("一般訊息");
            Debug.LogWarning("警告訊息");
            Debug.LogError("錯誤訊息");
            */
            #endregion

            #region 屬性練習
            /**屬性練習
            print("欄位資料 - 移動速度 ﹔ " + Speed);
            print("屬性資料 - 讀寫屬性 ﹔ " + readAndWrite);
            Speed = 20.5f;
            readAndWrite = 90;
            print("修改後的資料");
            print("欄位資料 - 移動速度 ﹔ " + Speed);
            print("屬性資料 - 讀寫屬性 ﹔ " + readAndWrite);

            // 唯獨屬性
            // read = 7;    //唯獨屬性不能設定 set
            print("唯獨屬性 ﹔ " + read);
            print("唯獨屬性 ﹔ " + readValue);

            //屬性存取練習
            print("HP ﹔ " + hp);
            hp = 100;
            print("HP ﹔ " + hp);
            /**/
            #endregion

            #region 呼叫方法語法用法
            /**
            // 呼叫自訂方法語法﹔方法名稱()﹔
            Test();
            Test();
            // 呼叫有傳回值的方法
            // 1.區域變數指定傳回值 - 區域變數僅能在此結構 (大括號) 內存取
            int j = ReturnJump();
            print("跳躍值 ﹔ " + j);
            // 2.將傳回方法當成值使用
            print("跳躍值．當值使用 ﹔ " + (ReturnJump() + 1));
            
            //對照組 不使用參數
            Skill_200();
            Skill_150();
            Skill_100();
            
            // 呼叫有參數的方法．必須輸入對應的引數
            Skill(100);
            Skill(999, "爆炸特效");
            // 需求﹔傷害值 500．特效預設．音效換成 咻咻咻
            // 有多個選填式參數時可使用指名參數語法﹔參數名稱: 值
            Skill(500, sound: "咻咻咻");

            print(BMI(61, 1.68f, "000"));
            /**/
            #endregion

            // 要取得腳本的遊戲物件可以使用關鍵字 gameObject

            // 取得元件的方式
            // 1.物件欄位名稱.取得元件(類型(元件類型)) 當作 元件類型
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            // 2.此腳本遊戲物件.取得元件<泛型>();
            rig = gameObject.GetComponent<Rigidbody>();
            // 3.取得元件<泛型>();
            // 類別可以使用繼承類別(父類別)的成員．公開或保護 欄位、屬性與方法
            ani = GetComponent<Animator>();

        }


        // 更新事件 Update﹔一秒約執行 60 次 ． 60 FPS - Frame Per Second
        private void Update()
        {
            #region 測試
            /** YOYOYO~
            print("YOYOYO ~ ");
            /**/
            #endregion

            //CheckGround();
            Jump();
            UpdateAnimation();

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

}