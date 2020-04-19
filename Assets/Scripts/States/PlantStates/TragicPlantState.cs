using UnityEngine;

public class TragicPlantState : PlantState
{
	public TragicPlantState(PlantController plant, Sprite icon) : base(plant, icon)
	{

	}

	public override void GetBetter()
	{
		this.plant.plantState = this.plant.sadState;
	}

	public override void GetWorse()
	{
		this.plant.plantState = this.plant.deadState;
	}
}
