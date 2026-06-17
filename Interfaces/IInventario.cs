using RpgGame.Models;

namespace RpgGame.Interfaces
{
    public interface IInventario
    {
        void AdicionarItem(Models.Item item);
        bool RemoverItem(string nomeItem);
    }
}