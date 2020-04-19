using UnityEngine;

public class FreezePlantState : PlantState
{
	public FreezePlantState(PlantController plant, Sprite icon) : base(plant, icon)
	{

	}

	public override void GetBetter()
	{
		this.plant.plantState = this.plant.neutralState;
	}

	public override void GetWorse()
	{
		this.plant.plantState = this.plant.tragicState;
	}
}
