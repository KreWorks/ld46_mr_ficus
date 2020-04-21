using UnityEngine;

public class PleasedPlantState : PlantState
{
	public PleasedPlantState(PlantController plant, Sprite icon, Color color) : base(plant, icon, color)
	{

	}

	public override void GetBetter()
	{
		this.plant.TransitionToState(this.plant.delightState);
	}

	public override void GetWorse()
	{
		this.plant.TransitionToState(this.plant.neutralState);
	}
}
