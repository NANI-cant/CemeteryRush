using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCage : MonoBehaviour
{
    
    public SpriteRenderer _spriteRenderer;
    public Sprite spriteBold;
    public Sprite spriteLight;
    public Animator _pressEAnimator;
    public GameObject playerClone;
    private bool canPushE = false;
    private PlayerController player;
    private AudioSource _audioSource;
    public AudioClip openAudio;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update(){
        if(canPushE && Input.GetKeyDown(KeyCode.E)){
            OpenCage();
        }
    }

    private void OpenCage(){
        _audioSource.PlayOneShot(openAudio);
        player.BuffLevel();
        Instantiate(playerClone,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            player = other.gameObject.GetComponent<PlayerController>();
            _spriteRenderer.sprite = spriteBold;
            _pressEAnimator.SetTrigger("Up");
            canPushE = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            _spriteRenderer.sprite = spriteLight;
            _pressEAnimator.SetTrigger("Down");
            canPushE = false;
        }
    }
}
