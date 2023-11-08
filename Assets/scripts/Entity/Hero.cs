using JetBrains.Annotations;
using UnityEngine;

public class Hero : Creature
{
    [Header("------Characteristics------")]
    public int exp;
    public bool isRun;
    [SerializeField] private int id;


    private static readonly int IsRunning = Animator.StringToHash("is-running");


    public int GetId() { return id; }

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
            if(weapon != null)
            {
                DeleteWeaponDamage();
                masteryLevel.WeaponMasteryUp(weapon.weaponType);
                AddWeaponDamage();
            }
            _particles.Spawn("LevelUp");
        }
    }

    public void AddExp(int tempExp)
    {
        exp += tempExp;
        if(exp >= 200) LevelUp();
    }
}
