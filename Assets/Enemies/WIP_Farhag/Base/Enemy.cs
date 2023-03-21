using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralImplements;
using EnemyImplements;
using DTO;

namespace Enemies.Base
{
    public class Enemy : MonoBehaviour, IHittable
    {

        protected IDamageable damageable;
        protected IStaggerable staggerable;
        protected ISeeking seeking;
        protected List<IAttack> attacks;
        public Enemy()
        {
            this.damageable = null;
            this.staggerable = null;
            this.seeking = null;
        }

        public void Hit(HitData data) //TODO: Add callback/out
        {
            damageable.TakeDamage(data);
            if (staggerable?.ShouldStagger(data) ?? false)
                staggerable.Stagger();
        }

        protected virtual void Update()
        { }
        protected virtual void FixedUpdate()
        { }


    }
}
