using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class deathHandler : MonoBehaviour
{

    [SerializeField]  bool stillwaiting=false;
    float answerRevealTimer;
    public bool mustDie=false;
    [SerializeField] public bool answerReveal;
    public float answerTimer;
    PhotonView pw;
    public Canvas loseCanvas;
    public Canvas controllerCanvas;
    [SerializeField] GameObject initialCameraPos;
    GameObject camGameObject;
    // Start is called before the first frame update
    void Start()
    {



        pw = GetComponent<PhotonView>();
        GameObject tempCanvas = GameObject.Find("LoseCanvas");
        GameObject tempControllerCanvas = GameObject.Find("Controller");
        if (tempCanvas != null && tempControllerCanvas != null)
        {
            loseCanvas = tempCanvas.GetComponent<Canvas>();
            controllerCanvas = tempControllerCanvas.GetComponent<Canvas>();


            if (loseCanvas == null)
            {
                Debug.Log("could not locate the lose canvas" + tempCanvas.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        answerTimer = FindObjectOfType<GameManager>().answeringTimer;
        answerReveal = FindObjectOfType<GameManager>().answerReveal;
        answerRevealTimer = FindObjectOfType<GameManager>().answerRevealTimer;
        if (answerReveal)
        {
            if (stillwaiting)
            {
                
                loseCanvas.enabled = true;
                controllerCanvas.enabled = false;
                FindObjectOfType<MyPlayer>().gameObject.GetPhotonView().enabled = false;
                FindObjectOfType<MyPlayer>().gameObject.GetComponent<CameraMovement>().enabled = false;
                GameObject restartMenager = FindObjectOfType<RestartOrSpectate>().gameObject;
                if (FindObjectOfType<MyPlayer>().gameObject.GetComponent<PhotonView>().IsMine)
                {
                    //PhotonNetwork.Destroy()
                }
                restartMenager.GetComponent<RestartOrSpectate>().enabled = true;
                //FindObjectOfType<teleportSc>().destroyCharacter();
               
            }
        }


        if (answerRevealTimer<=0)
        {
            stillwaiting = false;
        }

       

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PhotonView>().IsMine)
        {

            stillwaiting = true;
            
        }





    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PhotonView>().IsMine)
        {
            stillwaiting = false;
        }
    }



















































    //    pw = GetComponent<PhotonView>();
    //    GameObject tempCanvas = GameObject.Find("LoseCanvas");
    //    GameObject tempControllerCanvas = GameObject.Find("Controller");
    //    if (tempCanvas != null && tempControllerCanvas != null)
    //    {
    //        loseCanvas = tempCanvas.GetComponent<Canvas>();
    //        controllerCanvas = tempControllerCanvas.GetComponent<Canvas>();


    //        if (loseCanvas == null)
    //        {
    //            Debug.Log("could not locate the lose canvas" + tempCanvas.name);
    //        }
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    answerTimer = FindObjectOfType<GameManager>().answeringTimer;
    //    answerReveal = FindObjectOfType<GameManager>().answerReveal;
    //        if (answerReveal)
    //        {
    //            if (stillwaiting)
    //            {
    //                loseCanvas.enabled = true;
    //                controllerCanvas.enabled = false;
    //                gameObject.GetPhotonView().enabled = false;
    //                gameObject.GetComponent<CameraMovement>().enabled = false;
    //                GameObject restartMenager = FindObjectOfType<RestartOrSpectate>().gameObject;
    //                restartMenager.GetComponent<RestartOrSpectate>().enabled = true;
    //                PhotonNetwork.Destroy(gameObject);
    //                camGameObject = Instantiate(initialCameraPos, new Vector3(-27.3999996f, 62.5999985f, -37.4000015f), new Quaternion(0.0502322726f, 0.280282021f, -0.0146889426f, 0.958489954f)); ;
    //            }
    //        }

    //        //sürenin sonunda þey etmeyi aþaðýda yapalým süre bittikten sonra bool ile direkt.


    //}


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "waiting"&&pw.IsMine)
    //    {
    //        stillwaiting = true;
    //    }





    //}


    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "waiting"&&pw.IsMine)
    //    {
    //        stillwaiting = false;
    //    }
    //}
}

