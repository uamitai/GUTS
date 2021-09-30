//base entity script for both player and enemies to use

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    [SerializeField] protected uint maxHealth;
    [SerializeField] protected int attack;

    [FormerlySerializedAs("thrust")]
    [SerializeField] private float knockbackVelocity;
    [SerializeField] private float knockbackDur;
    [SerializeField] private float knockbackCooldown;

    protected Rigidbody2D rb;
    protected uint currentHealth;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeHit(Vector3 attackerPos, uint damage)
    {
        //Debug.Log("ouch");
        StartCoroutine(Knockback(transform.position - attackerPos));
    }

    protected virtual IEnumerator Knockback(Vector2 direction)
    {
        //knock back
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = direction.normalized * knockbackVelocity;

        yield return new WaitForSeconds(knockbackDur);

        //stop for a moment
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(knockbackCooldown);
        
        //continue
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
