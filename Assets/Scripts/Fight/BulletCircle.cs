using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle : MonoBehaviour
{
    private void Update(){
        transform.Rotate(Vector3.zero, 10*Time.deltaTime);
    }
}
