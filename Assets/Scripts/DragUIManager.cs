using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIManager : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragHandle;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool canDrag;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = eventData.pointerEnter != null &&
                  eventData.pointerEnter.transform.IsChildOf(dragHandle);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        rectTransform.anchoredPosition += eventData.delta;
    }
}