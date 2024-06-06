using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public ItemType type;
    public ItemType_Category typeCategory;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    public string itemName;
    public int dropChance;
    public int hpRestoreAmount;
    public EquipSlotType equipSlot;

    public enum EquipSlotType { None, Head, Body, Weapon }
    public enum ItemType { None, Tool, Consumable, Weapon, Key, Armor }
    public enum ItemType_Category { None, HpPotion, MpPotion, Sword, WoodenChest_Key, SilverChest_Key, MOBChest_Key, GoldenChest_Key, BAMChest_Key, HeadArmor, BodyArmor }
    public enum ActionType { None, Dig, Mine, Cut, Use, Consume, Attack, PutOn }
}