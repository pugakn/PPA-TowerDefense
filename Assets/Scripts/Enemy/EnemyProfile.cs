using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyProfile : MonoBehaviour {
    public int Health, Damage, Speed;
    void Death()
    {
        Waves_timer.enemyes.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void TakingDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Death();
        }
    }
    void OnTriggerEnter2D(Collider2D obj){
      if (obj.gameObject.tag == "BULLET"){
        TakingDamage(1);
        Destroy(obj);

      }

    }
}
