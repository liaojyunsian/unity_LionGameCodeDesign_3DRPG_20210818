using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIStaticPractice : MonoBehaviour
{
    private void Start()
    {
        #region 取得 靜態屬性
        // 所有攝影機數量
        /*
        int count = Camera.allCamerasCount;
        print("所有攝影機數量：" + count);
        /**/
        print("所有攝影機數量：" + Camera.allCamerasCount);
        // 2D 的重力大小
        /*
        Vector2 gravity = Physics2D.gravity;
        print("2D的重力大小：" + gravity);
        */
        print("2D的重力大小：" + Physics2D.gravity);
        // 圓周率
        /*
        float p = Mathf.PI;
        print("圓周率 = " + p.ToString("0.000"));
        */
        print("圓周率 = "+Mathf.PI);
        #endregion

        #region 設定 靜態屬性
        // 2D 的重力大小設定為 Y -20
        Physics2D.gravity = new Vector2(0, -20);
        print("2D的重力大小：" + Physics2D.gravity);
        // 時間大小設定為 0.5 (慢動作)
        Time.timeScale = 0.5f;
        float t = Time.timeScale;
        print("慢動作時間：" + t.ToString("0.000"));
        #endregion

        #region 呼叫 靜態方法
        // 對 9.999 去小數點
        Debug.Log(Mathf.Round(9.999F));
        //取得兩點的距離 new Vector3(1, 1, 1) new Vector3(22, 22, 22)
        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b = new Vector3(22, 22, 22);
        float distance = Vector3.Distance(a, b);
        print("兩點的距離：" + distance);
        //開啟連結 https://unity.com/
        Application.OpenURL("https://unity.com/");
        #endregion
    }
    private void Update()
    {
        #region 取得 靜態屬性
        // 是否輸入任意鍵
        print("是否輸入任意鍵" + Input.anyKey);
        // 遊戲經過時間
        print("遊戲經過時間" + Time.time);
        #endregion

        #region 呼叫 靜態方法
        // 是否按下按鍵 (指定為空白鍵)
        print("按下空白鍵" + Input.GetKey(KeyCode.Space));
        #endregion
    }
}
