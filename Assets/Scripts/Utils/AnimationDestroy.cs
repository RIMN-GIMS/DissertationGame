using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Animator animator;

    // Update is called once per frame
    void Start()
    {
        // destroys object after animation has played
        Destroy(gameObject,animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
