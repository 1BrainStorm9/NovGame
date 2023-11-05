using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneType : MonoBehaviour
{
    [SerializeField] private SceneEnum sceneType;

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