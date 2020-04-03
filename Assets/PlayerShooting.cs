using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public float fireRate = 0.5f;
    private float cooldown = 0;
    public float damage = 25f;


	
	// Update is called once per frame
	void Update ()
	{
	    cooldown -= Time.deltaTime;

	    if (Input.GetButton("Fire1"))
	    {
	        Fire();
	    }
	}

    void Fire()
    {
        if (cooldown > 0)
        {
            return;
        }
        Debug.Log("Firing our gun!");

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Transform hitTransform;
        Vector3 hitPoint;

        hitTransform = FindClosestHitObject(ray, out hitPoint);

        if (hitTransform != null)
        {
            Debug.Log("We hit: " + hitTransform.name);

            Health h = hitTransform.GetComponent<Health>();

            while (h == null && hitTransform.parent)
            {
                hitTransform = hitTransform.parent;
                h = hitTransform.GetComponent<Health>();
            }

            if (h != null)
            {
                PhotonView pv = h.GetComponent<PhotonView>();
                if (pv == null)
                {
                    Debug.Log("No Photon View");
                }
                else
                {
                    h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, damage);
                }
            }
        }

    }

    Transform FindClosestHitObject(Ray ray, out Vector3 hitPoint)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);

        Transform closestHit = null;
        float distance = 0;
        hitPoint = Vector3.zero;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != this.transform && (closestHit == null || hit.distance < distance))
            {
                closestHit = hit.transform;
                distance = hit.distance;
                hitPoint = hit.point;
            }
        }

        return closestHit;
    }
}
