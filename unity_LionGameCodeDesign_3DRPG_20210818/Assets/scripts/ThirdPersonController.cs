using UnityEngine;          // 引用 Unity API (倉庫 - 資料與功能)
using UnityEngine.Video;    // 引用 影片 API
/* 修飾詞 類別 類別名稱 : 繼承類別
MonoBehaviour Unity 基底類別．要掛在物件上一定要繼承
繼承後享有該類別的成員
在類別以及成員上方添加三條斜線會添加摘要
常用成員 ﹔ 欄位 Field 、 屬性 Property (變數) 、 方法 Method 、 事件 Event */
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
    [Header("移動速度"),Tooltip("用來調整角色移動速度"),Range(1,500)]

    public float Speed = 10.5f;

    #region Unity 資料類型
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
    public AudioClip sound;     // 音效 mp3,ogg,wav
    public VideoClip video;     // 影片 mp4
    public Sprite sprite;       // 圖片 png,jpeg
    public Material material;   // 材質球







    #endregion


    #endregion
    #region 屬性 Property





    #endregion
    #region 方法 Method






    #endregion
    #region 事件 Event






    #endregion
}
