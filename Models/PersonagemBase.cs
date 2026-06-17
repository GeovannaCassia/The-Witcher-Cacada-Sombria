using System;

namespace RpgGame.Models
{
    public abstract class PersonagemBase
    {
        private string _nome;
        private int _vida;
        private int _vidaMaxima;
        private int _forca;
        private int _nivel;

        protected PersonagemBase(string nome, int vidaMaxima, int forca)
        {
            _nome = nome;
            _vidaMaxima = vidaMaxima;
            _forca = forca;
            _nivel = 1;
            _vida = _vidaMaxima;
        }

        public string Nome => _nome;

        public int Vida
        {
            get => _vida;
            protected set
            {
                if (value < 0) _vida = 0;
                else if (value > _vidaMaxima) _vida = _vidaMaxima;
                else _vida = value;
            }
        }

        public int VidaMaxima
        {
            get => _vidaMaxima;
            protected set
            {
                _vidaMaxima = value > 0 ? value : 1;
                if (_vida > _vidaMaxima) _vida = _vidaMaxima;
            }
        }

        public int Forca
        {
            get => _forca;
            protected set => _forca = value > 0 ? value : 1;
        }

        public int Nivel
        {
            get => _nivel;
            protected set => _nivel = value > 0 ? value : 1;
        }

        public bool Vivo => Vida > 0;

        public abstract int Atacar();

        public abstract void ReceberDano(int dano);

        public void Curar(int quantidade)
        {
            if (quantidade <= 0) return;
            Vida += quantidade;
        }

        public override string ToString()
        {
            return $"{Nome} (Vida: {Vida}/{VidaMaxima}, Força: {Forca}, Nível: {Nivel})";
        }
    }
}