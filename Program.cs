using System;
using RpgGame.Enums;
using RpgGame.Models;
using RpgGame.Services;

namespace RpgGame
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "RPG - POO";

            Console.WriteLine("=== RPG POR TURNOS ===");
            Console.WriteLine("Crie seu personagem:");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "Herói";

            ClassePersonagem classe = EscolherClasse();
            RacaPersonagem raca = EscolherRaca();

            Jogador jogador = CriarJogador(nome, classe, raca);

            jogador.AdicionarItem(new Item("Poção de Cura", TipoItem.Consumivel, 30, 1));

            Console.Clear();
            Console.WriteLine($"Personagem criado: {jogador}");
            Console.WriteLine();

            var missaoService = new MissaoService();
            var combateService = new CombateService();

            var missoes = missaoService.CriarMissoes();

            foreach (var missao in missoes)
            {
                Console.WriteLine($"\n=== {missao.Nome} ===");
                Console.WriteLine(missao.Descricao);
                Console.WriteLine();

                missao.MarcarComoEmAndamento();

                foreach (var inimigo in missao.Inimigos)
                {
                    Console.WriteLine($"Inimigo da rodada: {inimigo.Nome}");
                    bool venceu = combateService.IniciarCombate(jogador, inimigo);

                    if (!venceu)
                    {
                        Console.WriteLine("\nVocê foi derrotado...");
                        return;
                    }

                    Console.WriteLine();
                }

                missao.MarcarComoConcluida();

                jogador.EvoluirAposMissao();

                Console.WriteLine($"\nMissão concluída! Seu personagem evoluiu.");
                Console.WriteLine($"Nível: {jogador.Nivel}");
                Console.WriteLine($"Vida Máxima: {jogador.VidaMaxima}");
                Console.WriteLine($"Força: {jogador.Forca}");
                Console.WriteLine($"Vida Atual: {jogador.Vida}");
                Console.WriteLine();

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("\nParabéns! Você concluiu as 3 missões e venceu o jogo.");
        }

        private static ClassePersonagem EscolherClasse()
        {
            while (true)
            {
                Console.WriteLine("\nEscolha sua classe:");
                Console.WriteLine("1 - Guerreiro");
                Console.WriteLine("2 - Mago");
                Console.WriteLine("3 - Arqueiro");

                string input = Console.ReadLine();

                if (input == "1") return ClassePersonagem.Guerreiro;
                if (input == "2") return ClassePersonagem.Mago;
                if (input == "3") return ClassePersonagem.Arqueiro;

                Console.WriteLine("Opção inválida.");
            }
        }

        private static RacaPersonagem EscolherRaca()
        {
            while (true)
            {
                Console.WriteLine("\nEscolha sua raça:");
                Console.WriteLine("1 - Humano");
                Console.WriteLine("2 - Elfo");
                Console.WriteLine("3 - Anão");

                string input = Console.ReadLine();

                if (input == "1") return RacaPersonagem.Humano;
                if (input == "2") return RacaPersonagem.Elfo;
                if (input == "3") return RacaPersonagem.Anao;

                Console.WriteLine("Opção inválida.");
            }
        }

        private static Jogador CriarJogador(string nome, ClassePersonagem classe, RacaPersonagem raca)
        {
            return classe switch
            {
                ClassePersonagem.Guerreiro => new Guerreiro(nome, raca),
                ClassePersonagem.Mago => new Mago(nome, raca),
                ClassePersonagem.Arqueiro => new Arqueiro(nome, raca),
                _ => new Guerreiro(nome, raca)
            };
        }
    }
}