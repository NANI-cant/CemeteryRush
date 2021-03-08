using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSword : MonoBehaviour
{
    public float damage;
    public Animator _animator;
    private bool canAttack = true;

    private void FixedUpdate(){
        if(Input.GetButton("Fire1")){
            Attack();
        }
    }

    private void Attack(){
        if(canAttack){
            canAttack = false;
            _animator.SetTrigger("attackTrigger");
        }
    }

    public void timeBtwAttacksEnd(){
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
}
