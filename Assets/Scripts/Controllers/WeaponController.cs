using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Vector3 different;
    public Transform playerParentTransform;
    public PlayerGFX _GFX;
    public GameObject[] allWeapons;
    private float angle;
    private void Update(){
        different = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerParentTransform.position;
        angle = Mathf.Atan2(different.y, different.x)*Mathf.Rad2Deg;

        if(different.x<0){
            transform.localScale = new Vector3(1f,-1f,1f);
        }else{
            transform.localScale = new Vector3(1f,1f,1f);
        }

        CalculateDirection(angle);

        transform.rotation = Quaternion.Euler(0.0f,0.0f,angle);
    }

    private void CalculateDirection(float _angle){
        _angle *= Mathf.Deg2Rad;
        if(Mathf.Cos(_angle)>=(Mathf.Sqrt(2)/2)){
            //_GFX.SetRightSprite();
            _GFX.direction = 1;
        }
        if(Mathf.Cos(_angle)<=(-Mathf.Sqrt(2)/2)){
            //_GFX.SetLeftSprite();
            _GFX.direction = 3;
        }
        if(Mathf.Sin(_angle)>(Mathf.Sqrt(2)/2)){
            //_GFX.SetTopSprite();
            _GFX.direction = 0;
        }
        if(Mathf.Sin(_angle)<(-Mathf.Sqrt(2)/2)){
            //_GFX.SetDownSprite();
            _GFX.direction = 2;
        }
    }
}
