using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateInfo : MonoBehaviour
{
    public int TurnsCount;

    public virtual void StateProcced(Creature entity)
    {

    }


    public virtual void RemoveState()
    {
        Destroy(this);
    }
}

public class DebuffState : StateInfo
{

}

public class BuffState : StateInfo
{

}

public class AttackBuffState : DebuffState
{
    public int damageBuff;
    private bool isBuffed = false;

    public override void StateProcced(Creature entity)
    {
        if (TurnsCount > 0)
        {
            if (!isBuffed)
            {
                entity.damage += damageBuff;
                isBuffed = true;
            }
            TurnsCount--;
        }
        else
        {
            entity.damage -= damageBuff;
            entity.CharStates.Remove(this);
            RemoveState();

        }
    }
}

public class TemporalDamageState : DebuffState
{
    public float temporalDamage;

    public override void StateProcced(Creature entity)
    {
        if (TurnsCount > 0)
        {
            entity.health -= temporalDamage;
            TurnsCount--;
        }
        else
        {
            entity.CharStates.Remove(this);
            RemoveState();

        }
    }
}