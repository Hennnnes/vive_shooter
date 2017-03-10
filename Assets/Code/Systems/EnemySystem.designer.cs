// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

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
    
    
    public partial class EnemySystemBase : uFrame.ECS.Systems.EcsSystem, uFrame.ECS.APIs.ISystemFixedUpdate {
        
        private IEcsComponentManagerOf<Wands> _WandsManager;
        
        private IEcsComponentManagerOf<EnemySpawner> _EnemySpawnerManager;
        
        private IEcsComponentManagerOf<Menu> _MenuManager;
        
        private IEcsComponentManagerOf<WandLeft> _WandLeftManager;
        
        private IEcsComponentManagerOf<bullet> _bulletManager;
        
        private IEcsComponentManagerOf<Enemy> _EnemyManager;
        
        private IEcsComponentManagerOf<Weapon> _WeaponManager;
        
        private IEcsComponentManagerOf<WandRight> _WandRightManager;
        
        private IEcsComponentManagerOf<Player> _PlayerManager;
        
        public IEcsComponentManagerOf<Wands> WandsManager {
            get {
                return _WandsManager;
            }
            set {
                _WandsManager = value;
            }
        }
        
        public IEcsComponentManagerOf<EnemySpawner> EnemySpawnerManager {
            get {
                return _EnemySpawnerManager;
            }
            set {
                _EnemySpawnerManager = value;
            }
        }
        
        public IEcsComponentManagerOf<Menu> MenuManager {
            get {
                return _MenuManager;
            }
            set {
                _MenuManager = value;
            }
        }
        
        public IEcsComponentManagerOf<WandLeft> WandLeftManager {
            get {
                return _WandLeftManager;
            }
            set {
                _WandLeftManager = value;
            }
        }
        
        public IEcsComponentManagerOf<bullet> bulletManager {
            get {
                return _bulletManager;
            }
            set {
                _bulletManager = value;
            }
        }
        
        public IEcsComponentManagerOf<Enemy> EnemyManager {
            get {
                return _EnemyManager;
            }
            set {
                _EnemyManager = value;
            }
        }
        
        public IEcsComponentManagerOf<Weapon> WeaponManager {
            get {
                return _WeaponManager;
            }
            set {
                _WeaponManager = value;
            }
        }
        
        public IEcsComponentManagerOf<WandRight> WandRightManager {
            get {
                return _WandRightManager;
            }
            set {
                _WandRightManager = value;
            }
        }
        
        public IEcsComponentManagerOf<Player> PlayerManager {
            get {
                return _PlayerManager;
            }
            set {
                _PlayerManager = value;
            }
        }
        
        public override void Setup() {
            base.Setup();
            WandsManager = ComponentSystem.RegisterComponent<Wands>(1);
            EnemySpawnerManager = ComponentSystem.RegisterComponent<EnemySpawner>(9);
            MenuManager = ComponentSystem.RegisterComponent<Menu>(7);
            WandLeftManager = ComponentSystem.RegisterComponent<WandLeft>(2);
            bulletManager = ComponentSystem.RegisterComponent<bullet>(4);
            EnemyManager = ComponentSystem.RegisterComponent<Enemy>(10);
            WeaponManager = ComponentSystem.RegisterComponent<Weapon>(8);
            WandRightManager = ComponentSystem.RegisterComponent<WandRight>(3);
            PlayerManager = ComponentSystem.RegisterComponent<Player>(5);
            this.OnEvent<uFrame.Kernel.KernelLoadedEvent>().Subscribe(_=>{ EnemySystemKernelLoadedFilter(_); }).DisposeWith(this);
            this.OnEvent<uFrame.ECS.UnityUtilities.OnTriggerEnterDispatcher>().Subscribe(_=>{ EnemySystemOnTriggerEnterFilter(_); }).DisposeWith(this);
        }
        
        protected virtual void EnemySystemFixedUpdateHandler(Enemy group) {
        }
        
        protected void EnemySystemFixedUpdateFilter() {
            var EnemyItems = EnemyManager.Components;
            for (var EnemyIndex = 0
            ; EnemyIndex < EnemyItems.Count; EnemyIndex++
            ) {
                if (!EnemyItems[EnemyIndex].Enabled) {
                    continue;
                }
                this.EnemySystemFixedUpdateHandler(EnemyItems[EnemyIndex]);
            }
        }
        
        public virtual void SystemFixedUpdate() {
            EnemySystemFixedUpdateFilter();
        }
        
        protected virtual void EnemySystemKernelLoadedHandler(uFrame.Kernel.KernelLoadedEvent data, EnemySpawner group) {
        }
        
        protected void EnemySystemKernelLoadedFilter(uFrame.Kernel.KernelLoadedEvent data) {
            var EnemySpawnerItems = EnemySpawnerManager.Components;
            for (var EnemySpawnerIndex = 0
            ; EnemySpawnerIndex < EnemySpawnerItems.Count; EnemySpawnerIndex++
            ) {
                if (!EnemySpawnerItems[EnemySpawnerIndex].Enabled) {
                    continue;
                }
                this.EnemySystemKernelLoadedHandler(data, EnemySpawnerItems[EnemySpawnerIndex]);
            }
        }
        
        protected virtual void EnemySystemOnTriggerEnterHandler(uFrame.ECS.UnityUtilities.OnTriggerEnterDispatcher data, Enemy collider, Player source) {
        }
        
        protected void EnemySystemOnTriggerEnterFilter(uFrame.ECS.UnityUtilities.OnTriggerEnterDispatcher data) {
            var ColliderEnemy = EnemyManager[data.ColliderId];
            if (ColliderEnemy == null) {
                return;
            }
            if (!ColliderEnemy.Enabled) {
                return;
            }
            var SourcePlayer = PlayerManager[data.EntityId];
            if (SourcePlayer == null) {
                return;
            }
            if (!SourcePlayer.Enabled) {
                return;
            }
            this.EnemySystemOnTriggerEnterHandler(data, ColliderEnemy, SourcePlayer);
        }
    }
    
    [uFrame.Attributes.uFrameIdentifier("2842d715-1bcf-46a9-924f-a9ada97b9594")]
    public partial class EnemySystem : EnemySystemBase {
        
        private static EnemySystem _Instance;
        
        public EnemySystem() {
            Instance = this;
        }
        
        public static EnemySystem Instance {
            get {
                return _Instance;
            }
            set {
                _Instance = value;
            }
        }
    }
}
