using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Assets.scripts.Entity
{
    public class DataAfterLoadController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> heroList;
        private GameSession _session;
        [SerializeField] private GameObject _panel;
        [SerializeField] private List<GameObject> startPositions;
        private SceneEnum sceneType;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var type = FindObjectOfType<SceneType>();
            if (type != null) sceneType = type.GetSceneType();
            
            LoadHeros(_session);


        }
        private void LoadHeros(GameSession gameSession)
        {
            switch (sceneType)
            {
                case SceneEnum.fightScene:
                    var prefabs = _session.HeroesPrefabs;
                    foreach (var prefab in prefabs)
                    {
                        var heroInst = Instantiate(heroList[prefab.HeroId]);
                        heroInst.transform.position = startPositions[prefab.HeroId].transform.position;
                        var hero = heroInst.GetComponent<Hero>();
                        if(prefab.health == 0)
                        {
                            return;
                        }

                        hero.exp = prefab.exp;
                        hero.health = prefab.health;
                        hero.maxHealth = prefab.maxHealth;
                        hero.damage = prefab.damage;
                        hero.criticalDamage = prefab.criticalDamage;
                        hero.criticalChance = prefab.criticalChance;
                        hero.protect = prefab.protect;
                        hero.evasionChance = prefab.evasionChance;
                        hero.creatureName = prefab.creatureName;
                        hero.lvl = prefab.lvl;
                        hero.Items = prefab.Items;
                        hero.weapon = prefab.weapon;
                        hero.CharStates = prefab.CharStates;
                        hero.masteryLevel = prefab.masteryLevel;

                    }
                    break;

                case SceneEnum.peaceScene:

                    break;

            }
        }

    }
}