namespace MarsRover.Core.CommandCenter
{
    public class CommandCenter
    {
        public void SendCommand(ICommand command)
        {
            command.ExecuteCommand();
        }
    }
}