using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Enemy : Entity
{
    private Entity enemyTarget;

    public void EnemyTurn()
    { 
        if(enemyTarget != null)
        {
            Attack(enemyTarget, 1f);
        }
    }


    public Entity AIChooseTarget(System.Collections.Generic.List<Player> targets)
    {
        int randomIndex = Random.Range(0, targets.Count);
        enemyTarget = targets[randomIndex];
        return enemyTarget;
    }
}
