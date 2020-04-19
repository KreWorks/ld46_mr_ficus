using UnityEngine;

public class PleasedPlantState : PlantState
{
	public PleasedPlantState(PlantController plant, Sprite icon) : base(plant, icon)
	{

	}

	public override void GetBetter()
	{
		this.plant.plantState = this.plant.delightState;
	}

	public override void GetWorse()
	{
		this.plant.plantState = this.plant.neutralState;
	}
}
