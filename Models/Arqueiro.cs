using RpgGame.Enums;

namespace RpgGame.Models
{
    public class Arqueiro : Jogador
    {
        public Arqueiro(string nome, RacaPersonagem raca)
            : base(nome, 100, 11, ClassePersonagem.Arqueiro, raca)
        {
        }

        public override int Atacar()
        {
            return Forca + 6;
        }

        public override void ReceberDano(int dano)
        {
            Vida -= dano;
        }
    }
}