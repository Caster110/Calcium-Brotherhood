public interface IInputHandler
{
    public bool IsActive { get; }
    public bool TryDisable();
}