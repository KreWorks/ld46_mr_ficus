using UnityEngine;

public class NeutralPlantState : PlantState
{
	public NeutralPlantState(PlantController plant, Sprite icon, Color color) : base(plant, icon, color)
	{

	}

	public override void GetBetter()
	{
		this.plant.TransitionToState(this.plant.pleaseState);
	}

	public override void GetWorse()
	{
		this.plant.TransitionToState(this.plant.sadState);
	}
}
