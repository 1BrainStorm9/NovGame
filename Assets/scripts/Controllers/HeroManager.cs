using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> heroes;
    public int heroSpeed = 0;

    private void OnEnable()
    {
        GameController.OnKilledHero += RemoveHero;
        CameraController.OnTriggerEnter += SetBasicHeroSpeed;
        GameController.OnKilledAllEnemies += SetRunningHeroSpeed;
    }

    private void OnDisable()
    {
        GameController.OnKilledHero -= RemoveHero;
        CameraController.OnTriggerEnter -= SetBasicHeroSpeed;
        GameController.OnKilledAllEnemies -= SetRunningHeroSpeed;
    }

    private void Start()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player").ToList())
        {
            heroes.Add(player.GetComponent<Hero>());
        }
    }

    private void RemoveHero(Hero hero)
    {
        heroes.Remove(hero);
    }

    private void SetBasicHeroSpeed()
    {
        heroSpeed = 0;
        foreach (var item in heroes)
        {
            item.isRun = false;
        }
    }
    private void SetRunningHeroSpeed()
    {
        heroSpeed = 5;
        foreach (var item in heroes)
        {
            item.isRun = true;
        }
    }
}
