using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;

public class hideSpawnCube : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}
