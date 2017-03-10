namespace ViveDatabase
{
    using uFrame.ECS.APIs;
    using uFrame.ECS.Components;
    using uFrame.ECS.Systems;
    using uFrame.ECS.UnityUtilities;
    using uFrame.Kernel;
    using UniRx;
    using UnityEngine;
    using ViveDatabase;


    public partial class EnemySpawnSystem
    {
        private int counter = 0;

        public bool gameStarted = false;

        public GameObject Player;

        public float spawnTimer;

        protected override void EnemySpawnSystemKernelLoadedHandler(KernelLoadedEvent data, EnemySpawner group)
        {
            base.EnemySpawnSystemKernelLoadedHandler(data, group);
            Player = Camera.main.gameObject;
            spawnTimer = 100;

        }


        protected override void EnemySpawnSystemFixedUpdateHandler(EnemySpawner group)
        {
            base.EnemySpawnSystemFixedUpdateHandler(group);


            if (gameStarted)
            {
                float distance = 0;
                spawnTimer -= 0.01f;
                Vector3 spawn = new Vector3();

                while (distance < 20)
                {

                    float randomX = Random.RandomRange(-50, 50);
                    float randomY = Random.RandomRange(2, 10);
                    float randomZ = Random.RandomRange(-50, 50);

                    spawn = new Vector3(randomX, randomY, randomZ);

                    distance = Vector3.Distance(spawn, Player.transform.position);

                }

                if (counter % (int)spawnTimer == 0)
                {
                    Instantiate(group.prefab, spawn, Quaternion.identity);
                    counter = 0;
                }

                counter++;
            }

        }

        protected override void EnemySpawnSystemOnTriggerEnterWeaponHandler(OnTriggerEnterDispatcher data, Weapon collider, WandRight source)
        {
            base.EnemySpawnSystemOnTriggerEnterWeaponHandler(data, collider, source);

            gameStarted = true;

            collider.transform.SetParent(source.transform);
            collider.transform.localPosition = new Vector3(0, 0, 0.545f);
            collider.transform.rotation = new Quaternion();

            collider.GetComponent<BoxCollider>().enabled = false;


        }
     
        protected override void EnemySpawnSystemOnTriggerEnterHandler(OnTriggerEnterDispatcher data, bullet collider, Enemy source)
        {
            base.EnemySpawnSystemOnTriggerEnterHandler(data, collider, source);

            Destroy(source.gameObject);
            Destroy(collider.gameObject);
        }

        protected override void EnemySpawnSystemMenuEventHandler(MenuEvent data)
        {
            base.EnemySpawnSystemMenuEventHandler(data);
            gameStarted = false;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }

    }
}
