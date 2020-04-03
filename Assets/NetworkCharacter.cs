using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour
{
    private Vector3 realPosistion;
    private Quaternion realRotation;
    private float lastUpdateTime;
    private Animator anim;

	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (photonView.isMine)
	    {
            // do nothing
	    }
	    else
	    {
            transform.position = Vector3.Lerp(transform.position, realPosistion,0.35f);
	        
	        transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // our player

            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(anim.GetFloat("Speed"));
            stream.SendNext(anim.GetBool("Jump"));
        }
        else
        {
            // other player
            realPosistion = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            anim.SetFloat("Speed",(float)stream.ReceiveNext() );
            anim.SetBool("Jump",(bool)stream.ReceiveNext());
        }
    }
}
