public interface IPauseManager
{
    bool IsPaused { get; }
    void TogglePauseMenu();
}
