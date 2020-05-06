using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController playerController;
    public int index;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.buttonsDown[index] = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.buttonsDown[index] = false;
    }
}