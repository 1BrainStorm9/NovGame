using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnToPoint : MonoBehaviour
{
    private GameSession gameSession;

    [System.Serializable]
    public class SceneToObjectsMapping
    {
        public string sceneName;
        public GameObject targetObject;
    }

    public List<SceneToObjectsMapping> sceneToObjectsMappings;

    private Transform playerTransform;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        string previeScene = gameSession.previousSceneName;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (previeScene != "Pole")
        {
            SceneToObjectsMapping mapping = sceneToObjectsMappings.Find(objs => objs.sceneName == previeScene);

            if (mapping != null)
            {
                playerTransform.position = mapping.targetObject.transform.position;
            }
        }
        else
        {
            playerTransform.position = gameSession.savedPlayerPosition;
        }
    }
}
