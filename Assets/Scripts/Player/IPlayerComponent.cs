public interface IPlayerComponent
{
    public bool ActionEnabled { get; set; }

    public void Initialize();
    public void Conclude();
}
