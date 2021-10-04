using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 非靜態練習
/// </summary>
public class APINotStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer spriteRenderer;

    [Header("圖片一")]
    public SpriteRenderer spriteRenderer_1;
    public Transform transform;
    [Header("圖片二")]
    public SpriteRenderer spriteRenderer_2;
    public Rigidbody2D rigidbody2D;

    private void Start()
    {
        #region 非靜態屬性
        // 取得 Get
        //攝影機深度 (Depth)
        print("攝影機的深度：" + cam.depth);
        //圖片的顏色
        print("圖片的顏色：" + spriteRenderer.color);

        // 設定 set
        // 攝影機的背景顏色 (隨機顏色)
        cam.backgroundColor = Random.ColorHSV();
        //圖片的上下翻面
        spriteRenderer.flipY = true;
        #endregion
    }

    private void Update()
    {
        //讓第一張圖片物件旋轉
        transform.Rotate(0.0f, 9.0f, 0.0f);
        //讓第二張圖片可以往上飛
        rigidbody2D.AddForce(new Vector2(0, 30));
    }


}
