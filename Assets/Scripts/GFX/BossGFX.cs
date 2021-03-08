using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossGFX : MonoBehaviour
{
    public BossEnemy _bossEnemy;
    public AIPath aiPath;

    private void Update(){
        transform.rotation = Quaternion.identity;
        if(aiPath.desiredVelocity.x >=0.01f){
            transform.localScale = new Vector3(-1f,1f,1f);
        } else if(aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }

    private void Summon(){
        _bossEnemy.Summon();
    }

    private void BulletStorm(){
        _bossEnemy.BulletStorm();
    }

    private void BigBang(){
        _bossEnemy.BigBang();
    }

    private void StopAttack(){
        _bossEnemy.CanMove();
    }
}
