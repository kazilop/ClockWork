using UnityEngine;

public class ClockRing : MonoBehaviour
{
        
    public ClockManager _clockManager;

    private RectTransform _hour;
    private RectTransform _minute;
    private RectTransform _second;

    void Start()
    {
        
        _hour = (RectTransform)GetComponentInChildren<RectTransform>().Find("Hour");
        _minute = (RectTransform)GetComponentInChildren<RectTransform>().Find("Minute");
        _second = (RectTransform)GetComponentInChildren<RectTransform>().Find("Second");
       
    }

    void FixedUpdate()
    {
        SetArrow();
    }

    public void SetArrow()
    {
        _hour.transform.rotation = Quaternion.Euler(0, 0, -30 * _clockManager.hour);
        _minute.transform.rotation = Quaternion.Euler(0, 0, -30 * _clockManager.minute);
        _second.transform.rotation = Quaternion.Euler(0, 0, -30 * _clockManager.second);
    }
}
