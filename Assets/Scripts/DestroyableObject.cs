using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public GameObject crackedModel;
    public void Crack(){
        //Animation
        Instantiate(crackedModel, transform.position, Quaternion.identity);
    }
}
