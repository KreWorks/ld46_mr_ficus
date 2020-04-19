using UnityEngine;

public class NeutralPlantState : PlantState
{
	public NeutralPlantState(PlantController plant, Sprite icon) : base(plant, icon)
	{

	}

	public override void GetBetter()
	{
		this.plant.plantState = this.plant.pleaseState;
	}

	public override void GetWorse()
	{
		this.plant.plantState = this.plant.sadState;
	}
}
