using System.Collections.Generic;
using RpgGame.Enums;

namespace RpgGame.Models
{
    public class Missao
    {
        private string _nome;
        private string _descricao;
        private StatusMissao _status;
        private List<Inimigo> _inimigos;

        public Missao(string nome, string descricao, List<Inimigo> inimigos)
        {
            _nome = nome;
            _descricao = descricao;
            _status = StatusMissao.Disponivel;
            _inimigos = inimigos;
        }

        public string Nome => _nome;
        public string Descricao => _descricao;
        public StatusMissao Status
        {
            get => _status;
            private set => _status = value;
        }

        public List<Inimigo> Inimigos => _inimigos;

        public void MarcarComoEmAndamento() => Status = StatusMissao.EmAndamento;
        public void MarcarComoConcluida() => Status = StatusMissao.Concluida;
    }
}