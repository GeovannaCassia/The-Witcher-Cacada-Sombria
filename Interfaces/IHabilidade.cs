namespace TheWitcher.Interfaces
{
    // Interface para habilidades especiais — cada classe implementa a sua
    public interface IHabilidade
    {
        string NomeHabilidade { get; }
        string DescricaoHabilidade { get; }

        // Executa a habilidade especial no alvo
        void UsarHabilidade(PersonagemBase alvo);
    }
}
