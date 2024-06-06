using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject[] menuButtons;
    private int currentButtonIndex = 0;
    private bool isAxisInUse = false;

    void Start()
    {
        // Select the first button in the array
        if (menuButtons.Length > 0)
        {
            eventSystem.SetSelectedGameObject(menuButtons[0]);
        }
    }

    void Update()
    {
        NavigateMenu();
        ActivateButton();
    }

    private void NavigateMenu()
    {
        float verticalInput = Gamepad.current.leftStick.y.ReadValue();

        if (Mathf.Abs(verticalInput) > 0.5f && !isAxisInUse)
        {
            if (verticalInput > 0)
            {
                currentButtonIndex = (currentButtonIndex > 0) ? currentButtonIndex - 1 : menuButtons.Length - 1;
            }
            else if (verticalInput < 0)
            {
                currentButtonIndex = (currentButtonIndex < menuButtons.Length - 1) ? currentButtonIndex + 1 : 0;
            }

            eventSystem.SetSelectedGameObject(menuButtons[currentButtonIndex]);
            isAxisInUse = true;
        }

        if (Mathf.Abs(verticalInput) < 0.5f)
        {
            isAxisInUse = false;
        }
    }

    private void ActivateButton()
    {
        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            var selectedButton = eventSystem.currentSelectedGameObject;
            if (selectedButton != null)
            {
                var button = selectedButton.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
