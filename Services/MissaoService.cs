using System.Collections.Generic;
using RpgGame.Models;

namespace RpgGame.Services
{
    public class MissaoService
    {
        public List<Missao> CriarMissoes()
        {
            return new List<Missao>
            {
                new Missao(
                    "Missão 1: Floresta Sombria",
                    "Derrote os monstros da floresta para avançar.",
                    new List<Inimigo>
                    {
                        new Inimigo("Monstro 1", 50, 6),
                        new Inimigo("Monstro 2", 55, 7),
                        new Inimigo("Monstro 3", 60, 8)
                    }),

                new Missao(
                    "Missão 2: Caverna dos Ossos",
                    "Os monstros ficaram mais fortes. Prepare-se.",
                    new List<Inimigo>
                    {
                        new Inimigo("Monstro 4", 100, 12),
                        new Inimigo("Monstro 5", 110, 13),
                        new Inimigo("Monstro 6", 120, 14)
                    }),

                new Missao(
                    "Missão 3: Castelo do Fim",
                    "A batalha final está próxima.",
                    new List<Inimigo>
                    {
                        new Inimigo("Monstro 7", 200, 18),
                        new Inimigo("Monstro 8", 220, 20),
                        new Inimigo("Monstro 9", 240, 22)
                    })
            };
        }
    }
}