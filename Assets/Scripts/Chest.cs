using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObj_variables
    [SerializeField]
    [Tooltip("health pack")]
    private GameObject healthpack;
    [SerializeField]
    [Tooltip("speed up")]
    private GameObject SpeedUp;
    #endregion
    public GameObject explosionObj;
    public float explosionDamage;
    public float  explosionRadius;

    #region Chest_functions
    IEnumerator DestroyChest()
    {
        GameObject item = null;
        int random100 = Random.Range(0,100);
        yield return new WaitForSeconds(.3f);
        if (random100 > 40){
            item = healthpack;
        } else if(random100 > 10) {
            item = SpeedUp;
        } else {
            Explode();
        }

        Instantiate(item, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

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

    public void Interact()
    {
        StartCoroutine("DestroyChest");
    }
    #endregion

}
