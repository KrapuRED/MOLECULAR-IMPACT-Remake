using UnityEngine;
using UnityEngine.Events;

public class CharacterButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Camera _camera;
    [SerializeField] private int _mouseButton = 0; // 0 = Left, 1 = Right, 2 = Middle

    [Header("Events")]
    public UnityEvent OnClick;
    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;

    private bool _isHovered = false;

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    private void Update()
    {
        bool hitThisFrame = IsMouseOver();

        // Hover enter
        if (hitThisFrame && !_isHovered)
        {

            _isHovered = true;
            OnHoverEnter?.Invoke();
        }

        // Hover exit
        if (!hitThisFrame && _isHovered)
        {

            _isHovered = false;
            OnHoverExit?.Invoke();
        }

        // Click
        if (_isHovered && Input.GetMouseButtonDown(_mouseButton))
        {
   
            OnClick?.Invoke();
        }
    }

    private bool IsMouseOver()
    {
        Vector2 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
}
