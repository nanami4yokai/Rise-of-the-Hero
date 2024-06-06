using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class VIA_Player : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public TextMeshProUGUI text;

    private Vector2 movement;
    private AudioManager audioManager;
    private Coroutine walkingCoroutine;
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;

        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("speed", movement.sqrMagnitude);
        }

        HandleWalkingSound();
    }

    private void HandleWalkingSound()
    {
        isMoving = movement.magnitude > 0;

        if (isMoving && walkingCoroutine == null)
        {
            walkingCoroutine = StartCoroutine(PlayWalkingSound());
        }
        else if (!isMoving && walkingCoroutine != null)
        {
            StopCoroutine(walkingCoroutine);
            walkingCoroutine = null;
            audioManager.StopLoopingAudio(audioManager.oneOffAudioSource);
        }
    }

    IEnumerator PlayWalkingSound()
    {
        while (true)
        {
            audioManager.PlayWalkingSound();
            yield return new WaitUntil(() => !isMoving);
            audioManager.StopLoopingAudio(audioManager.oneOffAudioSource);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnA(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "A";
        }
    }

    public void OnB(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "B";
        }
    }

    public void OnX(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "X";
        }
    }

    public void OnY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "Y";
        }
    }

    public void OnLeftTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "L Trigger";
        }
    }

    public void OnRightTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "R Trigger";
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            text.text = "Start";
        }
    }
}
