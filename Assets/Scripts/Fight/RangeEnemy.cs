using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangeEnemy : MonoBehaviour
{
    public float damage;
    public AIPath _aiPath;
    public GameObject bullet;
    public Animator _animator;
    public Transform shootPoint;
    private bool canHit = true;
    private Transform playerTransform;
    private AudioSource _audioSource;
    public AudioClip shootAudio;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate(){
        if(Vector2.Distance(transform.position, playerTransform.position)<=_aiPath.endReachedDistance){
            Hit();
        }
    }

    private void Hit(){
        if(canHit){
            CalculateDirection();
            _aiPath.canMove = false;
            canHit = false;
        }
    }

    public void ShootBullet(){
        Vector2 shootDirection = new Vector2(playerTransform.position.x - transform.position.x,playerTransform.position.y - transform.position.y).normalized;
        float angle = Mathf.Atan(shootDirection.y/shootDirection.x) * Mathf.Rad2Deg;
        if(shootDirection.x<0){
            angle+=180;
        }

        _audioSource.PlayOneShot(shootAudio);
        GameObject newBullet = Instantiate(bullet, shootPoint.position, Quaternion.Euler(0,0,angle));
        newBullet.GetComponent<EnemyBulletFly>().damage = damage;
    }

    public void EndHit(){
        canHit = true;
        _aiPath.canMove = true;
    }

    private void CalculateDirection(){
        Vector2 direction = new Vector2(_aiPath.desiredVelocity.x,_aiPath.desiredVelocity.y);
        float _angle  = Mathf.Atan2(direction.y,direction.x);
        if(Mathf.Cos(_angle)>=(Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("HitTriggerLeft");
        }
        if(Mathf.Cos(_angle)<=(-Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("HitTriggerLeft");
        }
        if(Mathf.Sin(_angle)>(Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("HitTriggerTop");
        }
        if(Mathf.Sin(_angle)<(-Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("HitTriggerDown");
        }
    }
}
