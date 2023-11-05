using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private AssetQuest assetQuest;
    private GameSession gameSession;
    [SerializeField] public List<AssetQuest> quests;
    [SerializeField] public List<AssetQuest> questsIsActive;
    [SerializeField] public List<AssetQuest> questsComplete;
    private Dictionary<string, bool> rewardsGiven = new Dictionary<string, bool>();

    private void Start()
    {
        gameSession = FindAnyObjectByType<GameSession>();
        assetQuest = FindAnyObjectByType<AssetQuest>();
    }

    public void ActivateQuest(string name) // ��������� ������ � ������� ��� ������������� ������
    {
        AssetQuest questToActivate = FindQuestByName(name);


        if (questToActivate != null)
        {
            if (!questsIsActive.Contains(questToActivate))
            {
                if (!questsComplete.Contains(questToActivate))
                {
                    
                    if (!gameSession.questsComplete.Contains(questToActivate))
                    {
                        gameSession.questsIsActive.Add(questToActivate);
                        questsIsActive.Add(questToActivate);
                    }
                }
            }
        }
    }

    public void CompleteQuest(string name) // ��������� ������ � ������� ��� ���������� ������
    {
        AssetQuest questToComplete = FindQuestByName(name);

        if (questToComplete != null)
        {
            if (questsIsActive.Contains(questToComplete) && !questsComplete.Contains(questToComplete))
            {
                
                if (gameSession.questsIsActive.Contains(questToComplete) && !gameSession.questsComplete.Contains(questToComplete))
                {
                    questsComplete.Add(questToComplete);
                    questsIsActive.Remove(questToComplete);
                    gameSession.questsComplete.Add(questToComplete);
                    gameSession.questsIsActive.Remove(questToComplete);
                }

                if (!questToComplete.IsRewardGiven())
                {
                    GiveReward(questToComplete);
                    questToComplete.SetRewardGiven(true);
                }
            }
        }
    }


    private void GiveReward(AssetQuest quest) // +rep
    {
        Debug.Log("�������� ������� �� ���������� ������: " + quest.Name);

        // ������ ��� ��������� ����� ��� ��?
    }


    public AssetQuest FindQuestByName(string name) // ����� ������ �� �����
    {
        AssetQuest quest = quests.FirstOrDefault(q => q.Name == name);
        return quest;
    }
}
