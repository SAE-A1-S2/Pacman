namespace Engine
{
  public interface IEnemyBehavior
  {
    void Move();
  }

  public class ChaserBehavior : IEnemyBehavior
  {
    public void Move() { }
  }

  public class AmbusherBehavior : IEnemyBehavior
  {
    public void Move() { }
  }

  public class WhimsicalBehavior : IEnemyBehavior
  {
    public void Move() { }
  }

  public class WandererBehavior : IEnemyBehavior
  {
    public void Move() { }
  }
}
