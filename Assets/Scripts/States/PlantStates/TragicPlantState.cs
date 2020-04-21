using UnityEngine;

public class TragicPlantState : PlantState
{
	public TragicPlantState(PlantController plant, Sprite icon, Color color) : base(plant, icon, color)
	{

	}

	public override void GetBetter()
	{
		this.plant.TransitionToState(this.plant.sadState);
	}

	public override void GetWorse()
	{
		this.plant.TransitionToState(this.plant.deadState);
	}
}
