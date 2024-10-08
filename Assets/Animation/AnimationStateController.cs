using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("test", 5);
    }

    // Update is called once per frame
    void Update()
    {
        

    
    }

    void test()
    {
        animator.SetBool("IsSurfing", false);
        Debug.Log("test");
    }
}
