using Enemies.Base;
using EnemyImplements;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyImplements.All
{
    class ClassicalSeeking : ISeeking
    {
        private float _sight, _distance;
        private Enemy _parent;
        private NavMeshAgent _navmesh;
        public ClassicalSeeking(float sightRange, float distanceKept, Enemy parent)
        {
            _sight = sightRange;
            _distance = distanceKept;
            _parent = parent;
            _navmesh = parent.gameObject.GetComponent<NavMeshAgent>() ?? parent.gameObject.AddComponent<NavMeshAgent>();//GetComponent<NavMeshAgent>();
        }

        public bool SeesHero => Vector3.Distance(Hero.ME.transform.position, _parent.transform.position) <= _sight;

        public void Follow()
        {
            _navmesh.destination = Vector3.Distance(Hero.ME.transform.position, _parent.transform.position) <= _distance ?
                _parent.transform.position : Hero.ME.transform.position;
        }
    }
}
