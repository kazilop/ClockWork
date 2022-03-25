using UnityEngine;

public class ClockRing : MonoBehaviour
{
    public ClockManager manager;

    private RectTransform hour;
    private RectTransform minute;
    private RectTransform second;

    void Start()
    {
        
        hour = (RectTransform)GetComponentInChildren<RectTransform>().Find("Hour");
        minute = (RectTransform)GetComponentInChildren<RectTransform>().Find("Minute");
        second = (RectTransform)GetComponentInChildren<RectTransform>().Find("Second");
       
    }

    void FixedUpdate()
    {
        SetArrow();
    }

    public void SetArrow()
    {
        if (!manager.isUpdate)
        {
            hour.transform.rotation = Quaternion.Euler(0, 0, -30 * manager.hour);
            minute.transform.rotation = Quaternion.Euler(0, 0, -6 * manager.minute);
            second.transform.rotation = Quaternion.Euler(0, 0, -6 * manager.second);
        }
    }
}
