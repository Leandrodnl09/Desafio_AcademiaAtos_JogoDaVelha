namespace DesafioAtos_JogoDaVelha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Desenvolva um jogo da velha utilizando matrizes em C#. Faça com que cada jogador insira a sua jogada em uma interface amigavel. 
            // Teste se a posição é válida e caso não seja solicite ao jogador repetir a jogada. Após cada jogada, apresente o tabuleiro com as
            // jogadas representadas por "X" e "O" e faça a verficação se algum jogador venceu.
            // Caso seja empate, apresente o resultado na tela.Possilibilite que o jogo seja reinicializado sem a necessidade de reiniciar o jogo.


            // Desafio extra, pode valer por alguma atividade futura: Faça a implementação de um jogo contra o computador.Faça o possível para evitar que o jogador vença do computador.
            // Para facilitar, faça com que o computador inicie jogando.

            bool jogarNovamente = true;

            while (jogarNovamente)
            {
                bool fimDeJogo = false;
                int jogadas = 0;
                string[,] tabuleiro = new string[3, 3];
                bool jogadaValida;

                Console.WriteLine("Bem-vindo ao Jogo da Velha!");
                Console.WriteLine();
                Console.Write("Digite o nome do Jogador 1: ");
                string jogador1 = Console.ReadLine();
                Console.Write("Digite o nome do Jogador 2: ");
                string jogador2 = Console.ReadLine();

                while (!fimDeJogo)
                {
                    Console.Clear();
                    DesenharTabuleiro(tabuleiro);

                    if (jogadas % 2 == 0)
                    {
                        Console.WriteLine($"{jogador1}, é sua vez (X)");
                    }
                    else
                    {
                        Console.WriteLine($"{jogador2}, é sua vez (O)");
                    }

                    jogadaValida = false;

                    while (!jogadaValida)
                    {
                        Console.Write("Digite a linha (1 a 3): ");
                        int linha = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Digite a coluna (1 a 3): ");
                        int coluna = int.Parse(Console.ReadLine()) - 1;

                        if (tabuleiro[linha, coluna] == null)
                        {
                            if (jogadas % 2 == 0)
                            {
                                tabuleiro[linha, coluna] = "X";
                            }
                            else
                            {
                                tabuleiro[linha, coluna] = "O";
                            }

                            jogadaValida = true;
                        }
                        else
                        {
                            Console.WriteLine("Jogada inválida. Tente novamente.");
                        }
                    }

                    if (VerificarGanhador(tabuleiro, jogadas))
                    {
                        fimDeJogo = true;
                        Console.Clear();
                        DesenharTabuleiro(tabuleiro);

                        if (jogadas % 2 == 0)
                        {
                            Console.WriteLine($"{jogador1} ganhou!");
                        }
                        else
                        {
                            Console.WriteLine($"{jogador2} ganhou!");
                        }
                    }
                    else if (jogadas == 8)
                    {
                        fimDeJogo = true;
                        Console.Clear();
                        DesenharTabuleiro(tabuleiro);
                        Console.WriteLine("Empate!");
                    }
                    jogadas++;
                }
                Console.WriteLine();
                Console.Write("Deseja jogar novamente? (S/N) ");
                string resposta = Console.ReadLine().ToUpper();

                if (resposta != "S")
                {
                    jogarNovamente = false;
                }
            }

            static void DesenharTabuleiro(string[,] tabuleiro)
            {
                Console.Clear();
                Console.WriteLine("    1   2   3 ");
                Console.WriteLine("  +---+---+---+");

                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"{i + 1} ");

                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write($"| {tabuleiro[i, j] ?? " "} ");
                    }

                    Console.Write("|");
                    Console.WriteLine();
                    Console.WriteLine("  +---+---+---+");
                }
            }

            static bool VerificarGanhador(string[,] tabuleiro, int jogadas)
            {
                string jogador;

                if (jogadas % 2 == 0)
                {
                    jogador = "X";
                }
                else
                {
                    jogador = "O";
                }

                // Verifica linhas
                for (int i = 0; i < 3; i++)
                {
                    if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                    {
                        return true;
                    }
                }
                // Verifica colunas
                for (int i = 0; i < 3; i++)
                {
                    if (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador)
                    {
                        return true;
                    }
                }

                // Verifica a diagonal principal
                if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
                {
                    return true;
                }

                // Verifica a diagonal secundária
                if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
                {
                    return true;
                }
                return false;
            }
        }
    }
}