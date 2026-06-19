using System;
using System.IO;
using System.Collections.Generic;
using TheWitcher.Enums;
using TheWitcher.Models;

namespace TheWitcher.Services
{
    // Serviço de persistência — salva e carrega o estado do jogo em JSON
    // Serialização manual para compatibilidade com Mono sem dependências externas
    public static class SistemaArquivo
    {
        private static readonly string CaminhoSave = "save.json";

        // ─── Estrutura de dados do save ──────────────────────────────
        // Armazena tudo que precisa ser restaurado entre sessões
        public class DadosSave
        {
            public string Nome;
            public string Classe;
            public string Raca;
            public int Nivel;
            public int Vida;
            public int VidaMaxima;
            public int Forca;
            public int MissaoAtual;
            public List<string> NomesItens    = new List<string>();
            public List<string> TiposItens    = new List<string>();
            public List<int>    CurasItens    = new List<int>();
        }

        // ─── SALVAR ──────────────────────────────────────────────────
        public static void Salvar(Jogador jogador, int missaoAtual)
        {
            try
            {
                // Serializa itens do inventário
                var nomesItens = new List<string>();
                var tiposItens = new List<string>();
                var curasItens = new List<int>();

                foreach (Item item in jogador.Inventario.Itens)
                {
                    nomesItens.Add(item.Nome);
                    tiposItens.Add(item.Tipo.ToString());
                    curasItens.Add(item.QuantidadeCura);
                }

                // Monta JSON manualmente (sem dependência externa)
                string itensJson = "";
                for (int i = 0; i < nomesItens.Count; i++)
                {
                    if (i > 0) itensJson += ",\n";
                    itensJson +=
                        $"    {{ \"Nome\": \"{nomesItens[i]}\", " +
                        $"\"Tipo\": \"{tiposItens[i]}\", " +
                        $"\"Cura\": {curasItens[i]} }}";
                }

                string json =
                    "{\n" +
                    $"  \"Nome\": \"{jogador.Nome}\",\n" +
                    $"  \"Classe\": \"{jogador.Classe}\",\n" +
                    $"  \"Raca\": \"{jogador.Raca}\",\n" +
                    $"  \"Nivel\": {jogador.Nivel},\n" +
                    $"  \"Vida\": {jogador.Vida},\n" +
                    $"  \"VidaMaxima\": {jogador.VidaMaxima},\n" +
                    $"  \"Forca\": {jogador.Forca},\n" +
                    $"  \"MissaoAtual\": {missaoAtual},\n" +
                    $"  \"Itens\": [\n{itensJson}\n  ]\n" +
                    "}";

                File.WriteAllText(CaminhoSave, json);
                Console.WriteLine($"\n  [SAVE] Progresso salvo em '{CaminhoSave}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n  [ERRO] Falha ao salvar: {ex.Message}");
            }
        }

        // ─── CARREGAR ────────────────────────────────────────────────
        public static DadosSave Carregar()
        {
            try
            {
                if (!File.Exists(CaminhoSave))
                    return null;

                string[] linhas = File.ReadAllLines(CaminhoSave);
                var dados = new DadosSave();

                bool dentroItens = false;
                string nomeItem = null;
                string tipoItem = null;
                int    curaItem = 0;

                foreach (string linha in linhas)
                {
                    string l = linha.Trim();

                    // Detecta bloco de itens
                    if (l.StartsWith("\"Itens\"")) { dentroItens = true; continue; }

                    if (!dentroItens)
                    {
                        // Leitura de campos simples
                        ExtrairString(l, "Nome",    ref dados.Nome);
                        ExtrairString(l, "Classe",  ref dados.Classe);
                        ExtrairString(l, "Raca",    ref dados.Raca);
                        ExtrairInt(l,    "Nivel",   ref dados.Nivel);
                        ExtrairInt(l,    "Vida",    ref dados.Vida);
                        ExtrairInt(l,    "VidaMaxima", ref dados.VidaMaxima);
                        ExtrairInt(l,    "Forca",   ref dados.Forca);
                        ExtrairInt(l,    "MissaoAtual", ref dados.MissaoAtual);
                    }
                    else
                    {
                        // Leitura de itens inline: { "Nome": "X", "Tipo": "Y", "Cura": Z }
                        string tmpNome = null; string tmpTipo = null; int tmpCura = 0;
                        ExtrairString(l, "Nome", ref tmpNome);
                        ExtrairString(l, "Tipo", ref tmpTipo);
                        ExtrairInt(l,    "Cura", ref tmpCura);

                        if (tmpNome != null) nomeItem = tmpNome;
                        if (tmpTipo != null) tipoItem = tmpTipo;
                        if (l.Contains("\"Cura\"")) curaItem = tmpCura;

                        // Linha com } fecha o objeto do item
                        if (l.Contains("}") && nomeItem != null && tipoItem != null)
                        {
                            dados.NomesItens.Add(nomeItem);
                            dados.TiposItens.Add(tipoItem);
                            dados.CurasItens.Add(curaItem);
                            nomeItem = null; tipoItem = null; curaItem = 0;
                        }
                    }
                }

                return dados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n  [ERRO] Falha ao carregar: {ex.Message}");
                return null;
            }
        }

        public static bool ExisteSave()
        {
            return File.Exists(CaminhoSave);
        }

        public static void DeletarSave()
        {
            if (File.Exists(CaminhoSave))
                File.Delete(CaminhoSave);
        }

        // ─── Helpers de parsing ──────────────────────────────────────
        private static void ExtrairString(string linha, string chave, ref string destino)
        {
            string busca = $"\"{chave}\"";
            if (!linha.Contains(busca)) return;
            int ini = linha.IndexOf(busca) + busca.Length;
            // Pula até a próxima aspa de valor
            int q1 = linha.IndexOf('"', ini + 1);
            if (q1 < 0) return;
            int q2 = linha.IndexOf('"', q1 + 1);
            if (q2 < 0) return;
            destino = linha.Substring(q1 + 1, q2 - q1 - 1);
        }

        private static void ExtrairInt(string linha, string chave, ref int destino)
        {
            string busca = $"\"{chave}\"";
            if (!linha.Contains(busca)) return;
            int ini = linha.IndexOf(busca) + busca.Length;
            int col = linha.IndexOf(':', ini);
            if (col < 0) return;
            string resto = linha.Substring(col + 1).Trim().TrimEnd(',', '}', ' ');
            int val;
            if (int.TryParse(resto, out val)) destino = val;
        }
    }
}
