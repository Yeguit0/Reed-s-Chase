using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIManager : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [Header("UI")]
    [SerializeField] private RectTransform dragHandle;
    [SerializeField] private RectTransform ScreenPanel;
    private float percentageToReturn = 0.35f;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    private readonly Vector3[] corners = new Vector3[4];
    private bool canDrag;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
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

        CheckOutsidePanel();
    }

    private void CheckOutsidePanel()
    {
        rectTransform.GetWorldCorners(corners);

        float objectWidth = Vector3.Distance(corners[0], corners[3]);
        float objectHeight = Vector3.Distance(corners[0], corners[1]);

        float totalArea = objectWidth * objectHeight;

        Vector3[] panelCorners = new Vector3[4];
        ScreenPanel.GetWorldCorners(panelCorners);

        float panelLeft = panelCorners[0].x;
        float panelRight = panelCorners[2].x;
        float panelBottom = panelCorners[0].y;
        float panelTop = panelCorners[2].y;

        float left = Mathf.Max(corners[0].x, panelLeft);
        float right = Mathf.Min(corners[2].x, panelRight);
        float bottom = Mathf.Max(corners[0].y, panelBottom);
        float top = Mathf.Min(corners[2].y, panelTop);

        float width = Mathf.Max(0, right - left);
        float height = Mathf.Max(0, top - bottom);

        float insideArea = width * height;

        float outsidePercentage = 1f - (insideArea / totalArea);

        if (outsidePercentage > percentageToReturn)
        {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}