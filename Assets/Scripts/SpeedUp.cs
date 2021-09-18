using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    #region HeathPack_variables
    [SerializeField]
    [Tooltip("the ammount the player speeds")]
    private int speedamount;
    #endregion

    #region Heal_functrions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Debug.Log("this is shit");
            collision.transform.GetComponent<PlayerControler>().SpeedUp(speedamount);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
