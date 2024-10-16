using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkills", menuName = "Turn-based/Data/Skill SO")]
public class SkillSO : ScriptableObject
{
    [ShowInInspector]
    public List<Skill> skills;
}
