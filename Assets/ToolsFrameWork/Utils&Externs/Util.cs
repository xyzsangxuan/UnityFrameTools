using UnityEngine;
using System.Text.RegularExpressions;
using System;

public static class Util 
{
    public static bool FloatEqual(float a, float b)
    {
        //绝对值
        return Mathf.Abs(a - b) < 0.0001f;
    }
    //2.5D游戏的距离判断方式
    public static float Distance2_5D(Vector3 a, Vector3 b)
    {

        //判断到达目标点的距离
        var dis = Vector3.Distance(new Vector3(a.x, 0, a.z), new Vector3(b.x, 0, b.z));

        return dis;
    }
    //2D游戏的距离判断方式
    public static float Distance2D(Vector3 a, Vector3 b)
    {

        //判断到达目标点的距离
        var dis = Vector3.Distance(new Vector3(a.x, a.y, 0), new Vector3(b.x, b.y, 0));

        return dis;
    }


    public static void SafeCall<T>(Action<T> callback, T arg)
    {
        if (callback != null) { callback(arg); }
    }

    public static void SafeCall(Action callback)
    {
        if (callback != null) { callback(); }
    }
    /// <summary>
    /// 字符串只保留字母和数字
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetNumberAlpha(string source)
    {
        string pattern = "[A-Za-z0-9]";
        string strRet = "";
        MatchCollection results = Regex.Matches(source, pattern);
        foreach (var v in results)
        {
            strRet += v.ToString();
        }
        return strRet;
    }
}
