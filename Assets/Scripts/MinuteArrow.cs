using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MinuteArrow : MonoBehaviour
{
    public ClockManager manager;
    public TMP_InputField minute;

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
                roundHour = Mathf.RoundToInt(ange / 6);
            }
            else
            {
                roundHour = 60 + Mathf.RoundToInt(ange / 6);
            }

            minute.text = roundHour.ToString();

        }
    }
}
