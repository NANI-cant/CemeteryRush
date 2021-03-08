using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DemonEnemy : MonoBehaviour
{
    private GameObject[] walkPoints;
    private Transform playerTransform;
    public Transform shootPoint;
    public float damage;
    public Animator _animator;
    public AIDestinationSetter _aiDestinationSetter;
    public AIPath _aiPath;
    public GameObject bullet;
    private AudioSource _audioSource;
    public AudioClip shootAudio;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        walkPoints = GameObject.FindGameObjectsWithTag("PointsAroundPlayer");
        StartCoroutine("RandomPosition");
        StartCoroutine("Attack");
    }

    IEnumerator RandomPosition(){
        yield return new WaitForSeconds(5f);
        
        int i = Random.Range(0,walkPoints.Length);
        _aiDestinationSetter.target = walkPoints[i].transform;

        StartCoroutine("RandomPosition");
    }

    IEnumerator Attack(){
        yield return new WaitForSeconds(Random.Range(7f,14f));
        CalculateDirection();
        StartCoroutine("Attack");
    }

    public void Shoot(){
        Vector2 shootDirection = new Vector2(playerTransform.position.x - transform.position.x,playerTransform.position.y - transform.position.y).normalized;
        float angle = Mathf.Atan2(shootDirection.y,shootDirection.x) * Mathf.Rad2Deg;

        _audioSource.PlayOneShot(shootAudio);
        GameObject newBullet = Instantiate(bullet, shootPoint.position, Quaternion.Euler(0,0,angle));
        newBullet.GetComponent<EnemyBulletFly>().damage = damage;
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