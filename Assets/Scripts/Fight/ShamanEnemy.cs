using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ShamanEnemy : MonoBehaviour
{
    
    public Transform[] summonPoints;
    public GameObject[] walkablePoints;
    public GameObject zombieObject;
    public AIPath _aiPath;
    public AIDestinationSetter _aiDestinationSetter;
    public Animator _animator;
    public float timeBetweenSummoning;
    private float timeSummoningRemaining;
    private AudioSource _audioSource;
    public AudioClip summonAudio;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        timeSummoningRemaining = timeBetweenSummoning;
        walkablePoints = GameObject.FindGameObjectsWithTag("WalkablePoint");
        Invoke("SetFirstPoint",3f);
    }
    private void Update(){
        timeSummoningRemaining -=Time.deltaTime;
        if(timeSummoningRemaining <= 0){
            Invoke("CanWalk", 2f);
            _aiPath.canMove = false;
            _animator.SetTrigger("SummonTrigger");
            timeSummoningRemaining = timeBetweenSummoning;
        }

        if(Vector2.Distance(transform.position, _aiDestinationSetter.target.position)<=6f){
            _aiDestinationSetter.target = walkablePoints[Random.Range(0,walkablePoints.Length)].transform;
        }
    }

    public void Summon(){
        _audioSource.PlayOneShot(summonAudio);
        int count = Random.Range(2,5);
        for(int i=0;i<count;i++){
            Instantiate(zombieObject,summonPoints[i].position,Quaternion.identity);
        }
    }

    private void CanWalk(){
        _aiPath.canMove = true;
    }

    private void SetFirstPoint(){
        _aiDestinationSetter.target = walkablePoints[Random.Range(0,walkablePoints.Length)].transform;
    }
}
