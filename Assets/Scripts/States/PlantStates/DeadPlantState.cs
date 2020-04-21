using UnityEngine;

public class DeadPlantState : PlantState
{
	public DeadPlantState(PlantController plant, Sprite icon, Color color) : base(plant, icon, color)
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
