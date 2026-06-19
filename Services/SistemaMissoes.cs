using System;
using System.Collections.Generic;
using TheWitcher.Enums;
using TheWitcher.Models;
using TheWitcher.Services;

namespace TheWitcher.Services
{
    // Gerencia o fluxo das 3 missões com 3 turnos cada
    public class SistemaMissoes
    {
        private List<Missao> _missoes;
        private Jogador _jogador;
        private SistemaCombate _combate;
        private int _missaoAtual; // índice da missão em andamento

        public SistemaMissoes(Jogador jogador, int missaoInicial = 0)
        {
            _jogador   = jogador;
            _combate   = new SistemaCombate(jogador);
            _missaoAtual = missaoInicial;
            _missoes   = CriarMissoes();

            // Ao carregar save, marca missoes anteriores como concluidas
            for (int i = 0; i < missaoInicial && i < _missoes.Count; i++)
                _missoes[i].Status = StatusMissao.Concluida;
        }

        // ─── Criação das 3 missões (9 monstros no total) ───────────
        // Atributos dos monstros das missões 2 e 3 são dobrados
        private List<Missao> CriarMissoes()
        {
            // Missão 1 — valores base
            var missao1 = new Missao(
                "A Besta das Trevas",
                "O vilarejo de Kaer Morhen e assombrado por tres criaturas sinistras.",
                new List<Inimigo>
                {
                    new Inimigo("Lobo Selvagem",  vidaMaxima: 60,  forca: 12),
                    new Inimigo("Goblin Feroz",   vidaMaxima: 70,  forca: 14),
                    new Inimigo("Lobisomem",      vidaMaxima: 90,  forca: 18)
                }
            );

            // Missão 2 — atributos dobrados (60->120, 12->24...)
            var missao2 = new Missao(
                "O Grifo de Vizima",
                "Criaturas aladas atacam as rotas comerciais ao norte.",
                new List<Inimigo>
                {
                    new Inimigo("Grifo Jovem",    vidaMaxima: 120, forca: 24),
                    new Inimigo("Harpia Raivosa", vidaMaxima: 140, forca: 28),
                    new Inimigo("Grifo Anciao",   vidaMaxima: 180, forca: 36)
                }
            );

            // Missão 3 — atributos dobrados novamente (120->240...)
            var missao3 = new Missao(
                "O Terror do Norte",
                "O Drake Negro e um dragao anciou que destroi cidades inteiras. Boss final.",
                new List<Inimigo>
                {
                    new Inimigo("Espectro Sombrio", vidaMaxima: 240, forca: 48),
                    new Inimigo("Vampiro Supremo",  vidaMaxima: 280, forca: 56),
                    new Inimigo("Drake Negro",      vidaMaxima: 360, forca: 72)
                }
            );

            return new List<Missao> { missao1, missao2, missao3 };
        }

        // ─── Exibe lista de missões ─────────────────────────────────
        public void ExibirMissoes()
        {
            Console.WriteLine("\n  ╔═══════════════════════════════════╗");
            Console.WriteLine("  ║           MISSOES                 ║");
            Console.WriteLine("  ╚═══════════════════════════════════╝");
            for (int i = 0; i < _missoes.Count; i++)
            {
                Console.Write($"\n  {i + 1}.");
                _missoes[i].Exibir();
            }
        }

        // ─── Inicia a próxima missão disponível ─────────────────────
        // Retorna false se o jogo acabou (vitória ou derrota)
        public bool IniciarProximaMissao()
        {
            if (_missaoAtual >= _missoes.Count)
            {
                Console.WriteLine("\n  Todas as missoes ja foram concluidas!");
                return true;
            }

            Missao missao = _missoes[_missaoAtual];

            // Regra: missão concluída não pode ser repetida
            if (missao.Status == StatusMissao.Concluida)
            {
                Console.WriteLine("  [!] Esta missao ja foi concluida.");
                return true;
            }

            // Regra: acesso sequencial às missões
            if (_missaoAtual > 0 && _missoes[_missaoAtual - 1].Status != StatusMissao.Concluida)
            {
                Console.WriteLine("  [!] Conclua a missao anterior primeiro.");
                return true;
            }

            missao.Status = StatusMissao.EmAndamento;
            Console.Clear();
            Console.WriteLine($"\n  ══ MISSAO {_missaoAtual + 1}: {missao.Nome} ══");
            Console.WriteLine($"  {missao.Descricao}");
            Console.WriteLine($"  Turnos: {missao.Inimigos.Count} inimigos");
            Console.WriteLine("\n  Pressione ENTER para iniciar...");
            Console.ReadLine();

            // ─── 3 turnos da missão ─────────────────────────────────
            for (int t = 0; t < missao.Inimigos.Count; t++)
            {
                Inimigo inimigo = missao.Inimigos[t];
                Console.WriteLine($"\n  == Turno {t + 1}/{missao.Inimigos.Count}: {inimigo.Nome} ==");

                bool vitoria = _combate.Combater(inimigo);

                if (!vitoria)
                {
                    // Jogador morreu: fim de jogo
                    missao.Status = StatusMissao.Disponivel;
                    return false;
                }

                // ─── DROP de item ao vencer o turno ─────────────────
                DropItem(inimigo, t + 1);
            }

            // ─── Missão concluída ────────────────────────────────────
            missao.Status = StatusMissao.Concluida;
            _missaoAtual++;
            Console.WriteLine($"\n  MISSAO CONCLUIDA: {missao.Nome}!");

            // Progressão: dobra atributos do jogador (enunciado)
            if (_missaoAtual < _missoes.Count)
            {
                Console.WriteLine("\n  O jogador ficou mais forte para a proxima missao!");
                _jogador.AplicarProgressao();
            }

            // ─── AUTO-SAVE após missão concluída ────────────────────
            SistemaArquivo.Salvar(_jogador, _missaoAtual);

            Console.WriteLine("\n  Pressione ENTER para continuar...");
            Console.ReadLine();
            return true;
        }

        // Verifica se todas as missões foram concluídas
        public bool JogoConcluido()
        {
            return _missaoAtual >= _missoes.Count;
        }

        public int MissaoAtual { get { return _missaoAtual; } }

        // ─── Drop de item ao vencer um turno ────────────────────────
        // Monstros mais fortes (turno 3) têm chance maior de dropar poção maior
        private void DropItem(Inimigo inimigo, int turno)
        {
            Random rnd = new Random();
            int chance = rnd.Next(0, 100);

            Item drop = null;

            if (turno == 3)
            {
                // Boss do turno 3: sempre dropa algo
                if (chance < 60)
                    drop = new Item("Pocao de Vida Grande", TipoItem.Consumivel, quantidadeCura: 80);
                else
                    drop = new Item("Pocao de Vida", TipoItem.Consumivel, quantidadeCura: 40);
            }
            else
            {
                // Turnos 1 e 2: 50% de chance de dropar poção pequena
                if (chance < 50)
                    drop = new Item("Pocao de Vida", TipoItem.Consumivel, quantidadeCura: 40);
            }

            if (drop != null)
            {
                Console.WriteLine($"\n  [DROP] {inimigo.Nome} deixou: {drop.Nome}!");
                bool adicionado = _jogador.Inventario.AdicionarItem(drop);
                if (!adicionado)
                    Console.WriteLine("  [!] Inventario cheio — item perdido.");
            }
            else
            {
                Console.WriteLine($"\n  [DROP] {inimigo.Nome} nao deixou nenhum item.");
            }
        }
    }
}
