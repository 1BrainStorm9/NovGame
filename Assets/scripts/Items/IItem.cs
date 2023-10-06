using UnityEngine;

public interface IItem
{
    Sprite UIIcon { get; }
    string Name { get; }
    string Description { get; }
    int Price { get; }
    int Protection { get; }
    int Health { get; }
    int Evasion { get; }
    int Weight { get; } // тык
}
