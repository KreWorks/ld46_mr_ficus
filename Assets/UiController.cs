using System;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
	public Image status;

	public Image temperatureLevel;
	public Image waterLevel;
	public Image lightlevel;
	public Image windLevel;

    // Start is called before the first frame update
    void Start()
    {
		lightlevel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ChangeStatusImage(Sprite newImage, Color newColor)
	{
		status.color = newColor;

		Image statusFace = status.GetComponentsInChildren<Image>()[1];

		statusFace.sprite = newImage;
	
	}
}
