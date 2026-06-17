using RpgGame.Enums;

namespace RpgGame.Models
{
    public class Mago : Jogador
    {
        public Mago(string nome, RacaPersonagem raca)
            : base(nome, 90, 10, ClassePersonagem.Mago, raca)
        {
        }

        public override int Atacar()
        {
            return Forca + 7;
        }

        public override void ReceberDano(int dano)
        {
            Vida -= dano;
        }
    }
}