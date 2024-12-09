using CodeBase.Utils.SmartDebug;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly DSender _sender = new("GameLoopState");

        public void Enter()
        {
            DLogger.Message(_sender)
                .WithText("Entering GameLoopState. Game cycle started.")
                .Log();
        }

        public void Exit()
        {
            DLogger.Message(_sender)
                .WithText("Exiting GameLoopState.")
                .Log();
        }
    }
}