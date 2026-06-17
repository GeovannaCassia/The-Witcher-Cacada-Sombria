using RpgGame.Enums;

namespace RpgGame.Models
{
    public abstract class Jogador : PersonagemBase
    {
        private readonly Inventario _inventario;
        private readonly ClassePersonagem _classe;
        private readonly RacaPersonagem _raca;

        protected Jogador(string nome, int vidaMaxima, int forca, ClassePersonagem classe, RacaPersonagem raca)
            : base(nome, vidaMaxima, forca)
        {
            _classe = classe;
            _raca = raca;
            _inventario = new Inventario();
        }

        public ClassePersonagem Classe => _classe;
        public RacaPersonagem Raca => _raca;
        public Inventario Inventario => _inventario;

        public void AdicionarItem(Item item) => _inventario.AdicionarItem(item);

        public bool UsarItemDeCura()
        {
            var item = _inventario.BuscarItem("Poção de Cura");
            if (item == null || item.Tipo != TipoItem.Consumivel) return false;

            Curar(item.Cura);
            _inventario.RemoverItem(item.Nome);
            return true;
        }

        public void EvoluirAposMissao()
        {
            VidaMaxima *= 2;
            Forca *= 2;
            Vida = VidaMaxima;
            Nivel++;
        }
    }
}