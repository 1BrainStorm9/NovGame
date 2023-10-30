using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSession;
using static UnityEngine.GraphicsBuffer;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private GameSession gameSession;

    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CycleDayNight cycleDayNight = FindAnyObjectByType<CycleDayNight>();
            if (cycleDayNight != null && cycleDayNight.returnType(cycleDayNight.getTime()) == EnumTime.isDay)                                                     
            {
                gameSession.CompleteQuest("VisitNewLocation"); // complete quest

                gameSession.GetTimeData();
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            /*var hero = collision.GetComponent<Creature>();
            var tempPoisonState = hero.GetComponent<TemporalDamageState>();
            if (tempPoisonState == null)
            { 
            TemporalDamageState PoisonState = hero.AddComponent<TemporalDamageState>();
            PoisonState.TurnsCount = 2;
            PoisonState.temporalDamage = 3;
            hero.CharStates.Add(PoisonState);
            }
            else
            {
                tempPoisonState.TurnsCount += 1;
            }*/
        }
    }

    public bool IsQuestCompleted(string questName)
    {
        Quest quest = gameSession.quests.Find(q => q.questName == questName);
        return quest != null && quest.isCompleted;
    }

    public void MarkCompleted(string questName)
    {
        Quest quest = gameSession.quests.Find(q => q.questName == questName);
        if (quest != null)
        {
            quest.isCompleted = true;
        }
    }
}
