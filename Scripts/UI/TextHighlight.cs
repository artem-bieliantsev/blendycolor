using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _highlight;
    [SerializeField] private Color _normal;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Text>().color = _highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Text>().color = _normal;
    }
}
