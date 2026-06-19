using System;
using TheWitcher.Enums;

namespace TheWitcher.Services
{
    // Serviço responsável pela criação interativa do personagem
    public static class CriacaoPersonagem
    {
        public static Jogador Criar()
        {
            Console.Clear();
            Console.WriteLine("\n  ╔═══════════════════════════════════╗");
            Console.WriteLine("  ║      CRIACAO DE PERSONAGEM        ║");
            Console.WriteLine("  ╚═══════════════════════════════════╝");

            // ─── Nome ────────────────────────────────────────────────
            Console.Write("\n  Digite o nome do seu personagem: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome)) nome = "Heroi";

            // ─── Classe ──────────────────────────────────────────────
            Console.WriteLine("\n  Escolha sua CLASSE:");
            Console.WriteLine("  1 - Guerreiro (Vida: 120 | Forca: 18 | Hab: Golpe Poderoso)");
            Console.WriteLine("  2 - Mago      (Vida:  80 | Forca: 25 | Hab: Bola de Fogo)");
            Console.WriteLine("  3 - Arqueiro  (Vida: 100 | Forca: 20 | Hab: Tiro Certeiro)");
            Console.Write("  Escolha: ");
            string opClasse = Console.ReadLine();

            // ─── Raça ────────────────────────────────────────────────
            Console.WriteLine("\n  Escolha sua RACA:");
            Console.WriteLine("  1 - Humano  (+10 vida)");
            Console.WriteLine("  2 - Elfo    (+3 forca)");
            Console.WriteLine("  3 - Anao    (+20 vida, +2 forca)");
            Console.Write("  Escolha: ");
            string opRaca = Console.ReadLine();

            RacaPersonagem raca =
                opRaca == "2" ? RacaPersonagem.Elfo :
                opRaca == "3" ? RacaPersonagem.Anao :
                                RacaPersonagem.Humano;

            // ─── Criação do objeto conforme classe escolhida ─────────
            Jogador jogador;
            if (opClasse == "2")
                jogador = new Mago(nome, raca);
            else if (opClasse == "3")
                jogador = new Arqueiro(nome, raca);
            else
                jogador = new Guerreiro(nome, raca);   // padrão: Guerreiro

            Console.WriteLine("\n  ─── Personagem Criado! ───");
            jogador.ExibirStatus();
            Console.WriteLine($"\n  Habilidade: {jogador.NomeHabilidade}");
            Console.WriteLine($"  Descricao : {jogador.DescricaoHabilidade}");
            Console.WriteLine("\n  Pressione ENTER para continuar...");
            Console.ReadLine();

            return jogador;
        }
    }
}
