using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
	Action<string> OnMouseClickedHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		/*
		 * To check if it is over something
		Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit2;


		if (Physics.Raycast(ray2.origin, ray2.direction, out hit2, Mathf.Infinity))
		{
			Debug.Log("Valami fölött vagy");
			if (hit2.transform.tag == "Window")
			{
				Debug.Log("Ablakra nyomtál");
			}

		}*/

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;


			if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
			{
				OnMouseClickedHandler?.Invoke(hit.transform.tag);

				/*if (hit.transform.tag == "Window")
				{
					Debug.Log("Ablakra nyomtál");
				}*/

			}
		}
		
	}

	public void AddListenerOnMouseClickedEvent(Action<string> listener)
	{
		OnMouseClickedHandler += listener;
	}
	public void RemoveListenerOnMouseClickedEvent(Action<string> listener)
	{
		OnMouseClickedHandler -= listener;
	}
}
