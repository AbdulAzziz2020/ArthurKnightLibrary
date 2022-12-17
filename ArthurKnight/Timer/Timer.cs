using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArthurKnight.Timer
{
    public abstract class Timer
    {
        private static float tickTime;

        public static void SetTime(ref int time, Text timeText)
        {
            if (tickTime < 0)
            {
                time--;
                tickTime = 1f;
                timeText.text = SetTimeText(time);
            }
            else  tickTime -= Time.deltaTime;
        }

        public static void SetTime(ref int time, TMP_Text timeText)
        {
            if (tickTime < 0)
            {
                time--;
                tickTime = 1f;
                timeText.text = SetTimeText(time);
            }
            else  tickTime -= Time.deltaTime;
        }
        
        public static void SetTime(ref int time, float timeAdjustment, float timeMin, float timeMax, TMP_Text timeText)
        {
            if (tickTime < 0)
            {
                time--;
                tickTime = Mathf.Clamp(timeAdjustment, timeMin, timeMax);
                timeText.text = SetTimeText(time);
            }
            else  tickTime -= Time.deltaTime;
        }

        public static void SetTime(ref int time, float timeAdjustment, float timeMin, float timeMax, Text timeText)
        {
            if (tickTime < 0)
            {
                time--;
                tickTime = Mathf.Clamp(timeAdjustment, timeMin, timeMax);
                timeText.text = SetTimeText(time);
            }
            else tickTime -= Time.deltaTime;
        }
        
        public static string SetTimeText(int time)
        {
            int _minute = Mathf.FloorToInt(time / 60);
            int _second = Mathf.FloorToInt(time % 60);
            return _minute.ToString("00") + " : " + _second.ToString("00");
        }
    }
}