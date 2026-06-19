using TheWitcher.Enums;

namespace TheWitcher.Models
{
    // Entidade Item conforme enunciado
    public class Item
    {
        // Atributos privados — Encapsulamento
        private string _nome;
        private TipoItem _tipo;
        private int _quantidadeCura;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public TipoItem Tipo
        {
            get { return _tipo; }
        }

        // Quantidade de vida restaurada (aplicável apenas para consumíveis de cura)
        public int QuantidadeCura
        {
            get { return _quantidadeCura; }
        }

        public Item(string nome, TipoItem tipo, int quantidadeCura = 0)
        {
            _nome          = nome;
            _tipo          = tipo;
            _quantidadeCura = quantidadeCura;
        }

        public override string ToString()
        {
            if (_tipo == TipoItem.Consumivel && _quantidadeCura > 0)
                return $"{_nome} (Cura: +{_quantidadeCura} vida)";
            return $"{_nome} ({_tipo})";
        }
    }
}
