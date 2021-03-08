using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePoint : MonoBehaviour
{
    public Vector2 different;
    public Transform playerParentTransform;
    private void Update(){
        different = Camera.main.ScreenToWorldPoint(Input.mousePosition) + playerParentTransform.position;
        different.x/=2;
        different.y/=2;
        transform.position = different;
    }
}
