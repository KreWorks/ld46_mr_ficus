using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
	float fullCircleTime;
	float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
		TimeController timeController = FindObjectOfType<TimeController>();

		fullCircleTime = timeController.inGameHour * 24;
		rotateSpeed = 360.0f / fullCircleTime;
		/*
		 ingamehour
		 */
    }

    // Update is called once per frame
    void Update()
    {
		transform.RotateAround(new Vector3(0, 0, 0), Vector3.right, -rotateSpeed * Time.deltaTime);
    }
}