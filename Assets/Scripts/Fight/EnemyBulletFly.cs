using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletFly : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    private void FixedUpdate(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Obstacle"){
            DestroyingBullet();
        }
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            DestroyingBullet();
        }
    }

    private void DestroyingBullet(){
        //Animation
        Destroy(gameObject);
    }
}
