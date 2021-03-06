using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 學習插值 Lerp
/// </summary>
public class LearnLerp : MonoBehaviour
{
    public float a = 0, b = 100;
    public float c = 0, d = 100;
    public Color colorA = Color.blue, colorB = Color.yellow;
    public Vector3 v3A = Vector3.zero, v3B = Vector3.one * 100;

    private void Start()
    {
        print("a b 兩點 差值 0.5 : " + Mathf.Lerp(a, b, 0.5f));
        print("a b 兩點 差值 0.1 : " + Mathf.Lerp(a, b, 0.1f));
    }
    private void Update()
    {
        d = Mathf.Lerp(c, d, 0.5f);
        colorB = Color.Lerp(colorA, colorB, 0.9f);
        v3B = Vector3.Lerp(v3A, v3B, 0.9f);
    }
}
