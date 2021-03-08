using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    private Transform playerTransform;
    private AIDestinationSetter _AIDestinationSetter;
    private AudioSource _audioSource;
    public float health;

    public GameObject spawnParticleSystem;
    public float maxHealth;
    public float microStanTime;
    public Animator _animator;
    public AIPath _aiPath;
    public SpriteRenderer _spriteRenderer;
    public AudioClip takeDamageAudio;
    public bool isBoss = false;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.EnemyCreated();
        Instantiate(spawnParticleSystem, transform.position, Quaternion.identity);
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _AIDestinationSetter = GetComponent<AIDestinationSetter>();
        _AIDestinationSetter.target = playerTransform;
    }

    private void Update(){
        CalculateDirection();
    }

    public void TakeDamage(float damage){
        _audioSource.PlayOneShot(takeDamageAudio);
        health-=damage;
        Bush();
        Invoke("UnBush",microStanTime);
        if(health<=0){
            Death();
        }
    }
    private void Bush(){
        _spriteRenderer.material.SetFloat("_isHit",1);
        if(!isBoss){
            _aiPath.canMove = false;
        }
    }
    private void UnBush(){
        _spriteRenderer.material.SetFloat("_isHit",0);
        if(!isBoss){
            _aiPath.canMove = true;
        }
    }
    private void Death(){
        //Animation
        gameManager.EnemyKilled();
        Destroy(gameObject);
    }

    public void Heal(){
        if(health < maxHealth){
            health++;
        }
    }

    private void CalculateDirection(){
        Vector2 direction = new Vector2(_aiPath.desiredVelocity.x,_aiPath.desiredVelocity.y);
        float _angle  = Mathf.Atan2(direction.y,direction.x);
        if(Mathf.Cos(_angle)>=(Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("RunningLeftTrigger");
        }
        if(Mathf.Cos(_angle)<=(-Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("RunningLeftTrigger");
        }
        if(Mathf.Sin(_angle)>(Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("RunningTopTrigger");
        }
        if(Mathf.Sin(_angle)<(-Mathf.Sqrt(2)/2)){
            _animator.SetTrigger("RunningDownTrigger");
        }
    }
}
