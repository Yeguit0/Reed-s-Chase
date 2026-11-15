using UnityEngine;
using UnityEngine.InputSystem;
public class MouseRestriction : MonoBehaviour
{
    [SerializeField] RectTransform ComputerPanel;
    [SerializeField] RectTransform FakeMouse;
    [SerializeField] Canvas Canvas;
    private RectTransform canvasRect;
    private bool cursorLocked = true;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Awake()
    {
        canvasRect = Canvas.GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        if (Mouse.current == null)
        {
            return;
        }
        updateFakeMouse();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            cursorLocked = !cursorLocked;

            Cursor.lockState = cursorLocked ? CursorLockMode.Confined : CursorLockMode.None;
            Cursor.visible = !cursorLocked;
        }
    }

    private void updateFakeMouse()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ComputerPanel,
            screenPosition,
            Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Canvas.worldCamera,
            out Vector2 mousePosition))
        {
            return;
        }

        Vector2 mouseSize = FakeMouse.rect.size;

        float minX = ComputerPanel.rect.xMin + mouseSize.x * FakeMouse.pivot.x;
        float maxX = ComputerPanel.rect.xMax - mouseSize.x * (1f - FakeMouse.pivot.x);

        float minY = ComputerPanel.rect.yMin + mouseSize.y * FakeMouse.pivot.y;
        float maxY = ComputerPanel.rect.yMax - mouseSize.y * (1f - FakeMouse.pivot.y);

        mousePosition.x = Mathf.Clamp(mousePosition.x, minX, maxX);
        mousePosition.y = Mathf.Clamp(mousePosition.y, minY, maxY);

        FakeMouse.anchoredPosition = mousePosition;

        Vector3 worldPosition = ComputerPanel.TransformPoint(mousePosition);

        Vector2 clampedScreenPosition = RectTransformUtility.WorldToScreenPoint(
            Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Canvas.worldCamera,
            worldPosition
        );

        if (Vector2.Distance(screenPosition, clampedScreenPosition) > 1f)
        {
            Mouse.current.WarpCursorPosition(clampedScreenPosition);
        }
    }


}
