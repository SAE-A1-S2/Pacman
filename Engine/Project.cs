namespace Engine;

public class Project
{
	public static void Main()
	{
		GameManager gm = new();
		gm.Player.SetPlayerName("Cédric");
		gm.StepPlayer(utils.Direction.RIGHT);
	}
}
