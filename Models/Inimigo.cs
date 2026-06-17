namespace RpgGame.Models
{
    public class Inimigo : PersonagemBase
    {
        public Inimigo(string nome, int vida, int forca)
            : base(nome, vida, forca)
        {
        }

        public override int Atacar()
        {
            return Forca;
        }

        public override void ReceberDano(int dano)
        {
            Vida -= dano;
        }
    }
}