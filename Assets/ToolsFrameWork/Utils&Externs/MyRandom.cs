using System;
using UnityEngine;

namespace _0._1FrameWork.Utils
{
    public static class MyRandom 
    {
        /// <summary>
        /// 返回一个包括最大最小的随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomNumber(int min, int max)
        {
            int rtn = 0;

            System.Random r = new System.Random();

            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            r = new System.Random(iSeed);

            rtn = r.Next(min, max + 1);//包含最大最小

            return rtn;
        }

        /// <summary>
        /// 返回0.0～1.0之间的随机数
        /// </summary>
        /// <returns></returns>
        public static double GetRandomNumber()
        {
            System.Random random = new System.Random(System.Guid.NewGuid().ToString().GetHashCode());

            return random.NextDouble();
        }
        /// <summary>
        /// 计算时分秒
        /// </summary>
        /// <param name="time">总共多少秒</param>
        /// <returns></returns>
        public static string GetTime(float time)
        {

            float m = Mathf.FloorToInt(time / 60f);
            float s = Mathf.FloorToInt(time - m * 60f);
            return (m.ToString("00") + ":" + s.ToString("00"));
        }
        /// <summary>
        /// 获取当前时间秒数
        /// </summary>
        /// <returns></returns>
        /*public static long GetCurrentTimeSecond()
    {
        return (UnbiasedTime.Instance.Now().Ticks - 621355968000000000) / 10000000;
    }*/
    }
}
