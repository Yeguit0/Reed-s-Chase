using UnityEngine;
using UnityEngine.InputSystem;
public class MouseRestriction : MonoBehaviour
{
    [SerializeField] RectTransform ComputerPanel;
    [SerializeField] RectTransform FakeMouse;
    [SerializeField] Canvas Canvas;
    private RectTransform canvasRect;
    private void Awake()
    {
        canvasRect = Canvas.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }
        updateFakeMouse();
    }
    private void updateFakeMouse()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Canvas.worldCamera, out Vector2 canvasPosition))
        {
            return;
        }

        Vector3 worldPosition = canvasRect.TransformPoint(canvasPosition);
        Vector3 panelLocalPosition = ComputerPanel.InverseTransformPoint(worldPosition);

        Vector2 mousePosition = new Vector2(panelLocalPosition.x, panelLocalPosition.y);

        Vector2 panelSize = ComputerPanel.rect.size;
        Vector2 mouseSize = FakeMouse.rect.size;

        float MinX = ComputerPanel.rect.xMin + mouseSize.x * FakeMouse.pivot.x;
        float MaxX = ComputerPanel.rect.xMax - mouseSize.x * (1f - FakeMouse.pivot.x);

        float MinY = ComputerPanel.rect.yMin + mouseSize.y * FakeMouse.pivot.y;
        float MaxY = ComputerPanel.rect.yMax - mouseSize.y * (1f - FakeMouse.pivot.y);

        mousePosition.x = Mathf.Clamp(mousePosition.x, MinX, MaxX);
        mousePosition.y = Mathf.Clamp(mousePosition.y, MinY, MaxY);

        FakeMouse.anchoredPosition = mousePosition;
    }
}
