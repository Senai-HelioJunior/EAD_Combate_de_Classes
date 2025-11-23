 
using System;
//------------------------------ Status do personagem ----------------
public interface IPersonagem
{
    string Nome { get; }
    int Ataque { get; }
    int Defesa { get; }
    int Vida { get; }
    
    void ReceberDano(int dano);
    int Dano();
}

public abstract class Personagem : IPersonagem
{
    public string Nome { get;  set; }
    public int Ataque { get;  set; }
    public int Defesa { get;  set; }
    public int Vida { get;  set; }
    
    public Personagem(string nome, int ataque, int defesa, int vida)
    {
        Nome = nome;
        Ataque = ataque;
        Defesa = defesa;
        Vida = vida;
    }
    
    public virtual void ReceberDano(int dano)
    {
        Vida -= dano;
        if (Vida < 0) Vida = 0;
    }
    
    public virtual int Dano()
    {
        return Ataque;
    }
}

//------------------------------- Criando os Personagens -----------------------
   public class Guerreiro : Personagem
{
    public Guerreiro(string nome) : base(nome, 15, 10, 100) { }

    public override int Dano()
    {
        return Ataque;
    }
}



public class Mago : Personagem
{
    public Mago(string nome) : base(nome, 20, 5, 100)
    {
    }
    
    public override void ReceberDano(int dano)
     {
    base.ReceberDano(dano);
    }
}

// --------------------------------Comeco do jogo ---------------------------------
public class JogoCombate
{
    private IPersonagem jogador1;
    private IPersonagem jogador2;
    private IPersonagem atacante;
    private IPersonagem defensor;
    
    public JogoCombate(IPersonagem p1, IPersonagem p2)
    {
        jogador1 = p1;
        jogador2 = p2;
        atacante = p1;
        defensor = p2;
    }
    
    public void IniciarCombate()
    {
        Console.WriteLine("===== COMBATE INICIADO =====");
        Console.WriteLine($"{jogador1.Nome} (Vida: {jogador1.Vida}) vs {jogador2.Nome} (Vida: {jogador2.Vida})");
        Console.WriteLine("                     ");
        
        while (jogador1.Vida > 0 && jogador2.Vida > 0)
        {
            Turno();
            TrocaDeTurno();
        }
        
        Vencedor();
    }
    
    private void Turno()
    {
        Console.WriteLine($"Vez do {atacante.Nome} atacar!");
        
        int dano = atacante.Dano();
        int danoFinal = dano - (defensor.Defesa / 3); 
        
        if (danoFinal < 1) danoFinal = 1; 
        
        Console.WriteLine($"{atacante.Nome} causa {danoFinal} de dano em {defensor.Nome}!");
        defensor.ReceberDano(danoFinal);
        
        Console.WriteLine($"{defensor.Nome} agora tem {defensor.Vida} de vida.");
        Console.WriteLine("----------------------------");
    }
    
    private void TrocaDeTurno()
    {
        if (atacante == jogador1)
        {
            atacante = jogador2;
            defensor = jogador1;
        }
        else
        {
            atacante = jogador1;
            defensor = jogador2;
        }
    }
    
    private void Vencedor()
    {
        IPersonagem vencedor;

    if (jogador1.Vida > 0)
    {
    vencedor = jogador1;
    }
    else
    {
    vencedor = jogador2;
    }
        Console.WriteLine($"{vencedor.Nome} VENCEU O COMBATE!");
    }
}

//--------------------- Principal(Não mexer)---------------------------------------
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo ao Jogo de Combate!");
        
      
        IPersonagem guerreiro = new Guerreiro("Guerreiro");
        IPersonagem mago = new Mago("Mago");
        
     
        JogoCombate jogo = new JogoCombate(guerreiro, mago);
        jogo.IniciarCombate();
        
    }
}

