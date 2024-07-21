using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)]
    public float leverRange;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
    }

    public void ControlJoyStickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;

        var clampedDir = inputDir.magnitude < leverRange ?
            inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;
        Debug.Log(clampedDir);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
    }

}
