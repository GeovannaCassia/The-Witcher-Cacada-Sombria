# 🐺 The Witcher — RPG POO em C#
**Trabalho de Programação Orientada a Objetos — PUC Minas**

---

## 📌 Descrição

Jogo de RPG por turnos em console, baseado no universo The Witcher, desenvolvido em C# aplicando rigorosamente os **4 pilares da POO**.

## Como executar o projeto

### Pré-requisitos
- .NET SDK instalado (versão compatível com o projeto)

### Executando o RPG

1. Abra um terminal na pasta raiz do projeto (onde está localizado o arquivo `Program.cs` e o arquivo `.csproj`).
2. Execute o comando:

```bash
dotnet run

```
---
## 1️⃣ Diagrama Textual das Classes
[Interface] IAtacavel               [Interface] IHabilidade
┌──────────────────────┐            ┌────────────────────────┐
│ + Atacar(alvo)       │            │ + NomeHabilidade       │
│ + ReceberDano(dano)  │            │ + DescricaoHabilidade  │
└──────────┬───────────┘            │ + UsarHabilidade(alvo) │
           │                        └────────────┬───────────┘
           └──────────────┬─────────────────────-┘
                          ▼
            [Abstract] PersonagemBase
            ┌─────────────────────────────────────┐
            │ - _nome, _vida, _vidaMaxima          │
            │ - _forca, _nivel                     │
            │ + Vivo : bool (readonly)             │
            │ + Atacar(alvo)      [abstract]       │
            │ + ReceberDano(dano) [abstract]       │
            │ + ExibirStatus()    [virtual]        │
            └──────────┬──────────────────────────┘
                       │
              ┌────────┴────────┐
              ▼                 ▼
       [Abstract] Jogador     Inimigo
       ┌──────────────────┐   ┌──────────────────────┐
       │ - _classe        │   │ + Atacar() [override] │
       │ - _raca          │   │ + ReceberDano()[ovr.] │
       │ - _inventario    │   └──────────────────────┘
       │ + AplicarProg.() │
       │ + UsarItemCura() │
       │ + UsarHabilidade │
       └──────┬───────────┘
              │
        ┌─────┼──────┐
        ▼     ▼      ▼
   Guerreiro  Mago  Arqueiro
   (override Atacar + UsarHabilidade em cada um)

[Model] Item                [Model] Inventario
┌────────────────┐          ┌────────────────────────────┐
│ - _nome        │          │ - _itens : List<Item>      │
│ - _tipo        │◄─────────│ - CAPACIDADE_MAXIMA = 10   │
│ - _qtdCura     │          │ + AdicionarItem()          │
└────────────────┘          │ + RetirarPrimeiroCuravel() │
                            └────────────────────────────┘

[Model] Missao
┌─────────────────────────────┐
│ - _nome : string            │
│ - _descricao : string       │
│ - _status : StatusMissao    │
│ - _inimigos : List<Inimigo> │
└─────────────────────────────┘

[Enum] ClassePersonagem : Guerreiro | Mago | Arqueiro
[Enum] RacaPersonagem   : Humano | Elfo | Anao
[Enum] StatusMissao     : Disponivel | EmAndamento | Concluida
[Enum] TipoItem         : Consumivel | Equipamento
```

---

## 2️⃣ Aplicação dos 4 Pilares da POO

### Encapsulamento
Todos os atributos são **privados** (`_nome`, `_vida`, etc.) e acessados via **propriedades públicas** com validações. Exemplo: a propriedade `Vida` impede valores negativos:
```csharp
public int Vida {
    get { return _vida; }
    set { _vida = value < 0 ? 0 : value; }
}
```

### Herança
`PersonagemBase` é a **classe abstrata base**. `Jogador` e `Inimigo` herdam dela. `Guerreiro`, `Mago` e `Arqueiro` herdam de `Jogador` — formando uma cadeia de herança em 3 níveis.

### Abstração
`PersonagemBase` declara `Atacar()` e `ReceberDano()` como **métodos abstratos**, forçando cada subclasse a implementar sua própria lógica. A interface `IHabilidade` define o contrato das habilidades especiais.

### Polimorfismo
Cada classe (`Guerreiro`, `Mago`, `Arqueiro`) sobrescreve `Atacar()` e `UsarHabilidade()` com comportamentos distintos usando `override`:
- **Guerreiro**: ataque físico + Golpe Poderoso (2× Força)
- **Mago**: arcano variável + Bola de Fogo (50 fixo)
- **Arqueiro**: flecha + Tiro Certeiro (1.8× Força)

---

## 3️⃣ Estrutura de Pastas

```
TheWitcher/
├── Program.cs                        ← Ponto de entrada e menu principal
├── TheWitcher.csproj                 ← Configuração do projeto
├── README.md
│
├── Enums/
│   └── Enums.cs                      ← ClassePersonagem, RacaPersonagem, StatusMissao, TipoItem
│
├── Interfaces/
│   ├── IAtacavel.cs                  ← Contrato: Atacar() + ReceberDano()
│   └── IHabilidade.cs                ← Contrato: UsarHabilidade()
│
├── Models/
│   ├── PersonagemBase.cs             ← Classe abstrata base (Abstração + Herança)
│   ├── Jogador.cs                    ← Jogador abstrato + Guerreiro, Mago, Arqueiro
│   ├── Inimigo.cs                    ← Inimigo concreto
│   ├── Item.cs                       ← Item consumível ou equipamento
│   ├── Inventario.cs                 ← Inventário com limite de 10 itens
│   └── Missao.cs                     ← Missão com nome, descrição, status e inimigos
│
├── Services/
│   ├── CriacaoPersonagem.cs          ← Criação interativa de personagem
│   ├── SistemaCombate.cs             ← Loop while(monstro.Vivo) com turnos
│   └── SistemaMissoes.cs             ← 3 missões × 3 turnos + progressão
│
└── Utils/
    └── Display.cs                    ← Utilitários de exibição e diagrama de classes
```

---

## 4️⃣ Regras do Jogo

- **3 missões** com **3 turnos cada** = 9 monstros no total
- Cada turno: Jogador ataca → Monstro ataca (`while(monstro.Vivo)`)
- Ao concluir uma missão: vida máxima e força do jogador são **dobradas**
- Monstros das missões seguintes já vêm com atributos proporcionalmente maiores
- Se o jogador morrer → **Fim de jogo**
- Ao concluir as 3 missões → **Vitória**

---

## 5️⃣ Como Executar

### Com .NET SDK (recomendado):
```bash
dotnet run
```

### Com Mono:
```bash
mcs -out:TheWitcher.exe Enums/Enums.cs Interfaces/*.cs Models/PersonagemBase.cs Models/Item.cs Models/Inventario.cs Models/Inimigo.cs Models/Jogador.cs Models/Missao.cs Services/CriacaoPersonagem.cs Services/SistemaCombate.cs Services/SistemaMissoes.cs Utils/Display.cs Program.cs
mono TheWitcher.exe
```

---

## 👥 Divisão de Tarefas

| Integrante | Responsabilidade |
|---|---|
| Anthonny | `SistemaCombate.cs`, `Inimigo.cs` — combate por turnos e gerenciamento de inimigos |
| [Colega 2] | `PersonagemBase.cs`, `Jogador.cs` — hierarquia de classes |
| [Colega 3] | `Item.cs`, `Inventario.cs` — sistema de inventário |
| [Colega 4] | `Missao.cs`, `SistemaMissoes.cs`, `Program.cs` — missões e menu |

---

## 🛠️ Tecnologias

- Linguagem: **C# (.NET Console Application)**
- Compiladores: **.NET 8 SDK** / **Mono 6.8**
- Versionamento: **Git + GitHub**
