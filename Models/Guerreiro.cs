using RpgGame.Enums;

namespace RpgGame.Models
{
    public class Guerreiro : Jogador
    {
        public Guerreiro(string nome, RacaPersonagem raca)
            : base(nome, 120, 12, ClassePersonagem.Guerreiro, raca)
        {
        }

        public override int Atacar()
        {
            return Forca + 5;
        }

        public override void ReceberDano(int dano)
        {
            Vida -= dano;
        }
    }
}