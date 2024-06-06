using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private AudioManager audioManager;
    public Transform attackPoint;
    public float attackRate;
    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayer;
    private float nextAttackTime = 0f;
    private float lastAttackTime = 0f;
    public int energyCostPerAttack;
    public int energyRegenRate;
    public float energyRegenDelay;
    public float energyRegenInterval = 1f;
    private Player player;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        player = GetComponent<Player>();
        inventoryManager = InventoryManager.instance;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame && !IsPlayerNearUIObject())
            {
                if (inventoryManager.IsWeaponEquipped() && player.currentEnergy >= energyCostPerAttack)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else if (!inventoryManager.IsWeaponEquipped())
                {
                    Debug.Log("No weapon equipped");
                }
                else
                {
                    Debug.Log("Not enough energy to attack");
                }
            }
        }
        if (Time.time - lastAttackTime > energyRegenDelay)
        {
            RegenerateEnergy();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        audioManager.PlayAudio(audioManager.swordSwing);

        player.WasteEnergy(energyCostPerAttack);

        lastAttackTime = Time.time; // Update the last attack time

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_Combat>().TakeDamage(attackDamage);
        }
    }

    void RegenerateEnergy()
    {
        if (Time.time - lastAttackTime >= energyRegenDelay)
        {
            int totalRegeneratedEnergy = energyRegenRate;
            player.currentEnergy += totalRegeneratedEnergy;
            player.currentEnergy = Mathf.Min(player.currentEnergy, player.maxEnergy);
            player.energyBar.SetEnergy(player.currentEnergy);
            lastAttackTime = Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private bool IsPlayerNearUIObject()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("UI"))
            {
                return true;
            }
        }
        return false;
    }
}
