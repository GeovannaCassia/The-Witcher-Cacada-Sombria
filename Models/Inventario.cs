using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgGame.Models
{
    public class Inventario
    {
        private readonly int _capacidadeMaxima = 10;
        private readonly List<Item> _itens = new();

        public int CapacidadeMaxima => _capacidadeMaxima;

        public IReadOnlyList<Item> Itens => _itens;

        public void AdicionarItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_itens.Count >= _capacidadeMaxima)
                throw new InvalidOperationException("Inventário cheio.");

            _itens.Add(item);
        }

        public bool RemoverItem(string nomeItem)
        {
            Item item = _itens.FirstOrDefault(i => i.Nome.Equals(nomeItem, StringComparison.OrdinalIgnoreCase));
            if (item == null) return false;

            item.Quantidade--;
            if (item.Quantidade <= 0)
                _itens.Remove(item);

            return true;
        }

        public Item? BuscarItem(string nomeItem)
        {
            return _itens.FirstOrDefault(i => i.Nome.Equals(nomeItem, StringComparison.OrdinalIgnoreCase));
        }

        public void ListarItens()
        {
            if (_itens.Count == 0)
            {
                Console.WriteLine("Inventário vazio.");
                return;
            }

            foreach (var item in _itens)
            {
                Console.WriteLine($"- {item.Nome} ({item.Tipo}) x{item.Quantidade}");
            }
        }
    }
}