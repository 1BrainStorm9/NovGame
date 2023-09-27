using UnityEngine;

public class Hero : Creature
{
    [Header("------Characteristics------")]
    public int exp;

    public Hero prefab;
    public bool isRun;

    private static readonly int IsRunning = Animator.StringToHash("is-running");


    private void FixedUpdate()
    {
        Animator.SetBool(IsRunning, isRun);
    }

    public void LevelUp()
    {
        if (exp >= 200) 
        {
            exp -= 200;
            lvl++;
            _particles.Spawn("LevelUp");
        }
    }

    public void AddExp(int tempExp)
    {
        exp += tempExp;
    }
}
