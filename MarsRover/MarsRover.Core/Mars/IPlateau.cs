namespace MarsRover.Core.Mars
{
    public interface IPlateau
    {
        void Define(int width, int height);

        int Height();

        int Width();
    }
}