using MarsRover.Core.Mars;

namespace MarsRover.Core.CommandCenter
{
    public class CreatePlateauCommand : ICommand
    {
        private IPlateau Plateau { get; set; }

        private string Command { get; set; }

        public CreatePlateauCommand(IPlateau plateau, string command)
        {
            Plateau = plateau;

            Command = command;
        }

        public void ExecuteCommand()
        {
            ParseCommand(Command, out var width, out var height);

            Plateau.Define(width, height);
        }

        private void ParseCommand(string command, out int width, out int height)
        {
            var splitCommand = command.Split(' ');

            width = int.Parse(splitCommand[0]);

            height = int.Parse(splitCommand[1]);
        }
    }
}