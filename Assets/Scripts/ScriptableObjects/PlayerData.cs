using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    //player movement speeds and threshholds
    [Header("Player Movement Speeds")]
    public float moveSpeed;
    public float holdSwordWalkSpeed;
    public float moveThreshhold;
    public float recoveryVelocity;
    public float sideJumpVelocity;
    public float sideJumpAngle;
    public float lungeAttackVelocity;
    public float jumpAttackVelocity;

    //action durations in seconds
    [Header("Player Action Durations in Seconds")]
    public float attackDuration;
    public float recoveryDuration;
    public float recoveryCooldownDuration;
    public float spinAttackDuration;
    public float sideJumpDuration;
    public float sideJumpCooldownDuration;
    public float chargeSwordDuration;
    public float lungeAttackDuration;
    public float jumpAttackPrepTime;
    public float jumpAttackDuration;
    public float jumpAttackCooldown;
}
