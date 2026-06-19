namespace TheWitcher.Interfaces
{
    // Interface que define o contrato de combate para qualquer entidade
    public interface IAtacavel
    {
        // Todo personagem que pode atacar deve implementar este método
        void Atacar(PersonagemBase alvo);

        // Todo personagem que pode receber dano deve implementar este método
        void ReceberDano(int dano);
    }
}
