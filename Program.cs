using System;
using TheWitcher.Enums;
using TheWitcher.Models;
using TheWitcher.Services;
using TheWitcher.Utils;

class Program
{
    static Jogador jogador;
    static SistemaMissoes sistemaMissoes;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Display.ExibirTitulo();
        Display.Pausar("\n  Pressione ENTER para comecar...");
        MenuInicial();
    }

    // ─── Menu Inicial ────────────────────────────────────────────
    static void MenuInicial()
    {
        while (true)
        {
            Display.ExibirTitulo();
            Console.WriteLine("\n  1 - Novo Jogo");
            if (SistemaArquivo.ExisteSave())
                Console.WriteLine("  2 - Continuar Jogo Salvo");
            Console.WriteLine("  3 - Ver Diagrama de Classes");
            Console.WriteLine("  0 - Sair");
            Console.Write("\n  Escolha: ");

            string op = Console.ReadLine();

            if (op == "1")
            {
                // Deleta save anterior ao iniciar novo jogo
                SistemaArquivo.DeletarSave();
                NovoJogo();
            }
            else if (op == "2" && SistemaArquivo.ExisteSave())
            {
                CarregarJogo();
            }
            else if (op == "3")
            {
                Display.ExibirDiagramaClasses();
                Display.Pausar();
            }
            else if (op == "0")
            {
                Console.WriteLine("\n  Ate logo!\n");
                return;
            }
        }
    }

    // ─── Novo Jogo ───────────────────────────────────────────────
    static void NovoJogo()
    {
        jogador = CriacaoPersonagem.Criar();
        sistemaMissoes = new SistemaMissoes(jogador);
        LoopJogo();
    }

    // ─── Carregar Jogo Salvo ─────────────────────────────────────
    static void CarregarJogo()
    {
        var dados = SistemaArquivo.Carregar();
        if (dados == null)
        {
            Console.WriteLine("\n  [!] Nao foi possivel carregar o save.");
            Display.Pausar();
            return;
        }

        // Recria o jogador com a classe correta
        RacaPersonagem raca =
            dados.Raca == "Elfo" ? RacaPersonagem.Elfo :
            dados.Raca == "Anao" ? RacaPersonagem.Anao :
                                   RacaPersonagem.Humano;

        if (dados.Classe == "Mago")
            jogador = new Mago(dados.Nome, raca);
        else if (dados.Classe == "Arqueiro")
            jogador = new Arqueiro(dados.Nome, raca);
        else
            jogador = new Guerreiro(dados.Nome, raca);

        // Restaura atributos salvos
        jogador.Nivel     = dados.Nivel;
        jogador.VidaMaxima = dados.VidaMaxima;
        jogador.Vida      = dados.Vida;
        jogador.Forca     = dados.Forca;

        // Restaura inventário salvo
        jogador.Inventario.Itens.Clear();
        for (int i = 0; i < dados.NomesItens.Count; i++)
        {
            TipoItem tipo = dados.TiposItens[i] == "Equipamento"
                ? TipoItem.Equipamento
                : TipoItem.Consumivel;
            jogador.Inventario.AdicionarItem(
                new Item(dados.NomesItens[i], tipo, dados.CurasItens[i])
            );
        }

        // Recria sistema de missões e avança para a missão correta
        sistemaMissoes = new SistemaMissoes(jogador, dados.MissaoAtual);

        Console.WriteLine($"\n  [SAVE] Bem-vindo de volta, {jogador.Nome}!");
        Console.WriteLine($"  Missao: {dados.MissaoAtual + 1}/3 | Vida: {jogador.Vida}/{jogador.VidaMaxima} | Forca: {jogador.Forca}");
        Display.Pausar();

        LoopJogo();
    }

    // ─── Loop principal do jogo ───────────────────────────────────
    static void LoopJogo()
    {
        bool jogando = true;
        while (jogando && !sistemaMissoes.JogoConcluido())
            jogando = MenuJogo();

        if (sistemaMissoes.JogoConcluido())
        {
            SistemaArquivo.DeletarSave(); // limpa save ao vencer
            Display.ExibirVitoria(jogador.Nome);
        }
        else
        {
            Display.ExibirDerrota(jogador.Nome);
        }

        Display.Pausar();
    }

    // ─── Menu durante o jogo ─────────────────────────────────────
    static bool MenuJogo()
    {
        Display.ExibirTitulo();
        Console.WriteLine($"\n  Jogador : {jogador.Nome} ({jogador.Classe} / {jogador.Raca})");
        Console.WriteLine($"  Vida    : {jogador.Vida}/{jogador.VidaMaxima} | Forca: {jogador.Forca}");
        Console.WriteLine($"  Missao  : {sistemaMissoes.MissaoAtual + 1}/3");
        Display.Separador();
        Console.WriteLine("\n  1 - Ver Missoes");
        Console.WriteLine("  2 - Iniciar Proxima Missao");
        Console.WriteLine("  3 - Ver Status do Personagem");
        Console.WriteLine("  4 - Ver Inventario");
        Console.WriteLine("  5 - Salvar Jogo");
        Console.WriteLine("  0 - Abandonar (voltar ao menu)");
        Console.Write("\n  Escolha: ");

        string op = Console.ReadLine();

        if (op == "1")
        {
            sistemaMissoes.ExibirMissoes();
            Display.Pausar();
            return true;
        }
        else if (op == "2")
        {
            bool vivo = sistemaMissoes.IniciarProximaMissao();
            return vivo;
        }
        else if (op == "3")
        {
            Console.Clear();
            jogador.ExibirStatus();
            Display.Pausar();
            return true;
        }
        else if (op == "4")
        {
            Console.Clear();
            jogador.Inventario.Exibir();
            Display.Pausar();
            return true;
        }
        else if (op == "5")
        {
            SistemaArquivo.Salvar(jogador, sistemaMissoes.MissaoAtual);
            Display.Pausar();
            return true;
        }
        else if (op == "0")
        {
            return false;
        }

        return true;
    }
}
