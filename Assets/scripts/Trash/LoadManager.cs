using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public void DestroyAllCreaturesAndLoadNewScene()
    {
        foreach (var creatures in GetComponent<GameController>().fightList)
        {
            creatures.Destroy();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
    }
}
