using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostEnemy : MonoBehaviour
{
    private Transform playerTransform;
    public float timeBetweenAttack;
    public AIPath _aiPath;
    private float timeRemaining;
    private AudioSource _audioSource;
    public AudioClip shiftAudio;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timeRemaining = timeBetweenAttack;
    }

    private void Update(){
        timeRemaining-=Time.deltaTime;
        if(timeRemaining<=0){
            _aiPath.canMove = false;
            transform.Translate(Vector2.up*10f*Time.deltaTime);
        }
        if(timeRemaining<=-0.5){
            _audioSource.PlayOneShot(shiftAudio);
            timeRemaining = timeBetweenAttack;
        } else{
            _aiPath.canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1f);
        }
    }
}
