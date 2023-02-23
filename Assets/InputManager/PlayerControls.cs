// GENERATED AUTOMATICALLY FROM 'Assets/InputManager/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""77f5d4d9-e0d1-4f37-a96c-35954668e0a3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c0cb73a0-714c-4bcb-a0cd-2b5999e26326"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""76aced7b-b786-4d0b-aebb-b7141251edb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""c115a401-e31a-49e0-9594-62e367d770b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""7701c58e-0db9-4347-81af-f79487667eb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""5589e008-f325-4e5a-a13c-5aa89d0cd501"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6ac0011d-dd60-453d-b7ba-a3ff58aef779"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ef1498aa-2ba3-4e4a-8ed1-3cc45a623693"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5ef5e69e-71bf-4b10-8700-53eb867c46ed"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0a6f2c21-1ac0-4e8d-8d72-79f85dad7e0a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""39a4800f-f5fa-4e4e-bc02-17aef7ba6133"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b63e3a1-4cd5-4f61-af84-fc07aae15c1e"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16b1c394-e541-4f45-ae24-9fa8934ffcbf"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraMovement"",
            ""id"": ""aabbcee2-68ba-4562-8040-454f66159bf1"",
            ""actions"": [
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a43d8bda-216d-4313-9098-80a868d05828"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""866930cd-83bb-4558-9dfb-3ef89b7289bf"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerAction"",
            ""id"": ""82977629-9940-4234-982c-79e95ffbec8d"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""38aafe6e-3236-43a9-84de-a59d3c7a1cfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpArrow"",
                    ""type"": ""Button"",
                    ""id"": ""682499c1-b6bc-492e-9f1e-dabdd5295c11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownArrow"",
                    ""type"": ""Button"",
                    ""id"": ""f960930c-0f3f-4199-929c-38d184d03173"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left Arrow"",
                    ""type"": ""Button"",
                    ""id"": ""f8150e3f-5508-4f03-afa7-c5dd9065f326"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Arrow"",
                    ""type"": ""Button"",
                    ""id"": ""08743508-8bc6-48b4-99a6-85f700d807db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""dcbdc424-eed8-4082-81b2-76e71b721987"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f9e32bee-6f05-4a70-8bcb-648af57ca3fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""becb5885-9a9d-4cca-aaf5-7435a3eba32c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""acde32ce-cdd1-4d63-bd48-84534545eaa1"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1775405f-c545-4f7f-aced-27cfd7942a02"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpArrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83b0643e-c855-4b12-84eb-8825bc7d8285"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownArrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21d19740-e2b6-47fa-8722-66ed7fd6cdfd"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ab885b-f4a2-4858-82c9-2fdd8056f020"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3450f834-0532-4ad6-9721-432d5e144e41"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bff30e50-812e-41f8-9d19-0e657619f671"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff28100f-c254-49a0-ad11-0efefd1029eb"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SystemAction"",
            ""id"": ""1984058f-5f02-4c8b-a3e2-3aa5d6af831e"",
            ""actions"": [
                {
                    ""name"": ""ToggleSelectMeunu"",
                    ""type"": ""Button"",
                    ""id"": ""8bc7ff5f-27f3-414c-a2a2-98d33b9a655d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7a121579-b7eb-4d01-a80e-064e525f3b4a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeybroadAndMouse"",
                    ""action"": ""ToggleSelectMeunu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeybroadAndMouse"",
            ""bindingGroup"": ""KeybroadAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Sprint = m_PlayerMovement.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerMovement_Walk = m_PlayerMovement.FindAction("Walk", throwIfNotFound: true);
        m_PlayerMovement_Roll = m_PlayerMovement.FindAction("Roll", throwIfNotFound: true);
        // CameraMovement
        m_CameraMovement = asset.FindActionMap("CameraMovement", throwIfNotFound: true);
        m_CameraMovement_Rotation = m_CameraMovement.FindAction("Rotation", throwIfNotFound: true);
        // PlayerAction
        m_PlayerAction = asset.FindActionMap("PlayerAction", throwIfNotFound: true);
        m_PlayerAction_LeftClick = m_PlayerAction.FindAction("LeftClick", throwIfNotFound: true);
        m_PlayerAction_UpArrow = m_PlayerAction.FindAction("UpArrow", throwIfNotFound: true);
        m_PlayerAction_DownArrow = m_PlayerAction.FindAction("DownArrow", throwIfNotFound: true);
        m_PlayerAction_LeftArrow = m_PlayerAction.FindAction("Left Arrow", throwIfNotFound: true);
        m_PlayerAction_RightArrow = m_PlayerAction.FindAction("Right Arrow", throwIfNotFound: true);
        m_PlayerAction_Interact = m_PlayerAction.FindAction("Interact", throwIfNotFound: true);
        m_PlayerAction_Jump = m_PlayerAction.FindAction("Jump", throwIfNotFound: true);
        m_PlayerAction_LockOn = m_PlayerAction.FindAction("LockOn", throwIfNotFound: true);
        // SystemAction
        m_SystemAction = asset.FindActionMap("SystemAction", throwIfNotFound: true);
        m_SystemAction_ToggleSelectMeunu = m_SystemAction.FindAction("ToggleSelectMeunu", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Sprint;
    private readonly InputAction m_PlayerMovement_Walk;
    private readonly InputAction m_PlayerMovement_Roll;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Sprint => m_Wrapper.m_PlayerMovement_Sprint;
        public InputAction @Walk => m_Wrapper.m_PlayerMovement_Walk;
        public InputAction @Roll => m_Wrapper.m_PlayerMovement_Roll;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Walk.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnWalk;
                @Roll.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRoll;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // CameraMovement
    private readonly InputActionMap m_CameraMovement;
    private ICameraMovementActions m_CameraMovementActionsCallbackInterface;
    private readonly InputAction m_CameraMovement_Rotation;
    public struct CameraMovementActions
    {
        private @PlayerControls m_Wrapper;
        public CameraMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotation => m_Wrapper.m_CameraMovement_Rotation;
        public InputActionMap Get() { return m_Wrapper.m_CameraMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraMovementActions set) { return set.Get(); }
        public void SetCallbacks(ICameraMovementActions instance)
        {
            if (m_Wrapper.m_CameraMovementActionsCallbackInterface != null)
            {
                @Rotation.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnRotation;
            }
            m_Wrapper.m_CameraMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
            }
        }
    }
    public CameraMovementActions @CameraMovement => new CameraMovementActions(this);

    // PlayerAction
    private readonly InputActionMap m_PlayerAction;
    private IPlayerActionActions m_PlayerActionActionsCallbackInterface;
    private readonly InputAction m_PlayerAction_LeftClick;
    private readonly InputAction m_PlayerAction_UpArrow;
    private readonly InputAction m_PlayerAction_DownArrow;
    private readonly InputAction m_PlayerAction_LeftArrow;
    private readonly InputAction m_PlayerAction_RightArrow;
    private readonly InputAction m_PlayerAction_Interact;
    private readonly InputAction m_PlayerAction_Jump;
    private readonly InputAction m_PlayerAction_LockOn;
    public struct PlayerActionActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_PlayerAction_LeftClick;
        public InputAction @UpArrow => m_Wrapper.m_PlayerAction_UpArrow;
        public InputAction @DownArrow => m_Wrapper.m_PlayerAction_DownArrow;
        public InputAction @LeftArrow => m_Wrapper.m_PlayerAction_LeftArrow;
        public InputAction @RightArrow => m_Wrapper.m_PlayerAction_RightArrow;
        public InputAction @Interact => m_Wrapper.m_PlayerAction_Interact;
        public InputAction @Jump => m_Wrapper.m_PlayerAction_Jump;
        public InputAction @LockOn => m_Wrapper.m_PlayerAction_LockOn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionActions instance)
        {
            if (m_Wrapper.m_PlayerActionActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLeftClick;
                @UpArrow.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnUpArrow;
                @UpArrow.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnUpArrow;
                @UpArrow.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnUpArrow;
                @DownArrow.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDownArrow;
                @DownArrow.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDownArrow;
                @DownArrow.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnDownArrow;
                @LeftArrow.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLeftArrow;
                @LeftArrow.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLeftArrow;
                @LeftArrow.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLeftArrow;
                @RightArrow.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRightArrow;
                @RightArrow.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRightArrow;
                @RightArrow.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRightArrow;
                @Interact.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnJump;
                @LockOn.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnLockOn;
            }
            m_Wrapper.m_PlayerActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @UpArrow.started += instance.OnUpArrow;
                @UpArrow.performed += instance.OnUpArrow;
                @UpArrow.canceled += instance.OnUpArrow;
                @DownArrow.started += instance.OnDownArrow;
                @DownArrow.performed += instance.OnDownArrow;
                @DownArrow.canceled += instance.OnDownArrow;
                @LeftArrow.started += instance.OnLeftArrow;
                @LeftArrow.performed += instance.OnLeftArrow;
                @LeftArrow.canceled += instance.OnLeftArrow;
                @RightArrow.started += instance.OnRightArrow;
                @RightArrow.performed += instance.OnRightArrow;
                @RightArrow.canceled += instance.OnRightArrow;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
            }
        }
    }
    public PlayerActionActions @PlayerAction => new PlayerActionActions(this);

    // SystemAction
    private readonly InputActionMap m_SystemAction;
    private ISystemActionActions m_SystemActionActionsCallbackInterface;
    private readonly InputAction m_SystemAction_ToggleSelectMeunu;
    public struct SystemActionActions
    {
        private @PlayerControls m_Wrapper;
        public SystemActionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleSelectMeunu => m_Wrapper.m_SystemAction_ToggleSelectMeunu;
        public InputActionMap Get() { return m_Wrapper.m_SystemAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SystemActionActions set) { return set.Get(); }
        public void SetCallbacks(ISystemActionActions instance)
        {
            if (m_Wrapper.m_SystemActionActionsCallbackInterface != null)
            {
                @ToggleSelectMeunu.started -= m_Wrapper.m_SystemActionActionsCallbackInterface.OnToggleSelectMeunu;
                @ToggleSelectMeunu.performed -= m_Wrapper.m_SystemActionActionsCallbackInterface.OnToggleSelectMeunu;
                @ToggleSelectMeunu.canceled -= m_Wrapper.m_SystemActionActionsCallbackInterface.OnToggleSelectMeunu;
            }
            m_Wrapper.m_SystemActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleSelectMeunu.started += instance.OnToggleSelectMeunu;
                @ToggleSelectMeunu.performed += instance.OnToggleSelectMeunu;
                @ToggleSelectMeunu.canceled += instance.OnToggleSelectMeunu;
            }
        }
    }
    public SystemActionActions @SystemAction => new SystemActionActions(this);
    private int m_KeybroadAndMouseSchemeIndex = -1;
    public InputControlScheme KeybroadAndMouseScheme
    {
        get
        {
            if (m_KeybroadAndMouseSchemeIndex == -1) m_KeybroadAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeybroadAndMouse");
            return asset.controlSchemes[m_KeybroadAndMouseSchemeIndex];
        }
    }
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
    }
    public interface ICameraMovementActions
    {
        void OnRotation(InputAction.CallbackContext context);
    }
    public interface IPlayerActionActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnUpArrow(InputAction.CallbackContext context);
        void OnDownArrow(InputAction.CallbackContext context);
        void OnLeftArrow(InputAction.CallbackContext context);
        void OnRightArrow(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
    }
    public interface ISystemActionActions
    {
        void OnToggleSelectMeunu(InputAction.CallbackContext context);
    }
}
