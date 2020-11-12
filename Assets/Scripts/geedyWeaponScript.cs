using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geedyWeaponScript : MonoBehaviour
{
    private Animator anim;
   
    void Start()
    {
        //Debug.Log("OK ATTACHED");
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("active");
        }
    }
}
