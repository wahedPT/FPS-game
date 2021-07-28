using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public bool enemyDamage,damagePlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Enemy")&& enemyDamage)
        {
            //Destroy(this.gameObject);
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(2);
        }
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            //Debug.Log("Got hit");
            PlayerHealth.instance.PlayerDamage(1);

        }
    }
    
}
