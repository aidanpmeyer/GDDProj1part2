using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    # region Movement_variables
    public float startmovespeed;
    private float movespeed;
    float x_input;
    float y_input;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Attack_variables
    public float Damage;
    public float attackspeed = 1;
    float attackTimer;
    private float speedTimer;
    public float hitboxtiming;
    public float endanimationtiming;
    bool isAttacking;
    Vector2 currDirection;
    

    #endregion

    #region Animation_components
    Animator anim;
    #endregion

    #region Health_variables
    public Slider HPSlider;
    public float maxHealth;
    float currHealth;
    #endregion

    #region Unity_functions
 
    private void Awake()
    {
        movespeed = startmovespeed;
        PlayerRB = GetComponent<Rigidbody2D>();
        attackTimer = 0;
        anim = GetComponent<Animator>();

        currHealth = maxHealth;
        HPSlider.value = currHealth/maxHealth;
    }

    private void Update()
    {
        if (isAttacking)
        {
            return;
        }
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        Move();
       

        if(speedTimer <= 0)
        {
            if (movespeed > startmovespeed){
              movespeed = startmovespeed;  
            }
            
        } else{
            speedTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.J) && attackTimer <= 0)
        {
            Attack();
        } else{
            attackTimer -= Time.deltaTime;
        }

         if(Input.GetKeyDown(KeyCode.L)){
             Interact();
         }

    }
    #endregion

    #region Movement_function

    private void Move()
    {
        anim.SetBool("Moving", true);
        if(x_input > 0)
        {
            PlayerRB.velocity = Vector2.right * movespeed;
            currDirection = Vector2.right; 
            
        }
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left; 
        }
        else if(y_input > 0)
        {
            PlayerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up; 
        }
        else if (y_input < 0)
        {
            PlayerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down; 
        } else
        {
            PlayerRB.velocity = Vector2.zero;
            anim.SetBool("Moving",false);
        }
        anim.SetFloat("DirX",currDirection.x);
        anim.SetFloat("DirY",currDirection.y);

    }
    #endregion

    #region Attack_functions

    private void Attack()
    {
        Debug.Log("attacking now");
        Debug.Log(currDirection);
        attackTimer = attackspeed;
        //handles animation and hit box calcs
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PlayerRB.velocity = Vector2.zero;

        anim.SetTrigger("Attacking");

        //sound
        FindObjectOfType<AudioManager>().Play("Player");
        yield return new WaitForSeconds(hitboxtiming);
        Debug.Log("Casting hitbox now");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position, Vector2.one, 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits) 
        {
           if(hit.transform.CompareTag("Enemy"))
           {
               Debug.Log("Tons of Damage");
               hit.transform.GetComponent<Enemy>().TakeDamage(Damage);
           }
           if(hit.transform.CompareTag("Enemy2"))
           {
               Debug.Log("Tons of Damage");
               hit.transform.GetComponent<Enemy2>().TakeDamage(Damage);
           }
        }
        yield return new WaitForSeconds(hitboxtiming);
        isAttacking = false;

        yield return null;
    }

    #endregion

    #region Health_functions

    public void TakeDamage(float value)
    {

        // sound
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        currHealth -= value;
        Debug.Log("Health is now" + currHealth.ToString());
         HPSlider.value = currHealth/maxHealth;

        // change UI

        //Check if dead
        if(currHealth <= 0){
            //die
            Die();
        }
    }

    public void Heal(float value)
    {
        currHealth += value;
        currHealth = Mathf.Min(currHealth,maxHealth);
        HPSlider.value = currHealth/maxHealth;
    }

    private void Die()
    {
        //sound
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        //destroy object
        
        Destroy(this.gameObject);
        //find game manager and lose game
        GameObject gm = GameObject.FindWithTag("GameController");
        Debug.Log("got to lose");
        gm.GetComponent<GameManager>().LoseGame();
    }
    #endregion

    #region interact_function
    private void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, new Vector2(.5f,.5f),0f,Vector2.zero,0f);
        foreach (RaycastHit2D hit in hits)
        {
             
            if (hit.transform.CompareTag("Chest"))
            {
               
                hit.transform.GetComponent<Chest>().Interact();
            }
        }
    }
    #endregion

    #region SpeedUp
    public void SpeedUp(float speedamount)
    {
        movespeed = speedamount;
        speedTimer = 5;
    }
    #endregion
}
