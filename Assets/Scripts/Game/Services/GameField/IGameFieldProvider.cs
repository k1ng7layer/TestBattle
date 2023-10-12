using Game.Views;

namespace Game.Services.GameField
{
    public interface IGameFieldProvider
    {
        GameFieldView GameField { get; }
    }
}