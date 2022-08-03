using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    
    [SerializeField] AudioClip walkingSound;


    public bool enableMobileInputs = false;
    bool isPlaying = false;


    public float moveSpeed = 8f;
    public float smoothRotationTime = 0.25f;


    float currentVelocity;
    float currentSpeed;
    float speedVelocity;

    Transform cameraTransform;

    public DynamicJoystick joystick;

    AudioSource aSource;
    PhotonView pw;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        aSource = GetComponent<AudioSource>();
        pw = GetComponent<PhotonView>();

    }

    void Update()
    {


        if (pw.IsMine)
        {
            Vector2 input = Vector2.zero;

            if (enableMobileInputs)
            {

                input = new Vector2(joystick.Horizontal, joystick.Vertical);

            }
            else
            {
                input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }



            Vector2 inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref currentVelocity, smoothRotationTime);

            }


            float targetSpeed = moveSpeed * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, 0.1f);



            if (inputDir.magnitude > 0)
            {
                if (isPlaying == false)
                {
                    PlayWalkSound();
                }
                WalkAnima();

            }
            else
            {
                StopWalking();
            }
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        }
    }

    private void WalkAnima()
    {
        GetComponent<Actions>().Walk();
        
    }

    private void StopWalking()
    {
        GetComponent<Actions>().Stay();
        StopSound();
    }

    private void StopSound()
    {
        aSource.Stop();
        isPlaying = false;
    }
    private void PlayWalkSound()
    {
        aSource.clip = walkingSound;
        aSource.loop = true;
        aSource.Play();

        isPlaying = true;

    }
}
