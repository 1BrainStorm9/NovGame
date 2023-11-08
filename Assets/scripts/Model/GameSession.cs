using Assets.scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    [SerializeField] private InventoryData _inventory;
    [SerializeField] public List<PrefabData> _heroesPrefabs;
    [SerializeField] private TimeOfDayData _dataTime;
    [SerializeField] private QuestManager questManager;
    [SerializeField] public Vector2 position;
    [SerializeField] public List<AssetQuest> quests;
    [SerializeField] public List<AssetQuest> questsIsActive;
    [SerializeField] public List<AssetQuest> questsComplete;

    public string previousSceneName;
    public Vector3 savedPlayerPosition;

    public PlayerData Data => _data;
    public InventoryData InvData => _inventory;
    public List<PrefabData> HeroesPrefabs => _heroesPrefabs;
    public TimeOfDayData DataTime => _dataTime;

    private void Awake()
    {
        questManager = FindAnyObjectByType<QuestManager>();
        foreach (var quest in quests)
        {
            questManager.ActivateQuest(quest.Name);
            if (questsIsActive.Contains(quest))
            {
                questManager.CompleteQuest(quest.Name);
            }
        }

        LoadHud();
        if (isSessionExit())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void GetTimeData()
    {
        var cycleDayNight = FindObjectOfType<CycleDayNight>();
        _dataTime.intensity = cycleDayNight.GetLight().intensity;
    }

    private void LoadHud()
    {
        SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
    }

    private bool isSessionExit()
    {
        var sessions = FindObjectsOfType<GameSession>();
        foreach (var gameSession in sessions)
        {
            if (gameSession != this)
            {
                return true;
            }
        }
        return false;
    }

    public void SavePrefabData(List<PrefabData> prefabData)
    {
        _heroesPrefabs.Clear();
        _heroesPrefabs.AddRange(prefabData);
        PlayerPrefs.SetString("PrefabData", JsonUtility.ToJson(_heroesPrefabs));
    }
}
