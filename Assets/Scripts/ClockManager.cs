using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;


public class ClockManager : MonoBehaviour
{

    public static ClockManager instance;
    public WakeUp wakeManager;

    const string API_URL = "https://www.timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow";
    const string API_URL2 = "http://worldtimeapi.org/api/timezone/Europe/Moscow";

    [HideInInspector]
    public bool isTimeLoaded = false;

    public bool isUpdate = false;

    public TMP_Text textClock;

    public int hour;
    public int minute;
    public int second;

    private int hour2;
    private int minute2;
    private int second2;

    [SerializeField]
    private int wakeupHour;
    [SerializeField]
    private int wakeupMinute;

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
        public string abbreviation;
        public string client_ip;
        public string datetime;
        public int day_of_week;
        public int day_of_year;
        public bool dst;
        public object dst_from;
        public int dst_offset;
        public object dst_until;
        public int raw_offset;
        public string timezone;
        public int unixtime;
        public string utc_datetime;
        public string utc_offset;
        public int week_number;
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

    private void FixedUpdate()
    {
        textClock.text = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
    }

    public void GetMyTime()
    {
        StartCoroutine(GetRealTime());
        StartCoroutine(GetWorldTime());
    }

    private IEnumerator GetWorldTime()
    {
        using (UnityWebRequest myWebRequest = UnityWebRequest.Get(API_URL2))
        {
            yield return myWebRequest.SendWebRequest();

            if (myWebRequest != null)
            {
                string json = myWebRequest.downloadHandler.text;
                MyDateSecond myDateSecond = JsonUtility.FromJson<MyDateSecond>(json);

                string timeStr = myDateSecond.datetime;

                hour2 = int.Parse(myDateSecond.datetime.Substring(11,2));
                minute2 = int.Parse(myDateSecond.datetime.Substring(14, 2));
                second2 = int.Parse(myDateSecond.datetime.Substring(17, 2));
            }
            else
            {
                textClock.text = "Server not enabled";
            }
        }

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

                hour = myDate.hour;
                minute = myDate.minute;
                second = myDate.seconds;
            }
            else
            {
                textClock.text = "Server not enabled";
            }
        }
           
        StartCoroutine(ClockTicking());
        StartCoroutine(RequestTimer());
    }

    private IEnumerator RequestTimer()
    {
        yield return new WaitForSeconds(3600f);
        StopAllCoroutines();
        GetMyTime();
        if(hour != hour2 || minute != minute2)
        {
            StopAllCoroutines();
            textClock.text = "Time not Recognize";
            GetMyTime();
        }
    }

    private IEnumerator ClockTicking()
    {
        if (isUpdate != true)
        {
            yield return new WaitForSeconds(1f);

            if (isUpdate == false)
            {

                if (second < 59)
                {
                    second++;
                }
                else
                {
                    second = 0;
                    if(minute < 59)
                    {
                        minute++;
                    }
                    else
                    {
                        minute = 0;
                        if(hour < 24)
                        {
                            hour++;
                        }
                        else
                        {
                            hour = 0;
                        }
                    }
                }
            }
            StartCoroutine(ClockTicking());
        }
    }

    public void SetAlarm(int aHour, int aMinute)
    {
        wakeupHour = aHour;
        wakeupMinute = aMinute;
    }

 }



