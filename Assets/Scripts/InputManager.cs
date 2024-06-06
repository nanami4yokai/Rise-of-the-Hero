using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerControls.DefaultActions playerActions;
    private VIA_Player viaPlayer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Default;
        viaPlayer = FindObjectOfType<VIA_Player>();

        if (viaPlayer == null)
        {
            Debug.LogError("VIA_Player instance not found in the scene!");
            return;
        }

        // Subscribe to input actions
        playerActions.Movement.performed += ctx => viaPlayer.OnMovement(ctx);
        playerActions.Movement.canceled += ctx => viaPlayer.OnMovement(ctx);
        playerActions.A.performed += ctx => viaPlayer.OnA(ctx);
        playerActions.B.performed += ctx => viaPlayer.OnB(ctx);
        playerActions.X.performed += ctx => viaPlayer.OnX(ctx);
        playerActions.Y.performed += ctx => viaPlayer.OnY(ctx);
        playerActions.LeftTrigger.performed += ctx => viaPlayer.OnLeftTrigger(ctx);
        playerActions.RightTrigger.performed += ctx => viaPlayer.OnRightTrigger(ctx);
        playerActions.Start.performed += ctx => viaPlayer.OnStart(ctx);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
