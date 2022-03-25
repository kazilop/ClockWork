using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HourArrow : MonoBehaviour
{
    public ClockManager manager;
    public TMP_InputField hour;

    private float ange; 
    
    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(BaseEventData data)
    {

        PointerEventData pointer = (PointerEventData)data;
        startPosition = pointer.position;
    }

    public void OnDrag(BaseEventData data)
    {
        if (manager.isUpdate)
        {
            int roundHour = 0;
            PointerEventData pointer = (PointerEventData)data;
            endPosition = pointer.position - startPosition;

            rectTransform.rotation = Quaternion.FromToRotation(startPosition, endPosition);
            ange = Vector2.SignedAngle(endPosition, startPosition);  

            if (ange >= 0)
            {
                roundHour = Mathf.RoundToInt(ange / 30);
            }
            else
            {
                roundHour = 12 + Mathf.RoundToInt(ange / 30);
            }

            hour.text = roundHour.ToString();

        }
    }
}
