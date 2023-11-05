using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryHud;
    [SerializeField] GameObject spellHud;
    private SceneEnum sceneType;
    private bool isOpen;

    void Start()
    {
        var type = FindObjectOfType<SceneType>();
        if (type != null) sceneType = type.GetSceneType();
        HideHud();
    }

    private void Update()
    {
        OpenInv();
    }

    private void HideHud()
    {
        switch (sceneType)
        {
            case SceneEnum.fightScene:
                spellHud.SetActive(true);
                inventoryHud.SetActive(false);
                isOpen = false;
                break;
                                           
            case SceneEnum.peaceScene:
                spellHud.SetActive(false);
                inventoryHud.SetActive(false);
                isOpen = false;
                break;
            
        }
    }

    private void OpenInv()
    {
        if(Input.GetKeyUp(KeyCode.I)) {
            isOpen = !isOpen;
            inventoryHud.SetActive(isOpen);
        }
    }

}
