using System;

public interface IEnemy
{
    event Action<int> Died;
}