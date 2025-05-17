public class PlayerStateContainer
{
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public WalkState WalkState { get; private set; }
    public RollState RollState { get; private set; }


    public PlayerStateContainer(PlayerController player,StateMachine stateMachine)
    {
        IdleState = new IdleState(player, stateMachine);
        RunState = new RunState(player, stateMachine);
        WalkState = new WalkState(player, stateMachine);
        RollState = new RollState(player, stateMachine);
    }
}