using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class PlantController : MonoBehaviour
{
	public Sprite deadIcon;
	public Sprite freezeIcon;
	public Sprite delightIcon;
	public Sprite pleaseIcon;
	public Sprite neutralIcon;
	public Sprite sadIcon;
	public Sprite tragicIcon;

	public UiController uiController;

	public PlantState plantState;

	public DeadPlantState deadState;
	public FreezePlantState freezeState;
	public DelightedPlantState delightState;
	public PleasedPlantState pleaseState;
	public NeutralPlantState neutralState;
	public SadPlantState sadState;
	public TragicPlantState tragicState;

	float waterLevel; // 0- 100
	float temperature; // 10-35
	float lightLevel; // 0 -5
	int windLevel; // 0-5

	List<GameObject> leaves;
	List<GameObject> parts;

	// Start is called before the first frame update
	void Start()
    {
		deadState =  new DeadPlantState(this, deadIcon);
		freezeState = new FreezePlantState(this, freezeIcon);
		delightState = new DelightedPlantState(this, delightIcon);
		pleaseState = new PleasedPlantState(this, pleaseIcon);
		neutralState = new NeutralPlantState(this, neutralIcon);
		sadState = new SadPlantState(this, sadIcon);
		tragicState = new TragicPlantState(this, tragicIcon);

		TransitionToState( pleaseState);

		waterLevel = 50.0f;
		temperature = 22.0f;
		lightLevel = 2;
		windLevel = 0;

		//get parts

	}

    // Update is called once per frame
    void Update()
    {
		Grow();
    }

	void Grow()
	{

	}

	public void TransitionToState(PlantState newState)
	{
		uiController.ChangeStatusImage(newState.GetIcon(), Color.green);
	}

	public float GetWaterUsage()
	{
		float waterUsage = 0.0f;
		// Based on the temperature
		if (temperature >= 10.0f && temperature < 15.0f)
		{
			waterUsage = 1.0f;
		}
		else if(temperature >= 15.0f && temperature < 20.0f)
		{
			waterUsage = 1.7f;
		} 
		else if(temperature >= 20.0f && temperature < 25.0f)
		{
			waterUsage = 2.0f;
		} 
		else if(temperature >= 25.0f && temperature < 30.0f)
		{
			waterUsage = 2.5f;
		}
		else if (temperature >= 30.0f)
		{
			waterUsage = 3.0f;
		}
		//Modifier based on the current water level
		if (waterLevel > 75.0f)
		{
			waterLevel *= 0.9f;
		} 
		else if(waterLevel >= 25.0f && waterLevel < 50.0f)
		{
			waterLevel *= 1.1f;
		}
		else if(waterLevel < 25.0f)
		{
			waterLevel *= 1.2f;
		}

		return waterUsage;
	}
}
