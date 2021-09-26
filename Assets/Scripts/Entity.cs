//base entity script for both player and enemies to use

using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int attack;

    protected Rigidbody2D rb;
    protected int currentHealth;
    protected const string entityTag = "Entity";

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(entityTag))
        {
            StartCoroutine(EntityCollision());
        }
    }

    //when colliding with another entity, freeze constraints for one frame then carry on
    protected virtual IEnumerator EntityCollision()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return null;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
