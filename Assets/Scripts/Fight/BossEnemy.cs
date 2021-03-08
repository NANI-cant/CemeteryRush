using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossEnemy : MonoBehaviour
{
    public float bossHealth;
    public EnemyController _enemyController;
    public AIPath _aiPath;
    public AIDestinationSetter _aiDestinationSetter;
    public Animator _animator;
    public Transform[] bulletPoints;
    public Transform[] summonPoints;
    public GameObject summoningObject;
    public GameObject bulletSmall;
    public GameObject bulletBig;
    public float timeBetweenAttacks;
    private float timeRemaining;
    private GameObject[] playerPoints;
    public bool attackIsGoing = false;


    private void Start(){
        timeRemaining = timeBetweenAttacks;
        playerPoints = GameObject.FindGameObjectsWithTag("PointsAroundPlayer");
        bossHealth = _enemyController.health;
    }

    private void Update(){
        if(!attackIsGoing){
            timeRemaining -= Time.deltaTime;
        }

        if(timeRemaining<=0){
            _aiPath.canMove = false;
            timeRemaining = timeBetweenAttacks;
            int randAttack = Random.Range(0,3);
            switch (randAttack)
            {
                case 0: attackIsGoing = true;
                        _animator.SetTrigger("SummonTrigger");
                break;
                case 1: attackIsGoing = true;
                        _animator.SetTrigger("BulletStromTrigger");
                break;
                case 2: BigBang();
                break;
            }
        }

        bossHealth = _enemyController.health;
        if(bossHealth <= _enemyController.maxHealth/2f){
            ChangeToSecondStadia();
        }
    }

    private void ChangeToSecondStadia(){
        timeBetweenAttacks-=3f;
    }

    public void Summon(){
        int count = Random.Range(1,4);
        for(int i=0;i<count;i++){
            Instantiate(summoningObject,summonPoints[i].position,Quaternion.identity);
        }
        _aiPath.canMove = true;
        attackIsGoing = false;
    }

    public void BulletStorm(){
        for(int i=0;i<bulletPoints.Length;i++){
            Instantiate(bulletSmall, bulletPoints[i].position, bulletPoints[i].localRotation);
        }
    }

    public void BigBang(){
        _aiPath.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1f);
        }
    }

    public void CanMove(){
        _aiPath.canMove = true;
        attackIsGoing = false;
    }

}
