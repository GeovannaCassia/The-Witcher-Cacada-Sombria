using System;
using System.Collections.Generic;
using TheWitcher.Enums;

namespace TheWitcher.Models
{
    // Entidade Missão conforme enunciado
    // Possui: Nome, Descrição, Status, Lista de Inimigos
    public class Missao
    {
        private string _nome;
        private string _descricao;
        private StatusMissao _status;
        private List<Inimigo> _inimigos;

        public string Nome         { get { return _nome; } }
        public string Descricao    { get { return _descricao; } }
        public StatusMissao Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public List<Inimigo> Inimigos { get { return _inimigos; } }

        public Missao(string nome, string descricao, List<Inimigo> inimigos)
        {
            _nome     = nome;
            _descricao = descricao;
            _status   = StatusMissao.Disponivel;
            _inimigos = inimigos;
        }

        public void Exibir()
        {
            string statusTexto =
                _status == StatusMissao.Disponivel  ? "[DISPONIVEL]" :
                _status == StatusMissao.EmAndamento ? "[EM ANDAMENTO]" : "[CONCLUIDA]";

            Console.WriteLine($"\n  {statusTexto} {_nome}");
            Console.WriteLine($"  Descricao: {_descricao}");
            Console.Write("  Inimigos : ");
            for (int i = 0; i < _inimigos.Count; i++)
            {
                Console.Write(_inimigos[i].Nome);
                if (i < _inimigos.Count - 1) Console.Write(", ");
            }
            Console.WriteLine();
        }
    }
}
