using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Enemy : Creature
{
    private Creature enemyTarget;

    public void EnemyTurn()
    { 
        if(enemyTarget != null)
        {
            Attack(enemyTarget, 1f);
        }
    }


    public Creature AIChooseTarget(System.Collections.Generic.List<Hero> targets)
    {
        int randomIndex = Random.Range(0, targets.Count);
        enemyTarget = targets[randomIndex];
        return enemyTarget;
    }
}
