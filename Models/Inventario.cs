using System;
using System.Collections.Generic;
using TheWitcher.Models;

namespace TheWitcher.Models
{
    // Gerencia o inventário do jogador com limite de 10 itens
    public class Inventario
    {
        private List<Item> _itens;
        private const int CAPACIDADE_MAXIMA = 10;   // limite definido no enunciado

        public List<Item> Itens { get { return _itens; } }
        public int Capacidade  { get { return CAPACIDADE_MAXIMA; } }

        public Inventario()
        {
            _itens = new List<Item>();
        }

        // Tenta adicionar item; retorna false se o inventário estiver cheio
        public bool AdicionarItem(Item item)
        {
            if (_itens.Count >= CAPACIDADE_MAXIMA)
            {
                Console.WriteLine("  [!] Inventario cheio! Capacidade maxima: 10 itens.");
                return false;
            }
            _itens.Add(item);
            return true;
        }

        // Remove e retorna o primeiro item de cura disponível
        public Item RetirarPrimeiroCuravel()
        {
            for (int i = 0; i < _itens.Count; i++)
            {
                if (_itens[i].QuantidadeCura > 0)
                {
                    Item item = _itens[i];
                    _itens.RemoveAt(i);   // removido após o uso (enunciado)
                    return item;
                }
            }
            return null;
        }

        public void Exibir()
        {
            Console.WriteLine($"\n  ╔══════════════════════════════╗");
            Console.WriteLine($"  ║         INVENTARIO           ║");
            Console.WriteLine($"  ╠══════════════════════════════╣");
            if (_itens.Count == 0)
            {
                Console.WriteLine("  ║   (vazio)                    ║");
            }
            else
            {
                for (int i = 0; i < _itens.Count; i++)
                    Console.WriteLine($"  ║  {i + 1,2}. {_itens[i],-25}║");
            }
            Console.WriteLine($"  ╠══════════════════════════════╣");
            Console.WriteLine($"  ║  Capacidade: {_itens.Count}/{CAPACIDADE_MAXIMA,-17}║");
            Console.WriteLine($"  ╚══════════════════════════════╝");
        }
    }
}
