using Engine.utils;

namespace Engine;

public class Project
{
	public static void Main()
	{
		GameManager gm = new(GameMode.STORY);
		gm.Player.SetPlayerName("Cédric");
		gm.StepPlayer(Direction.RIGHT);
	}
}
