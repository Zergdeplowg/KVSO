using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hipPoints = 100f;
    private float actualHitPoints;

    void Start()
    {
        actualHitPoints = hipPoints;
    }

    [PunRPC]
    public void TakeDamage(float amt)
    {
        actualHitPoints -= amt;

        if (actualHitPoints <= 0)
        {
            GetComponent<PhotonView>().RPC("Die",PhotonTargets.All,null);
        }
    }

    [PunRPC]
    void Die()
    {
        if (GetComponent<PhotonView>().instantiationId == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
