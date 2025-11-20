using UnityEngine;
using UnityEngine.EventSystems;

public class BringToFront : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
