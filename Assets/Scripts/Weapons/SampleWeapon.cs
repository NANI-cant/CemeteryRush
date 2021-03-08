using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Animator _animator;
    private bool canShoot = true;
    private AudioSource _audioSource;
    public AudioClip shootAudio;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate(){
        if(Input.GetButton("Fire1")){
            Shoot();
        }
    }

    private void Shoot(){
        if(canShoot){
            canShoot = false;
            _animator.SetTrigger("shootTrigger");
            Instantiate(bullet, transform.position, transform.rotation);
            _audioSource.PlayOneShot(shootAudio);
        }
    }

    public void timeBtwShootsEnd(){
        canShoot = true;
    }
}
