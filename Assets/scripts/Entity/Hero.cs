using JetBrains.Annotations;
using UnityEngine;

public class Hero : Creature
{
    [Header("------Characteristics------")]
    public int exp;
    public bool isRun;
    [SerializeField] private int id;
    private SceneType type;

    private static readonly int IsRunning = Animator.StringToHash("is-running");
    protected override void Awake()
    {
        base.Awake();
        type = FindObjectOfType<SceneType>();
    }

    protected void Start()
    {
        AssetItem item = Items.Find(x => x.itemType == ItemType.Weapon);
        if (item == null) { return; }
        var weapon = Instantiate(item.prefab);
        weapon.transform.SetParent(this.transform);
        this.weapon = weapon.GetComponent<Weapon>();
        this.AddWeaponSpellsToHeroSpells();
        this.AddWeaponDamage();
    }

    private void FixedUpdate()
    {
        if (type.GetSceneType() != SceneEnum.peaceScene) { Animator.SetBool(IsRunning, isRun); }
    }


    public int GetId() { return id; }


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
