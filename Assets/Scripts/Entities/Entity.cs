//base entity script for both player and enemies to use

using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityData data;
    [SerializeField] private Collider2D hitbox;

    protected Rigidbody2D rb;
    protected uint currentHealth;
    protected bool isInvulnerable;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = data.maxHealth;
        isInvulnerable = false;
    }

    public void TakeHit(Vector3 attackerPos, uint damage)
    {
        //cant get hit if invulnerable
        if (isInvulnerable) { return; }

        //Debug.Log("ouch");
        StartCoroutine(Knockback(transform.position - attackerPos));
    }

    protected virtual IEnumerator Knockback(Vector2 direction)
    {
        //knock back
        rb.velocity = direction.normalized * data.knockbackVelocity;

        //dont take more damage
        isInvulnerable = true;
        hitbox.enabled = false;

        yield return new WaitForSeconds(data.knockbackDuration);

        //stop for a moment
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(data.knockbackCooldown);
        
        //continue
        StartCoroutine(Invulnerability());
    }

    //the long awaited sequel to Knockback
    private IEnumerator Invulnerability()
    {
        float period = Mathf.Max(data.invulnerabilityPeriod - data.knockbackDuration - data.knockbackCooldown, 0);
        yield return new WaitForSeconds(period);
        isInvulnerable = false;
        hitbox.enabled = true;
    }
}
