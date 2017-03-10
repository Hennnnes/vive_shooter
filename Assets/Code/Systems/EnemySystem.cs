namespace ViveDatabase {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.ECS.APIs;
    using uFrame.ECS.Components;
    using uFrame.ECS.Systems;
    using uFrame.Kernel;
    using UniRx;
    using UnityEngine;
    using ViveDatabase;
    using uFrame.ECS.UnityUtilities;

    public partial class EnemySystem
    {

        public float minDieDistance;

        public GameObject player;

        protected override void EnemySystemKernelLoadedHandler(KernelLoadedEvent data, EnemySpawner group)
        {
            base.EnemySystemKernelLoadedHandler(data, group);

            player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("player: " + player.transform);

        }

        protected override void EnemySystemFixedUpdateHandler(Enemy group)
        {
            base.EnemySystemFixedUpdateHandler(group);

            if (player != null && group.transform != null)
            {
                moveTowards(player, group.transform, 5, group);
            }
            else {
                Debug.Log("player or transform not set");
            }
        }

        void moveTowards(GameObject target, Transform ownTransform, float speed, Enemy group)
        {
            speed = speed * Time.deltaTime;
            ownTransform.LookAt(target.transform);

            if (Vector3.Distance(group.transform.position, target.transform.position) > 0)
            {
                ownTransform.position += ownTransform.forward * speed;
            }
        }


        // not from uframe, because to params would be needed
        protected override void EnemySystemOnTriggerEnterHandler(OnTriggerEnterDispatcher data, Enemy collider, Player source)
        {
            base.EnemySystemOnTriggerEnterHandler(data, collider, source);

            Publish(new MenuEvent());

        }
    }
}
