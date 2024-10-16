using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.Items;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Turn-based/Data/Item SO")]
public class ItemSO : ScriptableObject
{
    [ShowInInspector]
    public ItemName item;
    public Sprite   image;

    public bool IsCustom = false;
    [ShowIfGroup("IsCustom")]
    [BoxGroup("IsCustom/Custom properties")]
    public string displayName;
    [ShowIfGroup("IsCustom")]
    [BoxGroup("IsCustom/Custom properties")]
    public uint cost;
    [ShowIfGroup("IsCustom")]
    [BoxGroup("IsCustom/Custom properties")]
    [TextArea(1, 5)]
    public string description;

}
