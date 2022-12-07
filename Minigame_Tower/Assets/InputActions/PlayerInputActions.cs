//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/InputActions/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Test"",
            ""id"": ""b56cbf4e-965b-46de-b0d3-16922f1f7947"",
            ""actions"": [
                {
                    ""name"": ""Test1"",
                    ""type"": ""Button"",
                    ""id"": ""7ab5a825-5304-44c5-bdc8-d2d206ff79c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test2"",
                    ""type"": ""Button"",
                    ""id"": ""0d60b2f4-bd6c-454a-a2b2-e2020da5ed14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test3"",
                    ""type"": ""Button"",
                    ""id"": ""fad09a48-ca49-48ff-be33-ffa35a1be80a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test4"",
                    ""type"": ""Button"",
                    ""id"": ""1591a29e-30e2-46ee-862b-949221298f68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test5"",
                    ""type"": ""Button"",
                    ""id"": ""fa56da9a-8ece-4f2a-924a-9acd9ebbddd6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f77b4e78-a4ff-48c5-b336-4b1b889380fc"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM"",
                    ""action"": ""Test1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0d3f0c8-0909-4321-b75c-c5e515e757f2"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM"",
                    ""action"": ""Test2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c616c820-13d8-4cbf-b9cf-60c9c04c85a0"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM"",
                    ""action"": ""Test3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc3582c9-5b4d-4ae2-896a-18e6aa9316a4"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM"",
                    ""action"": ""Test4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bad72287-1d4d-477b-ac94-18de129dfdc1"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM"",
                    ""action"": ""Test5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Input"",
            ""id"": ""afee02c1-d98a-4449-87a5-71261a0a8d92"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""6aa39ece-e77d-4531-9f41-3d41e86e900f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pos"",
                    ""type"": ""Value"",
                    ""id"": ""b7cc467d-293b-4a6c-a439-ac778ed5cff9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e411a22c-8d20-42a2-8a1c-0396f278c9ff"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3316bc7b-e06f-4789-a531-ed16b31ae058"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_Test1 = m_Test.FindAction("Test1", throwIfNotFound: true);
        m_Test_Test2 = m_Test.FindAction("Test2", throwIfNotFound: true);
        m_Test_Test3 = m_Test.FindAction("Test3", throwIfNotFound: true);
        m_Test_Test4 = m_Test.FindAction("Test4", throwIfNotFound: true);
        m_Test_Test5 = m_Test.FindAction("Test5", throwIfNotFound: true);
        // Input
        m_Input = asset.FindActionMap("Input", throwIfNotFound: true);
        m_Input_Click = m_Input.FindAction("Click", throwIfNotFound: true);
        m_Input_Pos = m_Input.FindAction("Pos", throwIfNotFound: true);
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

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_Test1;
    private readonly InputAction m_Test_Test2;
    private readonly InputAction m_Test_Test3;
    private readonly InputAction m_Test_Test4;
    private readonly InputAction m_Test_Test5;
    public struct TestActions
    {
        private @PlayerInputActions m_Wrapper;
        public TestActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test1 => m_Wrapper.m_Test_Test1;
        public InputAction @Test2 => m_Wrapper.m_Test_Test2;
        public InputAction @Test3 => m_Wrapper.m_Test_Test3;
        public InputAction @Test4 => m_Wrapper.m_Test_Test4;
        public InputAction @Test5 => m_Wrapper.m_Test_Test5;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @Test1.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest1;
                @Test1.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest1;
                @Test1.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest1;
                @Test2.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest2;
                @Test2.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest2;
                @Test2.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest2;
                @Test3.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest3;
                @Test3.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest3;
                @Test3.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest3;
                @Test4.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest4;
                @Test4.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest4;
                @Test4.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest4;
                @Test5.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest5;
                @Test5.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest5;
                @Test5.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest5;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Test1.started += instance.OnTest1;
                @Test1.performed += instance.OnTest1;
                @Test1.canceled += instance.OnTest1;
                @Test2.started += instance.OnTest2;
                @Test2.performed += instance.OnTest2;
                @Test2.canceled += instance.OnTest2;
                @Test3.started += instance.OnTest3;
                @Test3.performed += instance.OnTest3;
                @Test3.canceled += instance.OnTest3;
                @Test4.started += instance.OnTest4;
                @Test4.performed += instance.OnTest4;
                @Test4.canceled += instance.OnTest4;
                @Test5.started += instance.OnTest5;
                @Test5.performed += instance.OnTest5;
                @Test5.canceled += instance.OnTest5;
            }
        }
    }
    public TestActions @Test => new TestActions(this);

    // Input
    private readonly InputActionMap m_Input;
    private IInputActions m_InputActionsCallbackInterface;
    private readonly InputAction m_Input_Click;
    private readonly InputAction m_Input_Pos;
    public struct InputActions
    {
        private @PlayerInputActions m_Wrapper;
        public InputActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Input_Click;
        public InputAction @Pos => m_Wrapper.m_Input_Pos;
        public InputActionMap Get() { return m_Wrapper.m_Input; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputActions set) { return set.Get(); }
        public void SetCallbacks(IInputActions instance)
        {
            if (m_Wrapper.m_InputActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_InputActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnClick;
                @Pos.started -= m_Wrapper.m_InputActionsCallbackInterface.OnPos;
                @Pos.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnPos;
                @Pos.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnPos;
            }
            m_Wrapper.m_InputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Pos.started += instance.OnPos;
                @Pos.performed += instance.OnPos;
                @Pos.canceled += instance.OnPos;
            }
        }
    }
    public InputActions @Input => new InputActions(this);
    public interface ITestActions
    {
        void OnTest1(InputAction.CallbackContext context);
        void OnTest2(InputAction.CallbackContext context);
        void OnTest3(InputAction.CallbackContext context);
        void OnTest4(InputAction.CallbackContext context);
        void OnTest5(InputAction.CallbackContext context);
    }
    public interface IInputActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnPos(InputAction.CallbackContext context);
    }
}
