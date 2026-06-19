using System;

namespace TheWitcher.Utils
{
    // Classe utilitária com métodos de exibição reutilizáveis
    public static class Display
    {
        // Exibe o cabeçalho principal do jogo
        public static void ExibirTitulo()
        {
            Console.Clear();
            Console.WriteLine("\n  ╔══════════════════════════════════════════╗");
            Console.WriteLine("  ║          THE WITCHER  -  RPG POO         ║");
            Console.WriteLine("  ║      Trabalho POO C# - PUC Minas         ║");
            Console.WriteLine("  ╚══════════════════════════════════════════╝");
        }

        // Exibe uma linha separadora
        public static void Separador()
        {
            Console.WriteLine("  ──────────────────────────────────────────");
        }

        // Pausa e aguarda ENTER do usuário
        public static void Pausar(string mensagem = "  Pressione ENTER para continuar...")
        {
            Console.WriteLine(mensagem);
            Console.ReadLine();
        }

        // Exibe mensagem de vitória final
        public static void ExibirVitoria(string nomeJogador)
        {
            Console.Clear();
            Console.WriteLine("\n  ╔══════════════════════════════════════════╗");
            Console.WriteLine("  ║           PARABENS, BRUXO!               ║");
            Console.WriteLine("  ║   Voce concluiu todas as 3 missoes!      ║");
            Console.WriteLine("  ╚══════════════════════════════════════════╝");
            Console.WriteLine($"\n  {nomeJogador} provou ser o maior cacador do Continente.");
            Console.WriteLine("\n  FIM DE JOGO - Obrigado por jogar!");
            Separador();
        }

        // Exibe mensagem de derrota
        public static void ExibirDerrota(string nomeJogador)
        {
            Console.Clear();
            Console.WriteLine("\n  ╔══════════════════════════════════════════╗");
            Console.WriteLine("  ║              FIM DE JOGO                 ║");
            Console.WriteLine("  ╚══════════════════════════════════════════╝");
            Console.WriteLine($"\n  {nomeJogador} foi derrotado...");
            Console.WriteLine("  As trevas venceram desta vez.");
            Separador();
        }

        // Formata e exibe o diagrama textual das classes (entregável 1 do enunciado)
        public static void ExibirDiagramaClasses()
        {
            Console.Clear();
            Console.WriteLine(@"
  ╔══════════════════════════════════════════════════════════════╗
  ║              DIAGRAMA TEXTUAL DAS CLASSES                    ║
  ╚══════════════════════════════════════════════════════════════╝

  [Interface] IAtacavel               [Interface] IHabilidade
  ┌──────────────────────┐            ┌────────────────────────┐
  │ + Atacar(alvo)       │            │ + NomeHabilidade       │
  │ + ReceberDano(dano)  │            │ + DescricaoHabilidade  │
  └──────────┬───────────┘            │ + UsarHabilidade(alvo) │
             │                        └────────────┬───────────┘
             │                                     │
             ▼                                     │
  [Abstract] PersonagemBase ◄──────────────────────┘
  ┌─────────────────────────────────────────────────┐
  │ - _nome : string                                │
  │ - _vida : int                                   │
  │ - _vidaMaxima : int                             │
  │ - _forca : int                                  │
  │ - _nivel : int                                  │
  │ + Vivo : bool (readonly)                        │
  │ + Atacar(alvo) [abstract]                       │
  │ + ReceberDano(dano) [abstract]                  │
  │ + ExibirStatus() [virtual]                      │
  └──────────┬──────────────────────────────────────┘
             │
     ┌───────┴────────┐
     │                │
     ▼                ▼
  [Abstract] Jogador  Inimigo
  ┌──────────────────┐  ┌────────────────────────┐
  │ - _classe        │  │ + Atacar() [override]  │
  │ - _raca          │  │ + ReceberDano()[overr.] │
  │ - _inventario    │  └────────────────────────┘
  │ + AplicarProg.() │
  │ + UsarItemCura() │
  │ + UsarHabili.()  │
  └──────┬───────────┘
         │
   ┌─────┼──────┐
   ▼     ▼      ▼
Guerreiro Mago Arqueiro
(override Atacar + UsarHabilidade)

  [Model] Item              [Model] Inventario
  ┌────────────────┐        ┌────────────────────────────┐
  │ - _nome        │        │ - _itens : List<Item>      │
  │ - _tipo        │◄───────│ - CAPACIDADE_MAXIMA = 10   │
  │ - _qtdCura     │        │ + AdicionarItem()          │
  └────────────────┘        │ + RetirarPrimeiroCuravel() │
                            └────────────────────────────┘

  [Model] Missao
  ┌─────────────────────────────┐
  │ - _nome : string            │
  │ - _descricao : string       │
  │ - _status : StatusMissao    │
  │ - _inimigos : List<Inimigo> │
  └─────────────────────────────┘

  [Enum] ClassePersonagem : Guerreiro | Mago | Arqueiro
  [Enum] RacaPersonagem   : Humano | Elfo | Anao
  [Enum] StatusMissao     : Disponivel | EmAndamento | Concluida
  [Enum] TipoItem         : Consumivel | Equipamento
            ");
        }
    }
}
