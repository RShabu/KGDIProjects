public interface IState
{
    bool IsRunning { get; }
    bool UseFixedUpdate { get; }

    void Enter();
    void Exit();

    void Update();
   
}