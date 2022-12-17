using System;
using UnityEngine;

public interface IHealth
{
    event Action<int, Vector3> DamageTaken;
    void DecreaseHP(int value);
    void RestoreHP(int value);
    void UpdateMaxHP(int value);
}