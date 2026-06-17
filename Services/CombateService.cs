using System;
using RpgGame.Models;

namespace RpgGame.Services
{
    public class CombateService
    {
        public bool IniciarCombate(Jogador jogador, Inimigo inimigo)
        {
            while (inimigo.Vivo && jogador.Vivo)
            {
                Console.WriteLine($"\n--- Turno do jogador: {jogador.Nome} ---");
                Console.WriteLine("1 - Atacar");
                Console.WriteLine("2 - Usar item de cura");

                string opcao = Console.ReadLine() ?? "";

                if (opcao == "1")
                {
                    int dano = jogador.Atacar();
                    inimigo.ReceberDano(dano);
                    Console.WriteLine($"{jogador.Nome} atacou {inimigo.Nome} causando {dano} de dano.");
                    Console.WriteLine($"{inimigo.Nome}: {inimigo.Vida}/{inimigo.VidaMaxima}");
                }
                else if (opcao == "2")
                {
                    bool usou = jogador.UsarItemDeCura();

                    if (usou)
                    {
                        Console.WriteLine($"{jogador.Nome} usou uma poção e recuperou vida.");
                        Console.WriteLine($"{jogador.Nome}: {jogador.Vida}/{jogador.VidaMaxima}");
                    }
                    else
                    {
                        Console.WriteLine("Você não possui poções.");
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                    continue;
                }

                if (!inimigo.Vivo)
                {
                    Console.WriteLine($"\n{inimigo.Nome} foi derrotado!");
                    return true;
                }

                Console.WriteLine($"\n--- Turno do inimigo: {inimigo.Nome} ---");
                int danoInimigo = inimigo.Atacar();
                jogador.ReceberDano(danoInimigo);
                Console.WriteLine($"{inimigo.Nome} atacou {jogador.Nome} causando {danoInimigo} de dano.");
                Console.WriteLine($"{jogador.Nome}: {jogador.Vida}/{jogador.VidaMaxima}");

                if (!jogador.Vivo)
                {
                    Console.WriteLine($"\n{jogador.Nome} foi derrotado.");
                    return false;
                }
            }

            return jogador.Vivo;
        }
    }
}