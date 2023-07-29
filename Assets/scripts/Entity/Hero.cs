using UnityEngine;

public class Hero : Creature
{
    private static readonly int IsRunning = Animator.StringToHash("is-running");
    public Hero prefab;

    public bool isRun;

    private void FixedUpdate()
    {
        Animator.SetBool(IsRunning, isRun);
    }

    
}
