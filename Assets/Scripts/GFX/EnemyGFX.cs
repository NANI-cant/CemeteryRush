using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public MeleeEnemy _meleeEnemy;
    public RangeEnemy _rangeEnemy;
    public DemonEnemy _demonEnemy;
    public ShamanEnemy _shamanEnemy;

    private void Update(){
        transform.rotation = Quaternion.identity;
        if(aiPath.desiredVelocity.x >=0.01f){
            transform.localScale = new Vector3(-1f,1f,1f);
        } else if(aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }

    private void DoDamage(){
        _meleeEnemy.DoDamage();
    }
    private void EndMeleeHit(){
        _meleeEnemy.EndHit();
    }

    private void DoRangeShoot(){
        _rangeEnemy.ShootBullet();
    }
    private void EndRangeHit(){
        _rangeEnemy.EndHit();
    }

    private void DoDemonShoot(){
        _demonEnemy.Shoot();
    }

    private void Summon(){
        _shamanEnemy.Summon();
    }
}
