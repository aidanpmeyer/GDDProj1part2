using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    #region HeathPack_variables
    [SerializeField]
    [Tooltip("the ammount the player heals")]
    private int healamount;
    #endregion

    #region Heal_functrions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerControler>().Heal(healamount);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
