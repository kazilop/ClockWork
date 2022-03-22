using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;



public class ClockManager : MonoBehaviour
{

    public static ClockManager instance;

    const string API_URL = "https://www.timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow";
    const string API_URL2 = "http://worldclockapi.com/api/json/est/now";

    [HideInInspector]
    public bool isTimeLoaded = false;

    public TMP_Text textClock;

    public int hour;
    public int minute;
    public int second;

    [System.Serializable]
    public class MyDate
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int seconds;
        public int milliSeconds;
        public DateTime dateTime;
        public string date;
        public string time;
        public string timeZone;
        public string dayOfWeek;
        public bool dstActive;
    }

    [System.Serializable]
    public class MyDateSecond
    {
        public string id;
        public string currentDateTime;
        public string utcOffset;
        public bool isDayLightSavingsTime;
        public string dayOfTheWeek;
        public string timeZoneName;
        public long currentFileTime;
        public string ordinalDate;
        public object serviceResponse;
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GetMyTime();
    }



    public void GetMyTime()
    {
        StartCoroutine(GetRealTime());
    }

    private IEnumerator GetRealTime()
    {

        using (UnityWebRequest myWebRequest = UnityWebRequest.Get(API_URL))
        {
            yield return myWebRequest.SendWebRequest();

            if (myWebRequest != null)
            {
                string json = myWebRequest.downloadHandler.text;
                MyDate myDate = JsonUtility.FromJson<MyDate>(json);
                //MyDateSecond mysecond = JsonUtility.FromJson<MyDateSecond>(json);
                
                Debug.Log(myDate.hour);

                textClock.text = myDate.hour.ToString() + " : " + myDate.minute.ToString();
                hour = myDate.hour;
                minute = myDate.minute;
                second = myDate.seconds;
            }
            else
            {
                textClock.text = "Server not enabled";
            }
        }

    }
}



