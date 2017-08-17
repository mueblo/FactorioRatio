namespace FactorioRatio.Gui.ViewModels
{
    public class MathUtils
    {
        public static int gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int lcm(int a, int b)
        {
            return (a / gcf(a, b)) * b;
        }
    }
}