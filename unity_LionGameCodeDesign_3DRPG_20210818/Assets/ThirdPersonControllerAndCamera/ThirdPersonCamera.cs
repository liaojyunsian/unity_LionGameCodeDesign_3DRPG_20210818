using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第三人稱攝影機系統
/// 追蹤指定目標
/// 並且可以左右、上下旋轉(限制)
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    #region 欄位
    [Header("目標物件")]
    public Transform target;
    [Header("追蹤速度"), Range(0, 100)]
    public float speedTrack = 1.5f;
    [Header("旋轉左右速度"), Range(0, 100)]
    public float speedTurnHorizontal = 5;
    [Header("旋轉上下速度"), Range(0, 100)]
    public float speedTurnVertical = 5;
    [Header("X 軸上下旋轉值限制 : 最小與最大值")]
    public Vector2 limitAngleX = new Vector2(-0.2f, 0.2f);
    [Header("攝影機在角色前方上下旋轉限制 : 最小與最大值")]
    public Vector2 limitAngleFromTarget = new Vector2(-0.2f, 0);
    /// <summary>
    /// 攝影機前方座標
    /// </summary>
    private Vector3 _posForward;
    /// <summary>
    /// 前方的長度
    /// </summary>
    private float lengthForward = 3;
    #endregion

    #region 屬性
    /// <summary>
    /// 取得滑鼠水平座標
    /// </summary>
    private float inputMouseX { get => Input.GetAxis("Mouse X"); }
    /// <summary>
    /// 取的滑鼠垂直座標
    /// </summary>
    private float inpetMouseY { get => Input.GetAxis("Mouse Y"); }

    /// <summary>
    /// 攝影機前方座標
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

    #region 事件
    private void Update()
    {
        TurnCamera();
        LimitAngleX();
        FreezeAngleZ();
    }
    /// <summary>
    /// 在Update 後執行．處理攝影機追蹤行為
    /// </summary>
    private void LateUpdate()
    {
        TrackTarget();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // 前方座標 = 物件座標 + 此物件前方 * 長度
        _posForward = transform.position + transform.forward * lengthForward;
        // 前方座標.y = 目標.座標.y(讓前方座標的高度與目標相同)
        _posForward.y = target.position.y;
        Gizmos.DrawSphere(_posForward, 0.15f);

    }
    #endregion

    #region 方法
    /// <summary>
    /// 追蹤目標
    /// </summary>
    private void TrackTarget()
    {
        Vector3 posTarget = target.position;// 取得 目標 座標
        Vector3 posCamera = transform.position;//取得 攝影機 座標

        //攝影機座標 = 插值 (速度 * 一幀的時間)
        posCamera = Vector3.Lerp(posCamera, posTarget, speedTrack * Time.deltaTime);//攝影機座標 = 插值
        transform.position = posCamera;//此物件的座標 = 攝影機座標
    }
    /// <summary>
    /// 旋轉攝影機
    /// </summary>
    private void TurnCamera()
    {
        transform.Rotate(
            inpetMouseY * Time.deltaTime * speedTurnVertical,
            inputMouseX * Time.deltaTime * speedTurnHorizontal,
            0);
    }
    /// <summary>
    /// 限制角度 X 軸 與 在目標前方的 Z 軸
    /// </summary>
    private void LimitAngleX()
    {
        //print("攝影機的角度值 : " + transform.rotation);//0.3 0.3
        Quaternion angle = transform.rotation;// 取得四位元角度
        angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);// 夾住角度X軸
        angle.z = Mathf.Clamp(angle.z, limitAngleFromTarget.x, limitAngleFromTarget.y);
        transform.rotation = angle;// 更新物件角度
    }
    /// <summary>
    /// 凍結角度 Z 軸為零
    /// </summary>
    private void FreezeAngleZ()
    {
        Vector3 angle = transform.eulerAngles;// 取得三維角度
        angle.z = 0;// 凍結 Z 軸維零
        transform.eulerAngles = angle;// 更新物件角度
    }
    #endregion

}
