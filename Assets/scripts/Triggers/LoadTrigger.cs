using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private GameSession gameSession;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameSession = FindObjectOfType<GameSession>();
        if (collision.tag == "Player")
        {
            CycleDayNight cycleDayNight = FindAnyObjectByType<CycleDayNight>();             //тык
            if (cycleDayNight != null)                                                      //тык
            {                                                                               
                EnumTime currentTime = cycleDayNight.returnType(cycleDayNight.getTime());   //тык

                if (currentTime == EnumTime.isDay )     //тык
                {
                    gameSession.saveTime = cycleDayNight.getTime();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
                }
            }



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
}
