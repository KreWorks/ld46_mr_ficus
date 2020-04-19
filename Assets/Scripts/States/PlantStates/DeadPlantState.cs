using UnityEngine;

public class DeadPlantState : PlantState
{
	public DeadPlantState(PlantController plant, Sprite icon) : base(plant, icon)
	{

	}

	public override void GetBetter()
	{
		return;
	}

	public override void GetWorse()
	{
		return;
	}
}
