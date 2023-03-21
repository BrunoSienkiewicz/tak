using System;
using System.Collections.Generic;
using DTO;
using Enemies.Base;

namespace EnemyImplements
{
    public interface IDamageable
    {
        void TakeDamage(HitData data);
        void Recover(int amount);
        bool IsDead { get; }
        void Die();
    }

    public interface IStaggerable
    {
        bool ShouldStagger(HitData data);
        void Stagger();
    }

    public interface ISeeking 
    {
        bool SeesHero { get; }
        void Follow();
    }

    public interface IAttack
    {
        bool Available { get; }
        void Execute(EnemyDescriptor descriptor);
    }
}