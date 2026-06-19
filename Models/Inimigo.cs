using System;

// Inimigo herda de PersonagemBase (Herança)
// Entidade obrigatória do enunciado: Nome, Vida, Forca
public class Inimigo : PersonagemBase
{
    public Inimigo(string nome, int vidaMaxima, int forca)
        : base(nome, vidaMaxima, forca, nivel: 1)
    {
    }

    // Polimorfismo: implementação do ataque do inimigo
    public override void Atacar(PersonagemBase alvo)
    {
        Random rnd = new Random();
        int dano = Forca + rnd.Next(0, 5);   // variação pequena de dano

        Console.WriteLine($"\n  [{Nome}] ataca {alvo.Nome}!");
        alvo.ReceberDano(dano);
    }

    // Polimorfismo: como o inimigo recebe dano
    public override void ReceberDano(int dano)
    {
        Vida -= dano;
        Console.WriteLine($"  [{Nome}] recebeu {dano} de dano. Vida: {Vida}/{VidaMaxima}");
    }

    public override void ExibirStatus()
    {
        Console.WriteLine($"  Inimigo: {Nome} | Vida: {Vida}/{VidaMaxima} | Forca: {Forca}");
    }
}
