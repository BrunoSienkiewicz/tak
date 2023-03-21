/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using EnemyImplements;
using UnityEngine;

namespace EnemyImplements.All
{
    class ClassicalHP : IDamageable
    {
        private Enemy _parent; 
        private int _max, _current;

        public ClassicalHP(int maxHP,Enemy parent)
        {
            _parent = parent; 
            _max = maxHP;
            _current = maxHP;
        }

        public bool IsDead => _current<=0;

        public void Die()
        {
            Debug.Log($"{_parent.GetType().Name} was met with God-awful end!");
        }

        public void Recover(int amount)
        {
            _current += amount;
            _current = _current <= _max ? _current : _max;
        }

        public void TakeDamage(HitData data)
        {
            _current -= data.baseDamage;
            _current = _current >= 0 ? _current : 0;
        }
    }
}*/
