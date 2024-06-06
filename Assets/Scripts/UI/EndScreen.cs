using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class EndScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public EventSystem eventSystem;
    public GameObject[] menuButtons;
    private int currentButtonIndex = 0;
    private bool isAxisInUse = false;

    private InputActionMap inputActions;

    private void Start()
    {
        if (menuButtons.Length > 0)
        {
            eventSystem.SetSelectedGameObject(menuButtons[0]);
        }
        SetupInputActions();
    }

    private void SetupInputActions()
    {
        inputActions = new InputActionMap("UI");
        inputActions.AddAction("Navigate", InputActionType.PassThrough);
        inputActions.AddAction("Submit", InputActionType.Button, "<Gamepad>/buttonSouth");
        inputActions["Navigate"].AddCompositeBinding("Dpad")
            .With("Up", "<Gamepad>/dpad/up")
            .With("Down", "<Gamepad>/dpad/down");
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions["Navigate"].performed += OnNavigatePerformed;
        inputActions["Submit"].performed += OnSubmitPerformed;
    }

    private void OnDisable()
    {
        inputActions["Navigate"].performed -= OnNavigatePerformed;
        inputActions["Submit"].performed -= OnSubmitPerformed;
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    private void OnNavigatePerformed(InputAction.CallbackContext context)
    {
        float verticalInput = context.ReadValue<Vector2>().y;

        if (Mathf.Abs(verticalInput) > 0.5f && !isAxisInUse)
        {
            currentButtonIndex += (verticalInput > 0) ? -1 : 1;
            currentButtonIndex = Mathf.Clamp(currentButtonIndex, 0, menuButtons.Length - 1);

            eventSystem.SetSelectedGameObject(menuButtons[currentButtonIndex]);
            isAxisInUse = true;
        }

        if (Mathf.Abs(verticalInput) < 0.5f)
        {
            isAxisInUse = false;
        }
    }

    private void OnSubmitPerformed(InputAction.CallbackContext context)
    {
        ActivateButton();
    }

    private void ActivateButton()
    {
        var selectedButton = eventSystem.currentSelectedGameObject;
        if (selectedButton != null)
        {
            var button = selectedButton.GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                button.onClick.Invoke();
            }
        }
    }
}
