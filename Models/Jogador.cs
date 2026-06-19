using System;
using TheWitcher.Enums;
using TheWitcher.Interfaces;
using TheWitcher.Models;

// Jogador herda de PersonagemBase (Herança)
// Implementa IHabilidade para habilidades especiais (Polimorfismo)
public abstract class Jogador : PersonagemBase, IHabilidade
{
    // ─── Atributos adicionais do jogador ───────────────────────
    private ClassePersonagem _classe;
    private RacaPersonagem _raca;
    private Inventario _inventario;

    public ClassePersonagem Classe  { get { return _classe; } }
    public RacaPersonagem Raca      { get { return _raca; } }
    public Inventario Inventario    { get { return _inventario; } }

    // ─── Propriedades da interface IHabilidade ──────────────────
    public abstract string NomeHabilidade { get; }
    public abstract string DescricaoHabilidade { get; }

    protected Jogador(string nome, ClassePersonagem classe, RacaPersonagem raca,
                      int vidaMaxima, int forca)
        : base(nome, vidaMaxima + BonusVidaRaca(raca), forca + BonusForcaRaca(raca))
    {
        _classe     = classe;
        _raca       = raca;
        _inventario = new Inventario();

        // Itens iniciais no inventário
        _inventario.AdicionarItem(new Item("Pocao de Vida", TipoItem.Consumivel, quantidadeCura: 40));
        _inventario.AdicionarItem(new Item("Pocao de Vida", TipoItem.Consumivel, quantidadeCura: 40));
    }

    // ─── Bônus de atributos por Raça ───────────────────────────
    private static int BonusVidaRaca(RacaPersonagem raca)
    {
        if (raca == RacaPersonagem.Anao)  return 20;   // Anão: +20 vida
        if (raca == RacaPersonagem.Elfo)  return 0;
        return 10;                                       // Humano: +10 vida
    }

    private static int BonusForcaRaca(RacaPersonagem raca)
    {
        if (raca == RacaPersonagem.Elfo)  return 3;    // Elfo: +3 forca
        if (raca == RacaPersonagem.Anao)  return 2;    // Anão: +2 forca
        return 0;
    }

    // ─── Implementação de ReceberDano (abstract herdado) ────────
    public override void ReceberDano(int dano)
    {
        Vida -= dano;
        Console.WriteLine($"  [{Nome}] recebeu {dano} de dano. Vida: {Vida}/{VidaMaxima}");
    }

    // ─── Usar item de cura do inventário ────────────────────────
    public void UsarItemCura()
    {
        Item item = _inventario.RetirarPrimeiroCuravel();
        if (item == null)
        {
            Console.WriteLine("  [!] Sem itens de cura no inventario!");
            return;
        }
        int vidaAntes = Vida;
        Vida = Math.Min(Vida + item.QuantidadeCura, VidaMaxima);
        Console.WriteLine($"  [{Nome}] usou {item.Nome}. Vida recuperada: +{Vida - vidaAntes}. Vida: {Vida}/{VidaMaxima}");
    }

    // ─── Progressão ao concluir missão ──────────────────────────
    // Dobra vida máxima e força conforme regra do enunciado
    public void AplicarProgressao()
    {
        VidaMaxima *= 2;
        Vida        = VidaMaxima;    // restaura a vida ao dobrar
        Forca      *= 2;
        Console.WriteLine($"\n  [PROGRESSAO] {Nome} ficou mais forte!");
        Console.WriteLine($"  Vida Maxima: {VidaMaxima} | Forca: {Forca}");
    }

    // Método abstrato de habilidade especial (IHabilidade)
    public abstract void UsarHabilidade(PersonagemBase alvo);

    public override void ExibirStatus()
    {
        Console.WriteLine($"\n  Jogador : {Nome} ({Classe} / {Raca})");
        Console.WriteLine($"  Vida    : {Vida}/{VidaMaxima}");
        Console.WriteLine($"  Forca   : {Forca} | Nivel: {Nivel}");
        Console.WriteLine($"  Hab.    : {NomeHabilidade}");
    }
}

// ═══════════════════════════════════════════════════════════════
// GUERREIRO — ataque físico e habilidade: Golpe Poderoso
// ═══════════════════════════════════════════════════════════════
public class Guerreiro : Jogador
{
    public override string NomeHabilidade       { get { return "Golpe Poderoso"; } }
    public override string DescricaoHabilidade  { get { return "Golpe critico que causa o dobro do dano de Forca"; } }

    public Guerreiro(string nome, RacaPersonagem raca)
        : base(nome, ClassePersonagem.Guerreiro, raca, vidaMaxima: 120, forca: 18) { }

    // Polimorfismo: ataque padrão do Guerreiro
    public override void Atacar(PersonagemBase alvo)
    {
        Random rnd = new Random();
        int dano = Forca + rnd.Next(0, 8);
        Console.WriteLine($"\n  [{Nome}] ataca com a espada!");
        alvo.ReceberDano(dano);
    }

    // Polimorfismo: habilidade especial do Guerreiro
    public override void UsarHabilidade(PersonagemBase alvo)
    {
        int dano = Forca * 2;
        Console.WriteLine($"\n  [{Nome}] usa GOLPE PODEROSO!");
        alvo.ReceberDano(dano);
    }
}

// ═══════════════════════════════════════════════════════════════
// MAGO — dano magico e habilidade: Bola de Fogo
// ═══════════════════════════════════════════════════════════════
public class Mago : Jogador
{
    public override string NomeHabilidade       { get { return "Bola de Fogo"; } }
    public override string DescricaoHabilidade  { get { return "Dano magico fixo de 50 que ignora variacao de sorte"; } }

    public Mago(string nome, RacaPersonagem raca)
        : base(nome, ClassePersonagem.Mago, raca, vidaMaxima: 80, forca: 25) { }

    public override void Atacar(PersonagemBase alvo)
    {
        Random rnd = new Random();
        int dano = Forca + rnd.Next(0, 12);   // mago tem variacao maior
        Console.WriteLine($"\n  [{Nome}] lanca um arcano!");
        alvo.ReceberDano(dano);
    }

    public override void UsarHabilidade(PersonagemBase alvo)
    {
        int dano = 50;   // dano fixo
        Console.WriteLine($"\n  [{Nome}] lanca BOLA DE FOGO! (dano fixo: 50)");
        alvo.ReceberDano(dano);
    }
}

// ═══════════════════════════════════════════════════════════════
// ARQUEIRO — equilibrado e habilidade: Tiro Certeiro (critico)
// ═══════════════════════════════════════════════════════════════
public class Arqueiro : Jogador
{
    public override string NomeHabilidade       { get { return "Tiro Certeiro"; } }
    public override string DescricaoHabilidade  { get { return "Garantia de critico: dano = Forca x 1.8"; } }

    public Arqueiro(string nome, RacaPersonagem raca)
        : base(nome, ClassePersonagem.Arqueiro, raca, vidaMaxima: 100, forca: 20) { }

    public override void Atacar(PersonagemBase alvo)
    {
        Random rnd = new Random();
        int dano = Forca + rnd.Next(0, 10);
        Console.WriteLine($"\n  [{Nome}] dispara uma flecha!");
        alvo.ReceberDano(dano);
    }

    public override void UsarHabilidade(PersonagemBase alvo)
    {
        int dano = (int)(Forca * 1.8);
        Console.WriteLine($"\n  [{Nome}] usa TIRO CERTEIRO! (dano: {dano})");
        alvo.ReceberDano(dano);
    }
}
