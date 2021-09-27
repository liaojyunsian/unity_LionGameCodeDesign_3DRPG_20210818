using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 認識API靜態 Static
/// </summary>
public class APIStatic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 靜態屬性
        // 不需要實體物件
        // 不需要取得實體物件
        // 取得 Get
        // 語法：
        // 類別名稱.靜態屬性
        float r = Random.value;
        print("取得靜態屬性．隨機值：" + r);

        // 設定 Set
        // 語法：
        // 類別名稱.靜態屬性 指定 值;
        Cursor.visible = false;
        #endregion

        #region 靜態方法
        // 呼叫．參數、傳回
        // 簽章：參數、傳回
        // 語法：
        // 類別名稱.靜態方法(對應引數)
        float range = Random.Range(10.5f, 20.9f);
        print("隨機範圍 10.5 ~ 20.9：" + range);

        // ※ API 說明很重要：使用整數時不包含最大值
        int rangeInt = Random.Range(1, 2);
        print("隨機範圍 1 ~ 2：" + rangeInt);
        #endregion
    }

    private void Update()
    {
        #region 靜態屬性
        // 取得 Get
        // 語法：
        // 類別名稱.靜態屬性
        print("經過多久：" + Time.timeSinceLevelLoad);// time 時間
        #endregion

        #region 靜態方法
        float h = Input.GetAxis("Horizontal");
        print("水平值：" + h);
        #endregion
    }
}
