using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;

    public static Action<Enemy> OnEnemyAdded;

    private ColliderCreationController enemyColiderController;

    private void OnEnable()
    {
        GetEnemiesInCollider.OnEnemyTriggerEnter += AddEnemy;
        GameController.OnKilledEnemy += RemoveEnemy;
    }

    private void OnDisable()
    {
        GetEnemiesInCollider.OnEnemyTriggerEnter -= AddEnemy;
        GameController.OnKilledEnemy -= RemoveEnemy;
    }

    private void Start()
    {
        enemyColiderController = FindObjectOfType<ColliderCreationController>();
        enemyColiderController.AddCollider(Component.FindAnyObjectByType<Camera>().transform);
    }

    private void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        OnEnemyAdded?.Invoke(enemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
