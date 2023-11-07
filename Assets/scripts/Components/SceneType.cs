using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneType : MonoBehaviour
{
    [SerializeField] private SceneEnum sceneType;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public SceneEnum GetSceneType()
    {
        return sceneType;
    }
    
}

public enum SceneEnum
{
    fightScene,
    peaceScene,
}