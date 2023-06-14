//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/Player/ControlSchemes/PlayerInputsMobile.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputsMobile: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputsMobile()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputsMobile"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""f83acbf6-b784-4b5a-973b-eb55b43449b0"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f97bb95c-fd6b-43a0-9f3c-e55e71cec665"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""dbac44ab-e8b4-48a6-8a9a-6a6cdd3f4d0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""5924818b-c7b1-4a77-afdb-4fd2756c8312"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6a91f6ee-14f3-4fc1-86c9-5325bd5df9ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a4248b3a-85c7-4f66-a16e-bef436212dd5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""b01b3172-bce2-4877-9ad1-85447c258436"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HookLength"",
                    ""type"": ""Value"",
                    ""id"": ""9d768043-8f29-4d43-b5ed-8cb612a39b2c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""Button"",
                    ""id"": ""ca356d87-392a-4ece-9f6a-ace3acc8e120"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f485819-1328-45de-ac92-0c074ecb7562"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a876dd94-616b-4bbc-b226-921f123dfeb9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e3d7fe6-e26c-4410-acdc-81b623c1a66e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Hold,Press"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16792cb6-1d87-41d3-a14b-774f184f85e8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d9e01e5a-ba6c-460a-b473-27e9f9e0425c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f3396237-4762-4005-b3c7-881526e24841"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""418081ca-71e7-43e0-b6d0-6817dae2354d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fa535b6b-db23-4997-8528-07f707a8654e"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""33f1833d-54f5-4b4d-9ca5-f4e798f80bbe"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7fa5e4df-4c1c-49d8-9ae9-3ddf154ccf6a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4eb77bdc-6158-4790-b04f-dde7aa99abfb"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""de0949f2-e852-438a-81e7-5120aefbc486"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d561284d-67af-414f-8e65-b1537d300a23"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HookLength"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e39aabac-4841-4eb5-962a-5bd0315f445a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HookLength"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""500ddbcd-3bfc-4fb2-955d-4c2dc4f9cbda"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""HookLength"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""36a292f8-eec8-4306-8eb1-a7b75dc30e22"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""HookLength"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7fb8e137-5a06-40d2-a045-fd2cb095711d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HookLength"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""62118573-05b7-4017-b021-8c49ee31ff0b"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""HookLength"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cbacbb91-66be-4e78-a305-921a9f30a96f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""HookLength"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""74bcfcd3-00e5-4996-8604-aba2d7bc3580"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""d12240f1-2b29-4f27-9fd9-569543d5d8f8"",
            ""actions"": [
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""d7a3c790-5a2d-4c13-8570-b175023baf12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkipHologram"",
                    ""type"": ""Button"",
                    ""id"": ""7ce9e255-7e53-4579-8e4a-95eb3c687f70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right Holograms"",
                    ""type"": ""Button"",
                    ""id"": ""f252440f-e3e2-42a7-b2f4-c13119aef5f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left Hologram"",
                    ""type"": ""Button"",
                    ""id"": ""81f554bb-8987-4b8c-91d2-3af912cf2c70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""63998744-b046-4dbb-8b71-2f56e0303714"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dba1898d-d3cb-480f-b009-d70bb4fabdcd"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ab8e59b-18e4-4b96-8cf1-13ae7da306a3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d40a104-e9c9-4446-a9b0-ae1bfc860550"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bb81251-8eb3-4549-92ac-36f605fa664e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""SkipHologram"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b122ef20-fc0f-4659-8ea0-db9bb79384d7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipHologram"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dee1d6d7-3d63-4eb9-8d98-934eb016ebbd"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Right Holograms"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26b030ee-043d-412c-b936-af90be929615"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Left Hologram"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Jump = m_Game.FindAction("Jump", throwIfNotFound: true);
        m_Game_Fire = m_Game.FindAction("Fire", throwIfNotFound: true);
        m_Game_Interact = m_Game.FindAction("Interact", throwIfNotFound: true);
        m_Game_Pause = m_Game.FindAction("Pause", throwIfNotFound: true);
        m_Game_Move = m_Game.FindAction("Move", throwIfNotFound: true);
        m_Game_Aim = m_Game.FindAction("Aim", throwIfNotFound: true);
        m_Game_HookLength = m_Game.FindAction("HookLength", throwIfNotFound: true);
        m_Game_Sneak = m_Game.FindAction("Sneak", throwIfNotFound: true);
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_Skip = m_Dialogue.FindAction("Skip", throwIfNotFound: true);
        m_Dialogue_SkipHologram = m_Dialogue.FindAction("SkipHologram", throwIfNotFound: true);
        m_Dialogue_RightHolograms = m_Dialogue.FindAction("Right Holograms", throwIfNotFound: true);
        m_Dialogue_LeftHologram = m_Dialogue.FindAction("Left Hologram", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Game
    private readonly InputActionMap m_Game;
    private List<IGameActions> m_GameActionsCallbackInterfaces = new List<IGameActions>();
    private readonly InputAction m_Game_Jump;
    private readonly InputAction m_Game_Fire;
    private readonly InputAction m_Game_Interact;
    private readonly InputAction m_Game_Pause;
    private readonly InputAction m_Game_Move;
    private readonly InputAction m_Game_Aim;
    private readonly InputAction m_Game_HookLength;
    private readonly InputAction m_Game_Sneak;
    public struct GameActions
    {
        private @PlayerInputsMobile m_Wrapper;
        public GameActions(@PlayerInputsMobile wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Game_Jump;
        public InputAction @Fire => m_Wrapper.m_Game_Fire;
        public InputAction @Interact => m_Wrapper.m_Game_Interact;
        public InputAction @Pause => m_Wrapper.m_Game_Pause;
        public InputAction @Move => m_Wrapper.m_Game_Move;
        public InputAction @Aim => m_Wrapper.m_Game_Aim;
        public InputAction @HookLength => m_Wrapper.m_Game_HookLength;
        public InputAction @Sneak => m_Wrapper.m_Game_Sneak;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void AddCallbacks(IGameActions instance)
        {
            if (instance == null || m_Wrapper.m_GameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @HookLength.started += instance.OnHookLength;
            @HookLength.performed += instance.OnHookLength;
            @HookLength.canceled += instance.OnHookLength;
            @Sneak.started += instance.OnSneak;
            @Sneak.performed += instance.OnSneak;
            @Sneak.canceled += instance.OnSneak;
        }

        private void UnregisterCallbacks(IGameActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @HookLength.started -= instance.OnHookLength;
            @HookLength.performed -= instance.OnHookLength;
            @HookLength.canceled -= instance.OnHookLength;
            @Sneak.started -= instance.OnSneak;
            @Sneak.performed -= instance.OnSneak;
            @Sneak.canceled -= instance.OnSneak;
        }

        public void RemoveCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameActions instance)
        {
            foreach (var item in m_Wrapper.m_GameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameActions @Game => new GameActions(this);

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private List<IDialogueActions> m_DialogueActionsCallbackInterfaces = new List<IDialogueActions>();
    private readonly InputAction m_Dialogue_Skip;
    private readonly InputAction m_Dialogue_SkipHologram;
    private readonly InputAction m_Dialogue_RightHolograms;
    private readonly InputAction m_Dialogue_LeftHologram;
    public struct DialogueActions
    {
        private @PlayerInputsMobile m_Wrapper;
        public DialogueActions(@PlayerInputsMobile wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skip => m_Wrapper.m_Dialogue_Skip;
        public InputAction @SkipHologram => m_Wrapper.m_Dialogue_SkipHologram;
        public InputAction @RightHolograms => m_Wrapper.m_Dialogue_RightHolograms;
        public InputAction @LeftHologram => m_Wrapper.m_Dialogue_LeftHologram;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void AddCallbacks(IDialogueActions instance)
        {
            if (instance == null || m_Wrapper.m_DialogueActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DialogueActionsCallbackInterfaces.Add(instance);
            @Skip.started += instance.OnSkip;
            @Skip.performed += instance.OnSkip;
            @Skip.canceled += instance.OnSkip;
            @SkipHologram.started += instance.OnSkipHologram;
            @SkipHologram.performed += instance.OnSkipHologram;
            @SkipHologram.canceled += instance.OnSkipHologram;
            @RightHolograms.started += instance.OnRightHolograms;
            @RightHolograms.performed += instance.OnRightHolograms;
            @RightHolograms.canceled += instance.OnRightHolograms;
            @LeftHologram.started += instance.OnLeftHologram;
            @LeftHologram.performed += instance.OnLeftHologram;
            @LeftHologram.canceled += instance.OnLeftHologram;
        }

        private void UnregisterCallbacks(IDialogueActions instance)
        {
            @Skip.started -= instance.OnSkip;
            @Skip.performed -= instance.OnSkip;
            @Skip.canceled -= instance.OnSkip;
            @SkipHologram.started -= instance.OnSkipHologram;
            @SkipHologram.performed -= instance.OnSkipHologram;
            @SkipHologram.canceled -= instance.OnSkipHologram;
            @RightHolograms.started -= instance.OnRightHolograms;
            @RightHolograms.performed -= instance.OnRightHolograms;
            @RightHolograms.canceled -= instance.OnRightHolograms;
            @LeftHologram.started -= instance.OnLeftHologram;
            @LeftHologram.performed -= instance.OnLeftHologram;
            @LeftHologram.canceled -= instance.OnLeftHologram;
        }

        public void RemoveCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDialogueActions instance)
        {
            foreach (var item in m_Wrapper.m_DialogueActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DialogueActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnHookLength(InputAction.CallbackContext context);
        void OnSneak(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnSkip(InputAction.CallbackContext context);
        void OnSkipHologram(InputAction.CallbackContext context);
        void OnRightHolograms(InputAction.CallbackContext context);
        void OnLeftHologram(InputAction.CallbackContext context);
    }
}
