using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

namespace IFramework.Test.InputSystem
{
    public class MyInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }

        public MyInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInput"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""4dcdd2f3-19fd-4bbe-a906-30779cff3eb6"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3c9af8d9-3554-48dc-b3b8-5c9505f19441"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2ee1b70d-7810-4ea6-a4b3-f1cd7f313cfe"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e90e3c8a-32c2-4122-ba06-d8fc883ef801"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyborad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WSAD"",
                    ""id"": ""9cb34f71-5682-442d-b8f2-19a13b42cd5b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9115fd38-d540-4694-b7a5-0a58b8d69d45"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyborad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9da9748f-0369-41eb-9535-1f8a5e3f01b9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyborad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f2e356e1-7588-48b9-a27a-9df6ed628dc0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyborad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8b38405b-dc17-496f-839d-3f0ccdd494ce"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyborad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyborad"",
            ""bindingGroup"": ""Keyborad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
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
            // GamePlay
            m_GamePlay = asset.FindActionMap("GamePlay", true);
            m_GamePlay_Jump = m_GamePlay.FindAction("Jump", true);
            m_GamePlay_Move = m_GamePlay.FindAction("Move", true);
        }

        public void Dispose()
        {
            Object.Destroy(asset);
        }

        public InputBinding? bindingMask { get => asset.bindingMask; set => asset.bindingMask = value; }

        public ReadOnlyArray<InputDevice>? devices { get => asset.devices; set => asset.devices = value; }

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

        // GamePlay
        private readonly InputActionMap m_GamePlay;
        private IGamePlayActions m_GamePlayActionsCallbackInterface;
        private readonly InputAction m_GamePlay_Jump;
        private readonly InputAction m_GamePlay_Move;

        public struct GamePlayActions
        {
            private readonly MyInput m_Wrapper;

            public GamePlayActions(MyInput wrapper)
            {
                m_Wrapper = wrapper;
            }

            public InputAction Jump => m_Wrapper.m_GamePlay_Jump;
            public InputAction Move => m_Wrapper.m_GamePlay_Move;

            public InputActionMap Get()
            {
                return m_Wrapper.m_GamePlay;
            }

            public void Enable()
            {
                Get().Enable();
            }

            public void Disable()
            {
                Get().Disable();
            }

            public bool enabled => Get().enabled;

            public static implicit operator InputActionMap(GamePlayActions set)
            {
                return set.Get();
            }

            public void SetCallbacks(IGamePlayActions instance)
            {
                if (m_Wrapper.m_GamePlayActionsCallbackInterface != null) {
                    Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                    Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                    Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                    Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
                if (instance != null) {
                    Jump.started += instance.OnJump;
                    Jump.performed += instance.OnJump;
                    Jump.canceled += instance.OnJump;
                    Move.started += instance.OnMove;
                    Move.performed += instance.OnMove;
                    Move.canceled += instance.OnMove;
                }
            }
        }

        public GamePlayActions GamePlay => new GamePlayActions(this);
        private int m_KeyboradSchemeIndex = -1;

        public InputControlScheme KeyboradScheme {
            get {
                if (m_KeyboradSchemeIndex == -1) m_KeyboradSchemeIndex = asset.FindControlSchemeIndex("Keyborad");
                return asset.controlSchemes[m_KeyboradSchemeIndex];
            }
        }

        private int m_XboxSchemeIndex = -1;

        public InputControlScheme XboxScheme {
            get {
                if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
                return asset.controlSchemes[m_XboxSchemeIndex];
            }
        }

        public interface IGamePlayActions
        {
            void OnJump(InputAction.CallbackContext context);

            void OnMove(InputAction.CallbackContext context);
        }
    }
}
