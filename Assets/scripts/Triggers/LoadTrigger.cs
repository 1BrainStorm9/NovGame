using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            var hero = collision.GetComponent<Creature>();
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
            }
        }
    }
}
