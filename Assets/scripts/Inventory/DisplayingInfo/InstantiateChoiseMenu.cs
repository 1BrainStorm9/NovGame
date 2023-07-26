using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateChoiseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _container;
    private GameObject _panel;

    public void InstantiateMenu(PointerEventData eventData)
    {
        _panel = Instantiate(_prefab, _container);
        _panel.transform.position = eventData.position;
    }

}
