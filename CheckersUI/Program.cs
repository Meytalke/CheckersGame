namespace CheckersUI
{
    public class Program
    {
        public static void Main()
        {
            FormGame formGame = new FormGame();

            if (formGame.ShowGameSettings())
            {
                formGame.ShowDialog();
            }
        }
    }
}
