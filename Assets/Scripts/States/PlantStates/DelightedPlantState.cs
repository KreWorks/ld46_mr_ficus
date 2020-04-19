using UnityEngine;

public class DelightedPlantState : PlantState
{
	public DelightedPlantState(PlantController plant, Sprite icon) : base(plant, icon)
	{

	}

	public override void GetBetter()
	{
		return;
	}

	public override void GetWorse()
	{
		this.plant.plantState = this.plant.pleaseState;
	}
}
