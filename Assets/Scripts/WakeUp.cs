using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : MonoBehaviour
{
    public GameObject panel;
    public ClockManager manager;


    public string setHour;
    public string setMinute;

    

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        manager.isUpdate = true;
        
    }

    public void SetWakeUp()
    {
        panel.SetActive(false);
        manager.isUpdate = false;
    }
}
