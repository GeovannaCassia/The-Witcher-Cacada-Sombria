using RpgGame.Enums;

namespace RpgGame.Models
{
    public class Item
    {
        private string _nome;
        private TipoItem _tipo;
        private int _cura;
        private int _quantidade;

        public Item(string nome, TipoItem tipo, int cura, int quantidade)
        {
            _nome = nome;
            _tipo = tipo;
            _cura = cura;
            _quantidade = quantidade > 0 ? quantidade : 1;
        }

        public string Nome => _nome;
        public TipoItem Tipo => _tipo;
        public int Cura => _cura;
        public int Quantidade
        {
            get => _quantidade;
            set => _quantidade = value > 0 ? value : 0;
        }

        public bool Consumivel => Tipo == TipoItem.Consumivel;
    }
}