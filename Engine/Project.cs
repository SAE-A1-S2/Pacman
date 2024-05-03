namespace Engine;

public class Project
{
  public static void Main()
  {
    GameManager gm = new();
    gm.m_Player.SetPlayerName("Cédric");
    gm.Run();
  }
}
