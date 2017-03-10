namespace ViveDatabase {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.ECS.APIs;
    using uFrame.ECS.Components;
    using uFrame.ECS.Systems;
    using uFrame.ECS.UnityUtilities;
    using uFrame.Kernel;
    using UniRx;
    using UnityEngine;
    using ViveDatabase;
    using UnityEngine.SceneManagement;


    public partial class InputSystem
    {

        public SteamVR_TrackedController trackedContrLeft;
        public SteamVR_TrackedController trackedContrRight;
        private SteamVR_Controller.Device controller;
        public SteamVR_TrackedObject trackedObj;
        public int i = 0;
        GameObject mainCamera;
        GameObject Menu;

        protected LineRenderer lineRenderer;
        protected Vector3[] lineRendererVertices;

        //k_EButton_System = 0,
        //k_EButton_ApplicationMenu = 1,
        //k_EButton_Grip = 2,
        //k_EButton_DPad_Left = 3,
        //k_EButton_DPad_Up = 4,
        //k_EButton_DPad_Right = 5,
        //k_EButton_DPad_Down = 6,
        //k_EButton_A = 7,
        //k_EButton_ProximitySensor = 31,
        //k_EButton_Axis0 = 32,
        //k_EButton_Axis1 = 33,
        //k_EButton_Axis2 = 34,
        //k_EButton_Axis3 = 35,
        //k_EButton_Axis4 = 36,
        //k_EButton_SteamVR_Touchpad = 32,
        //k_EButton_SteamVR_Trigger = 33,
        //k_EButton_Dashboard_Back = 2,
        //k_EButton_Max = 64,

        private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
        private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
        private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
        private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;


        protected override void InputSystemKernelLoadedHandler(KernelLoadedEvent data, Wands group)
        {
            base.InputSystemKernelLoadedHandler(data, group);
            trackedObj = group.Right.GetComponent<SteamVR_TrackedObject>();
            trackedContrLeft = group.Left.GetComponent<SteamVR_TrackedController>();
            trackedContrRight = group.Right.GetComponent<SteamVR_TrackedController>();
            controller = SteamVR_Controller.Input((int)trackedObj.index);
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            Menu = GameObject.FindGameObjectWithTag("Menu");
            Menu.SetActive(false);



            // Initialize our LineRenderer
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.SetWidth(0.01f, 0.01f);
            lineRenderer.SetVertexCount(2);

            // Initialize our vertex array. This will just contain
            // two Vector3's which represent the start and end locations
            // of our LineRenderer
            lineRendererVertices = new Vector3[2];
        }


        protected override void InputSystemUpdateHandler(Wands group)
        {
            base.InputSystemUpdateHandler(group);
            if (trackedContrRight.triggerPressed)
            {
                Publish(new ShootEvent());
            }

            if (trackedContrRight.padPressed)
            {
                Publish(new TeleportEvent());
            }

            // Update our LineRenderer
            if (lineRenderer && lineRenderer.enabled)
            {
                RaycastHit hit;
                Vector3 startPos = trackedContrRight.transform.position; ;

                // If our raycast hits, end the line at that position. Otherwise,
                // just make our line point straight out for 1000 meters.
                // If the raycast hits, the line will be green, otherwise it'll be red.
                if (Physics.Raycast(startPos, trackedContrRight.transform.forward, out hit, 1000.0f))
                {
                    lineRendererVertices[1] = hit.point;
                    lineRenderer.SetColors(Color.green, Color.green);
                }
                else
                {
                    lineRendererVertices[1] = startPos + trackedContrRight.transform.forward * 1000.0f;
                    lineRenderer.SetColors(Color.red, Color.red);
                }

                lineRendererVertices[0] = trackedContrRight.transform.position;
                lineRenderer.SetPositions(lineRendererVertices);
            }
        }

        protected override void InputSystemOnTriggerEnterHandler(OnTriggerEnterDispatcher data, Menu collider, WandLeft source)
        {
            base.InputSystemOnTriggerEnterHandler(data, collider, source);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        protected override void InputSystemMenuEventHandler(MenuEvent data, Wands group)
        {
            base.InputSystemMenuEventHandler(data, group);

            Menu.SetActive(true);

            Debug.Log("restart menu opened");


        }

        public Vector2 GetTouchpadAxis()
        {
            return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        }

        protected override void InputSystemShootEventHandler(ViveDatabase.ShootEvent data, Wands group)
        {
            if ((i % 5 == 0))
            {
                base.InputSystemShootEventHandler(data, group);

                var bullet = Instantiate(group.bullet);
                bullet.transform.position = GameObject.FindGameObjectWithTag("Weapon").transform.position;
                bullet.GetComponent<Rigidbody>().velocity = trackedContrRight.transform.forward * 100;
                Destroy(bullet, 3);

            }
            else {
                i++;
            }

        }

        protected override void InputSystemTeleportEventHandler(ViveDatabase.TeleportEvent data, Wands group)
        {
            Debug.Log("Teleport");

            base.InputSystemTeleportEventHandler(data, group);
            // We want to move the whole [CameraRig] around when we teleport,
            // which should be the parent of this controller. If we can't find the
            // [CameraRig], we can't teleport, so return.
            if (trackedContrRight.transform.parent == null)
                return;

            RaycastHit hit;
            Vector3 startPos = trackedContrRight.transform.position;

            // Perform a raycast starting from the controller's position and going 1000 meters
            // out in the forward direction of the controller to see if we hit something to teleport to.
            if (Physics.Raycast(startPos, trackedContrRight.transform.forward, out hit, 1000.0f))
            {
                trackedContrRight.transform.parent.position = hit.point;
            }
        }
    }
}
