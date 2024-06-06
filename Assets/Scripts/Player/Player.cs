using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HP_bar healthBar;

    public int maxMana = 80;
    public int currentMana;
    public MP_bar manaBar;

    public int maxEnergy = 100;
    public int currentEnergy;
    public EN_bar energyBar;

    public ParticleSystem deathParticles;

    private Item item;

    public Transform Target { get; set; }

    public Animator animator;
    private AudioManager audioManager;

    // Animator Controllers
    public RuntimeAnimatorController baseAnimatorController;
    public RuntimeAnimatorController helmetAnimatorController;
    public RuntimeAnimatorController bodyArmorAnimatorController;
    public RuntimeAnimatorController fullGearAnimatorController;

    public bool hasHelmet = false;
    public bool hasBodyArmor = false;
    private bool isTeleporting = false;

    public GameOverSceen gameOverScreen;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);

        UpdateAnimator();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftAlt))
        // {
        //     WasteMana(5);
        // }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void WasteMana(int waste)
    {
        currentMana -= waste;
        manaBar.SetMana(currentMana);
    }

    public void WasteEnergy(int waste)
    {
        currentEnergy -= waste;
        energyBar.SetEnergy(currentEnergy);
    }

    private bool InLineOfSight()
    {
        Vector3 targetDirection = (Target.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, Target.transform.position), 64);

        if (hit.collider == null)
        {
            return true;
        }

        return false;
    }

    public void EquipHelmet(bool equip)
    {
        hasHelmet = equip;
        UpdateAnimator();
    }

    public void EquipBodyArmor(bool equip)
    {
        hasBodyArmor = equip;
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        if (hasHelmet && hasBodyArmor)
        {
            animator.runtimeAnimatorController = fullGearAnimatorController;
        }
        else if (hasHelmet)
        {
            animator.runtimeAnimatorController = helmetAnimatorController;
        }
        else if (hasBodyArmor)
        {
            animator.runtimeAnimatorController = bodyArmorAnimatorController;
        }
        else
        {
            animator.runtimeAnimatorController = baseAnimatorController;
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");

        // Instantiate and play the particle system at the enemy's position and rotation
        ParticleSystem particles = Instantiate(deathParticles, transform.position, transform.rotation);
        particles.Play();

        // Start the coroutine to destroy the particles after their duration
        StartCoroutine(DestroyParticlesAfterDuration(particles));
        audioManager.PlayAudio(audioManager.playerDeath);

        Destroy(gameObject);

        gameOverScreen.Setup();

    }

    private IEnumerator DestroyParticlesAfterDuration(ParticleSystem particles)
    {
        yield return new WaitForSeconds(particles.main.duration);
        Destroy(particles.gameObject);
    }

    public void UseConsumable(Item item)
    {
        Debug.Log("Using consumable: " + item.itemName);
        if (item.type == Item.ItemType.Consumable && item.typeCategory == Item.ItemType_Category.HpPotion)
        {
            Debug.Log("Item is a consumable HP potion.");

            RegenerateHealth(item.hpRestoreAmount);
            InventoryManager.instance.RemoveHpPotion();
        }
        else
        {
            Debug.Log("Item is not a consumable HP potion.");
        }
    }

    void RegenerateHealth(int amount)
    {
        Debug.Log("Regenerating health: " + amount);
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public bool HasHelmet()
    {
        return hasHelmet;
    }

    public bool HasBodyArmor()
    {
        return hasBodyArmor;
    }

    public void StartTeleportation(GameObject endScreenGameObject, GameObject exitButtonGameObject)
    {
        if (isTeleporting) return;

        isTeleporting = true;
        animator.SetBool("Teleport", true);
        StartCoroutine(TeleportRoutine(endScreenGameObject, exitButtonGameObject));
    }

    private IEnumerator TeleportRoutine(GameObject endScreenGameObject, GameObject exitButtonGameObject)
    {
        // Wait until the teleport animation has finished
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Teleport_spawn_armed") &&
                                         animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        // Enable and fade in the end screen image
        Image endScreenImage = endScreenGameObject.GetComponent<Image>();
        if (endScreenImage != null)
        {
            endScreenImage.gameObject.SetActive(true);
            CanvasGroup canvasGroup = endScreenImage.GetComponent<CanvasGroup>();
            float fadeDuration = 1f;
            float fadeSpeed = 1f / fadeDuration;

            for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
            {
                canvasGroup.alpha = t;
                yield return null;
            }

            canvasGroup.alpha = 1;
        }
        else
        {
            Debug.LogWarning("EndScreen GameObject does not have an Image component attached.");
        }

        yield return new WaitForSeconds(2f);

        Button exitButton = exitButtonGameObject.GetComponent<Button>();
        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(true);
            CanvasGroup canvasGroup = exitButton.GetComponent<CanvasGroup>();
            float fadeDuration = 1f;
            float fadeSpeed = 1f / fadeDuration;

            for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
            {
                canvasGroup.alpha = t;
                yield return null;
            }

            canvasGroup.alpha = 1;
        }
        else
        {
            Debug.LogWarning("ExitButton GameObject does not have a Button component attached.");
        }
        Destroy(gameObject);
    }
}