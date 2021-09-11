using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   #region Movement_variables
   public float movespeed;
   #endregion
   #region Physics_components
   Rigidbody2D EnemyRB;
   #endregion

   #region Targeting_variables
    public Transform player;
   #endregion

    #region Attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion

    #region Health_variables
    float currHealth;
    public float maxHealth;

    #endregion

   #region Unity_functions
   private void Awake()
   {
       EnemyRB = GetComponent<Rigidbody2D>();

       currHealth = maxHealth;
   }

   private void Update()
   {
       if(player == null)
       {
           return;
       }
       Move();
   }

   #endregion

   #region Movement_functions

   private void Move()
   {
       //Calculate the movement vector
       Vector2 direction = player.position - transform.position;
       EnemyRB.velocity = direction.normalized * movespeed;
   }
   #endregion

   #region Attack_function
    private void Explode(){
        //sound
        FindObjectOfType<AudioManager>().Play("Explosion");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);

        foreach(RaycastHit2D hit in hits){
            if(hit.transform.CompareTag("Player")){
                Debug.Log("Hit Player with explosion");

                //spawn explosion Prefab
                Instantiate(explosionObj, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerControler>().TakeDamage(explosionDamage);
                Destroy(this.gameObject);
                
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
   #endregion

   #region Health_functions
    public void TakeDamage(float value)
    {
        //sound
        FindObjectOfType<AudioManager>().Play("BatHurt");
        currHealth -= value;
        Debug.Log("health is now" + currHealth.ToString());

        if (currHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
   #endregion
}
