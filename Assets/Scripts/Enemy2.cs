using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
   #region Movement_variables
   public float movespeed;
   private float moveTimer;
   #endregion
   #region Physics_components
   Rigidbody2D EnemyRB;
   #endregion

    #region Attack_variables
    public float touchDamage;
    public GameObject bulletObj;
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
       if(moveTimer > 0)
       {
           moveTimer -= Time.deltaTime;
       } else {
         Move();  
        
       }
   }

   #endregion

   #region Movement_functions

   private void Move()
   {
       //to do , rotate the object, make a bullet prefab
       transform.Rotate(Vector3.forward * 45);
       //vector(1,0), (0,1),(-1,0),(0,-1)
       GameObject b = Instantiate(bulletObj, transform.position, transform.rotation);
       b.GetComponent<bullet>().shoot(transform.position);

       moveTimer = movespeed;
   }
   #endregion

   #region Attack_function
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerControler>().TakeDamage(touchDamage);
        }
    }
   #endregion

   #region Health_functions
    public void TakeDamage(float value)
    {
        //sound
        FindObjectOfType<AudioManager>().Play("BatHurt");
        currHealth -= value;
        Debug.Log("did it work mom");
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
