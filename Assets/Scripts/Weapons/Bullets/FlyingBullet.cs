﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBullet : MonoBehaviour
{
    public float speed;
    public float damage = 1f;
    private void FixedUpdate(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Obstacle"){
            DestroyingBullet();
        }
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            DestroyingBullet();
        }
    }

    private void DestroyingBullet(){
        //Animation
        Destroy(gameObject);
    }
}
