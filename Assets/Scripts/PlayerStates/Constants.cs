using UnityEngine;

public static class Constants
{
    //input axis, button and animation parameter names
    public const string LeftStickHorizontal = "Left_Stick_Horizontal";
    public const string LeftStickVertical = "Left_Stick_Vertical";
    public const string BButton = "B_Button";
    public const string ZLTrigger = "ZL_Trigger";
    public const string animatorXParameter = "X";
    public const string animatorYParameter = "Y";

    //player movement speeds and threshholds
    public const float moveSpeed = 5f;
    public const float holdSwordWalkSpeed = 2f;
    public const float moveThreshhold = 0.33f;
    public const float recoveryVelocity = 7f;
    public const float sideJumpVelocity = 10f;
    public const float sideJumpAngle = Mathf.PI / 4 + 0.01f;
    public const float lungeAttackVelocity = 12f;
    public const float jumpAttackVelocity = 18f;

    //action durations in seconds
    public const float attackDuration = 0.3f;
    public const float recoveryDuration = 0.4f;
    public const float recoveryCooldownDuration = 0.2f;
    public const float spinAttackDuration = 0.6f;
    public const float sideJumpDuration = 0.2f;
    public const float sideJumpCooldownDuration = 0.05f;
    public const float chargeSwordDuration = 0.5f;
    public const float lungeAttackDuration = 0.3f;
    public const float jumpAttackPrepTime = 0.1f;
    public const float jumpAttackDuration = 0.2f;
    public const float jumpAttackCooldown = 0.4f;
}