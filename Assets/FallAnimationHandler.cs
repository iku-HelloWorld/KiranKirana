using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAnimationHandler : MonoBehaviour
{

    Animator animatorController;
    AudioSource aSource;
    [SerializeField] AudioClip screamingClip;

    private void Start()
    {
        animatorController = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Falling")
        {
            Debug.Log("Carpti");
            
            animatorController.SetBool("Falling", true);
            aSource.clip = screamingClip;
            
            aSource.Play();

        }
    }

}
