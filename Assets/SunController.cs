using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
	public GameObject sun;

	public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
		sun = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		sun.transform.RotateAround(new Vector3(0, 0, 0), Vector3.right, rotateSpeed * Time.deltaTime);
    }
}
