using System;
using System.Threading;

namespace TheWitcher.Services
{
    // Serviço responsável pelo combate por turnos
    // Contém o loop while(monstro.Vivo) exigido no enunciado
    public class SistemaCombate
    {
        private Jogador _jogador;
        private bool _habilidadeUsada; // habilidade especial: uma vez por combate

        public SistemaCombate(Jogador jogador)
        {
            _jogador = jogador;
        }

        // Executa o combate completo contra um inimigo
        // Retorna true se o jogador venceu, false se morreu
        public bool Combater(Inimigo monstro)
        {
            _habilidadeUsada = false;
            ExibirInicio(monstro);

            // ─── Loop de combate exigido no enunciado ──────────────────
            while (monstro.Vivo)
            {
                ExibirStatusCombate(monstro);

                // Turno do jogador
                bool acaoValida = TurnoJogador(monstro);
                if (!acaoValida) continue;   // ação inválida — repete sem gastar turno

                // Verifica se o monstro morreu após o turno do jogador
                if (!monstro.Vivo) break;

                Thread.Sleep(600);

                // Turno do monstro (só ataca, conforme enunciado)
                Console.WriteLine("\n  --- Turno do Monstro ---");
                monstro.Atacar(_jogador);

                // Verifica se o jogador morreu
                if (!_jogador.Vivo)
                {
                    Console.WriteLine($"\n  {_jogador.Nome} foi derrotado. FIM DE JOGO.");
                    Console.ReadLine();
                    return false;
                }

                Thread.Sleep(400);
            }

            // Monstro derrotado
            Console.WriteLine($"\n  [{monstro.Nome}] foi derrotado! Turno vencido!");
            Console.WriteLine("  Pressione ENTER para continuar...");
            Console.ReadLine();
            return true;
        }

        // ─── Turno do Jogador ───────────────────────────────────────
        private bool TurnoJogador(Inimigo monstro)
        {
            Console.WriteLine("\n  --- Turno do Jogador ---");
            Console.WriteLine("  O que voce vai fazer?");
            Console.WriteLine("  1 - Atacar");
            Console.WriteLine("  2 - Utilizar Item de Cura");
            if (!_habilidadeUsada)
                Console.WriteLine($"  3 - Habilidade Especial: {_jogador.NomeHabilidade}");
            Console.Write("  Escolha: ");

            string op = Console.ReadLine();

            if (op == "1")
            {
                _jogador.Atacar(monstro);
                return true;
            }
            else if (op == "2")
            {
                _jogador.UsarItemCura();
                return true;
            }
            else if (op == "3" && !_habilidadeUsada)
            {
                _jogador.UsarHabilidade(monstro);
                _habilidadeUsada = true;
                return true;
            }
            else
            {
                Console.WriteLine("  [!] Opcao invalida.");
                return false;
            }
        }

        // ─── Exibições ──────────────────────────────────────────────
        private void ExibirInicio(Inimigo monstro)
        {
            Console.Clear();
            Console.WriteLine("\n  ╔═══════════════════════════════════╗");
            Console.WriteLine("  ║         COMBATE INICIADO          ║");
            Console.WriteLine("  ╚═══════════════════════════════════╝");
            Console.WriteLine($"\n  {_jogador.Nome}  VS  {monstro.Nome}");
            Console.WriteLine("\n  Pressione ENTER para comecar...");
            Console.ReadLine();
        }

        private void ExibirStatusCombate(Inimigo monstro)
        {
            Console.WriteLine("\n  ═══════════════════════════════════");
            ExibirBarra(_jogador.Nome, _jogador.Vida, _jogador.VidaMaxima, "JOG");
            ExibirBarra(monstro.Nome,  monstro.Vida,  monstro.VidaMaxima,  "INI");
            Console.WriteLine("  ═══════════════════════════════════");
        }

        private void ExibirBarra(string nome, int vida, int max, string tag)
        {
            double pct   = (double)vida / max;
            int barras   = (int)(pct * 20);
            string barra = new string('|', barras) + new string('.', 20 - barras);
            Console.WriteLine($"  [{tag}] {nome,-14} [{barra}] {vida}/{max}");
        }
    }
}
