using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemy : MonoBehaviour
{
    public float damage;
    public AIPath _aiPath;
    public Animator _animator;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask playerLayerMask;
    private bool canHit = true;
    private Transform playerTransform;
    private AudioSource _audioSource;
    public AudioClip hitAudio;

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

    public void DoDamage(){
        _audioSource.PlayOneShot(hitAudio);
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayerMask);
        foreach (Collider2D player in playerCollider){
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }
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
