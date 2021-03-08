using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerCloneController : MonoBehaviour
{
    public PlayerGFX _GFX;
    public AIPath _aiPath;
    public AIDestinationSetter _aiDestinationSetter;
    public GameObject deathParticleSystem;
    private Transform playerTransform;
    private GameManager gameManager;
    public AudioClip openCageSound;
    private AudioSource _audioSource;
    public GameObject weapons;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        int i = gameManager.playerAndClones.Count-1;
        _aiDestinationSetter.target = gameManager.playerAndClones[i];
        gameManager.playerAndClones.Add(transform);
    }

    private void Update(){
        if(_aiPath.desiredVelocity.x != 0 || _aiPath.desiredVelocity.y != 0){
            _GFX.isRunning = true;
        }
    }

    public void Death(){
        weapons.SetActive(false);
        _GFX._spriteRenderer.color = new Color(1f,1f,1f,0f);
        Instantiate(deathParticleSystem,transform.position, Quaternion.identity);
    }

}
