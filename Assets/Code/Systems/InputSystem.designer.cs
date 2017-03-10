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
    
    
    public partial class InputSystemBase : uFrame.ECS.Systems.EcsSystem {
        
        private IEcsComponentManagerOf<WandLeft> _WandLeftManager;
        
        private IEcsComponentManagerOf<bullet> _bulletManager;
        
        private IEcsComponentManagerOf<WandRight> _WandRightManager;
        
        private IEcsComponentManagerOf<Player> _PlayerManager;
        
        private IEcsComponentManagerOf<Wands> _WandsManager;
        
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
        
        public IEcsComponentManagerOf<Wands> WandsManager {
            get {
                return _WandsManager;
            }
            set {
                _WandsManager = value;
            }
        }
        
        public override void Setup() {
            base.Setup();
            WandLeftManager = ComponentSystem.RegisterComponent<WandLeft>(2);
            bulletManager = ComponentSystem.RegisterComponent<bullet>(4);
            WandRightManager = ComponentSystem.RegisterComponent<WandRight>(3);
            PlayerManager = ComponentSystem.RegisterComponent<Player>(5);
            WandsManager = ComponentSystem.RegisterComponent<Wands>(1);
        }
    }
    
    [uFrame.Attributes.uFrameIdentifier("78c0d850-4d4f-4de4-a31f-55c314216749")]
    public partial class InputSystem : InputSystemBase {
        
        private static InputSystem _Instance;
        
        public InputSystem() {
            Instance = this;
        }
        
        public static InputSystem Instance {
            get {
                return _Instance;
            }
            set {
                _Instance = value;
            }
        }
    }
}
