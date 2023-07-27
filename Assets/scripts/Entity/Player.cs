using PixelCrew.Components.GoBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player : Entity
{
    private static readonly int IsRunning = Animator.StringToHash("is-running");


    public bool isRun;

    private void FixedUpdate()
    {
        Animator.SetBool(IsRunning, isRun);
    }

}
