using UnityEngine;
using UnityEngine.EventSystems;

public class ExitMenuHandler : MonoBehaviour, IPointerExitHandler
{
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(this.gameObject);
    }

    public void DestroyMenu()
    {
        Destroy(this.gameObject);
    }

}
