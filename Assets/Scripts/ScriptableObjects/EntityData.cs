using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    public uint maxHealth;
    public uint attack;

    public float knockbackVelocity;
    public float knockbackDuration;
    public float knockbackCooldown;
    public float invulnerabilityPeriod;
    public float moveSpeed;
}
