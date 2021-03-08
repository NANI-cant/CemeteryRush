using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int level = 1;
    public float maxHealth = 3;
    public float health;
    public float speed = 1.0f;
    public Rigidbody2D _rigidbody2D;
    public PlayerGFX _GFX;
    public GameManager gameManager;
    public GameObject deathParticleSystem;
    private AudioSource _audioSource;
    public AudioClip takeDamageAudio;
    public AudioClip deathAudio;
    private bool canTakeDamage = true;
    private bool canRun = true;
    private bool isAlive = true;
    public GameObject weapons;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        gameManager.playerAndClones.Add(transform);
    }
    private void FixedUpdate(){
        if(canRun){
            CalculateVelocity();
        }
    }

    private void CalculateVelocity(){
        float moveX, moveY;
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Vector2 velocityVector = new Vector2(moveX,moveY).normalized*speed;
        _rigidbody2D.velocity = velocityVector;
        if(moveX==0 && moveY==0){
            _GFX.isRunning = false;
        }else{
            _GFX.isRunning = true;
        }
    }

    public void TakeDamage(float takingDamage){
        if(canTakeDamage && isAlive){
            _audioSource.PlayOneShot(takeDamageAudio);
            ImmortalityStart();
            Invoke ("ImmortalityEnd",2f);
            gameManager.PlayerTakeDamage();
            health -= takingDamage;
            if(health<=0){
                Death();
            }
        }
    }

    public void SetRun(bool flag){
        canRun = flag;
        if(!flag){
            _rigidbody2D.velocity = new Vector2(0f,0f);
        }
    }

    private void ImmortalityStart(){
        canTakeDamage = false;
        speed+=2f;
        _GFX._spriteRenderer.color = new Color(1f,1f,1f,0.5f);
    }

    private void ImmortalityEnd(){
        if(isAlive){
            canTakeDamage = true;
            speed-=2f;
            _GFX._spriteRenderer.color = new Color(1f,1f,1f,1f);
        }
    }

    private void Death(){
        _audioSource.PlayOneShot(deathAudio);
        weapons.SetActive(false);
        for(int i = 1;i<gameManager.playerAndClones.Count;i++){
            gameManager.playerAndClones[i].GetComponent<PlayerCloneController>().Death();
        }
        gameManager.EndGame();
        isAlive = false;
        _GFX._spriteRenderer.color = new Color(1f,1f,1f,0f);
        SetRun(false);
        Instantiate(deathParticleSystem,transform.position, Quaternion.identity);
    }

    public void BuffLevel(){
        //Animation
        //Change Sprite
        BustMaxHealth();
        Heal();
    }

    public void BustMaxHealth(){
        gameManager.PlayerBoost();
        maxHealth++;
    }

    public void Heal(){
        if(health<maxHealth){
            health++;
        }
    }
    public void FullHeal(){
        health = maxHealth;
        gameManager.PlayerFullHeal();
    }
}
