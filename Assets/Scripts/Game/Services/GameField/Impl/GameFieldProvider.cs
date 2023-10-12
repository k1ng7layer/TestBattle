using Game.Views;

namespace Game.Services.GameField.Impl
{
    public class GameFieldProvider : IGameFieldProvider
    {
        public GameFieldProvider(GameFieldView gameField)
        {
            GameField = gameField;
        }

        public GameFieldView GameField { get; }
    }
}