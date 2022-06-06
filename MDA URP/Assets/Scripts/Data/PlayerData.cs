using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [field: SerializeField] public string UserName { get; set; }
    [field: SerializeField] public string CrewName { get; set; }
    [field: SerializeField] public int UserIndexInCrew { get; set; }
    [field: SerializeField] public int CrewIndex { get; set; }
    [field: SerializeField] public Roles UserRole { get; set; }

    [field: SerializeField] public Animation PlayerAnimation;
}
