
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRestriction : MonoBehaviour
{
    [SerializeField] private RectTransform ComputerPanel;
    [SerializeField] private RectTransform FakeMouse;
    [SerializeField] private Canvas Canvas;

    private bool cursorLocked = true;

    private Vector3[] corners = new Vector3[4];

    private Camera UICamera =>
        Canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : Canvas.worldCamera;

    private void Awake()
    {
        if (!ComputerPanel || !FakeMouse || !Canvas)
        {
            Debug.LogError("MouseRestriction: faltan referencias en el Inspector.");
            enabled = false;
        }
    }

    private void Start()
    {
        SetCursorMode(true);
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.pKey.wasPressedThisFrame)
        {
            SetCursorMode(!cursorLocked);
        }

        if (cursorLocked && Mouse.current != null)
        {
            UpdateFakeMouse();
        }
    }

    private void SetCursorMode(bool locked)
    {
        cursorLocked = locked;

        Cursor.lockState = locked
            ? CursorLockMode.Confined
            : CursorLockMode.None;

        Cursor.visible = !locked;

        FakeMouse.gameObject.SetActive(locked);
    }

    private void UpdateFakeMouse()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ComputerPanel,
            screenPosition,
            UICamera,
            out Vector2 localPosition))
        {
            return;
        }

        Vector2 clampedPosition = ClampMouseToPanel(localPosition);

        Vector3 worldPosition = ComputerPanel.TransformPoint(
            new Vector3(clampedPosition.x, clampedPosition.y, 0f)
        );

        FakeMouse.position = worldPosition;
    }

    private Vector2 ClampMouseToPanel(Vector2 position)
    {
        Rect panelRect = ComputerPanel.rect;

        FakeMouse.GetWorldCorners(corners);

        Vector3 pivotPosition = ComputerPanel.InverseTransformPoint(
            FakeMouse.position
        );

        float minOffsetX = float.PositiveInfinity;
        float maxOffsetX = float.NegativeInfinity;
        float minOffsetY = float.PositiveInfinity;
        float maxOffsetY = float.NegativeInfinity;

        for (int i = 0; i < 4; i++)
        {
            Vector3 corner = ComputerPanel.InverseTransformPoint(corners[i]);

            float offsetX = corner.x - pivotPosition.x;
            float offsetY = corner.y - pivotPosition.y;

            minOffsetX = Mathf.Min(minOffsetX, offsetX);
            maxOffsetX = Mathf.Max(maxOffsetX, offsetX);

            minOffsetY = Mathf.Min(minOffsetY, offsetY);
            maxOffsetY = Mathf.Max(maxOffsetY, offsetY);
        }

        float minX = panelRect.xMin - minOffsetX;
        float maxX = panelRect.xMax - maxOffsetX;

        float minY = panelRect.yMin - minOffsetY;
        float maxY = panelRect.yMax - maxOffsetY;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        return position;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
