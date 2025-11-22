using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ObjectInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] public string npcName;
    private DialogManager dialogManager;
    private bool inRange = false;

    [SerializeField] private Image objectImage;

    void Start()
    {
        dialogManager = FindFirstObjectByType<DialogManager>();
        SetAlpha(0f); // start invisible
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("highlight");
        SetAlpha(1f);
        inRange = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetAlpha(0f);
        inRange = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inRange)
        {
            dialogManager.ShowLine(npcName, "1", "2");
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = objectImage.color;
        c.a = alpha;
        objectImage.color = c;
        objectImage.raycastTarget = alpha > 0f;
    }
}
