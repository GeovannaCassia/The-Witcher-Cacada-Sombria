using System;
using TheWitcher.Interfaces;

// Classe abstrata obrigatória conforme enunciado
// Serve de base para Jogador e Inimigo
public abstract class PersonagemBase : IAtacavel
{
    // ─── Atributos privados (Encapsulamento) ───────────────────
    private string _nome;
    private int _vida;
    private int _vidaMaxima;
    private int _forca;
    private int _nivel;

    // ─── Propriedades públicas ──────────────────────────────────
    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public int Vida
    {
        // Impede que a vida fique negativa
        get { return _vida; }
        set { _vida = value < 0 ? 0 : value; }
    }

    public int VidaMaxima
    {
        get { return _vidaMaxima; }
        set { _vidaMaxima = value; }
    }

    public int Forca
    {
        get { return _forca; }
        set { _forca = value; }
    }

    public int Nivel
    {
        get { return _nivel; }
        set { _nivel = value; }
    }

    // Propriedade somente-leitura para verificar se está vivo
    public bool Vivo
    {
        get { return _vida > 0; }
    }

    // ─── Construtor ─────────────────────────────────────────────
    protected PersonagemBase(string nome, int vidaMaxima, int forca, int nivel = 1)
    {
        _nome       = nome;
        _vidaMaxima = vidaMaxima;
        _vida       = vidaMaxima;  // começa com vida cheia
        _forca      = forca;
        _nivel      = nivel;
    }

    // ─── Métodos Abstratos (Abstração + Polimorfismo) ───────────
    // Cada subclasse DEVE implementar sua forma de atacar
    public abstract void Atacar(PersonagemBase alvo);

    // Cada subclasse DEVE implementar como recebe dano
    public abstract void ReceberDano(int dano);

    // ─── Método virtual — pode ser sobrescrito ──────────────────
    public virtual void ExibirStatus()
    {
        Console.WriteLine($"  {_nome} | Vida: {_vida}/{_vidaMaxima} | Forca: {_forca} | Nivel: {_nivel}");
    }
}
