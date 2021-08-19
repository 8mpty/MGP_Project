using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Range(0.0f, 5f)] public float holdDuration = 0.5f;
    public UnityEvent onLongPress;

    private bool isPointerDown = false;
    private bool isLongPressed = false;
    private float elapsedTime = 0f;

    private Button button;

    public PlayerController player;

    private void Awake()
    {
        button = GetComponent<Button>();
        player = FindObjectOfType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
    }

    private void Update()
    {
        if (isPointerDown && !isLongPressed)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= holdDuration)
            {
                isLongPressed = true;
                elapsedTime = 0f;
                if (button.interactable && !object.ReferenceEquals(onLongPress, null))
                    onLongPress.Invoke();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        isLongPressed = false;
        elapsedTime = 0f;
        player.PlayerUnCrouch();
    }
}