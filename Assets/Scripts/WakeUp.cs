using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WakeUp : MonoBehaviour
{
    public GameObject panel;
    public GameObject alarmPanel;
    public ClockManager manager;
    public Image alarmImg;

    public int setHour;
    public int setMinute;

    public bool alarm = false;

    public TMP_InputField hour;
    public TMP_InputField minute;
    

    void Start()
    {
        panel.SetActive(false);
        alarmPanel.SetActive(false);
    }

    private void Update()
    {
        CheckForAlarm();
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        manager.isUpdate = true;
    }

    public void SetWakeUp()
    {
        alarm = true;
        setHour = int.Parse(hour.text);
        setMinute = int.Parse(minute.text);
        panel.SetActive(false);
        manager.isUpdate = false;
        manager.GetMyTime();
    }

    public void CloseAlarmPanel()
    {
        alarmPanel.SetActive(false);
        alarm = false;
    }

    public void AlarmPanel()
    {
        alarmPanel.SetActive(true);
    }

    private void CheckForAlarm()
    {
        if (alarm)
        {
            alarmImg.color = Color.red;
        }
        else
        {
            alarmImg.color = Color.white;
        }

        if(manager.hour == setHour && manager.minute == setMinute && manager.second == 0 && alarm)
        {
            AlarmPanel();
        }
    }
    
}
