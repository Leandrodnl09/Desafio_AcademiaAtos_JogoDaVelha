using System;
using System.ComponentModel.Design;

namespace DesafioAtos_JogoDaVelha
{
    internal class Program
    {
        static char[,] tabuleiro = new char[3, 3]; // Tabuleiro do jogo da velha
        static Random rnd = new Random(); // Gerador de números aleatórios

        static void Main(string[] args)
        {
            // Desenvolva um jogo da velha utilizando matrizes em C#. Faça com que cada jogador insira a sua jogada em uma interface amigavel. 
            // Teste se a posição é válida e caso não seja solicite ao jogador repetir a jogada. Após cada jogada, apresente o tabuleiro com as
            // jogadas representadas por "X" e "O" e faça a verficação se algum jogador venceu.
            // Caso seja empate, apresente o resultado na tela.Possilibilite que o jogo seja reinicializado sem a necessidade de reiniciar o jogo.


            // Desafio extra, pode valer por alguma atividade futura: Faça a implementação de um jogo contra o computador.Faça o possível para evitar que o jogador vença do computador.
            // Para facilitar, faça com que o computador inicie jogando.

            // Saudação do jogo
            string mensagem = Saudacao();
            DateTime dateTime = DateTime.Now;
            Console.WriteLine("/////////////////////////////////////////////////////");
            Console.WriteLine("//////////// Bem-vindo ao Jogo da Velha! ////////////");
            Console.WriteLine("////////// " + mensagem + " Agora são " + dateTime.ToShortTimeString() + " Hrs! //////////"); // mostra a data e hora do momento incluindo "bom dia, tarde , noite"
            Console.WriteLine("/////////////////////////////////////////////////////");

            // Loop do jogo
            bool Jogar = true;
            while (Jogar)
            {
                Console.Write("Digite o nome do jogador 1 >>>>>>> ");
                string player1 = Console.ReadLine().ToUpper(); // Úsuário digita o nome do jogador 1
                Console.WriteLine("/////////////////////////////////////////////////////");
                Console.Write("Deseja jogar com a IA? (s/n): ");
                string player2 = Console.ReadLine().ToUpper();
                if (player2 == "S")
                {
                player2 = "IA"; // Nome do jogador 2 (IA)
                Console.Clear(); // Limpar a tela
                }
                else
                {
                    Console.WriteLine("/////////////////////////////////////////////////////");
                    Console.Write("Digite o nome do jogador 2 >>>>>>> ");
                    player2 = Console.ReadLine(); // Usuario digita o nome do jogador 2
                }
                // Zerar o tabuleiro
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tabuleiro[i, j] = ' ';
                    }
                }

                bool player1Jogar = true; // Vez do jogador 1

                // Loop da partida
                while (true)
                {
                    Console.Clear(); // Limpar a tela
                    DrawBoard(); // Desenhar o tabuleiro

                    // Verificar se alguém ganhou ou empatou
                    if (CheckWin('X'))
                    {
                        Console.WriteLine($"////////// >>>>> {player1} ganhou!");
                        break;
                    }
                    else if (CheckWin('0'))
                    {
                        Console.WriteLine($"////////// >>>>> {player2} ganhou!");
                        break;
                    }
                    else if (CheckTie())
                    {
                        Console.WriteLine("////////// >>>>> Empate!");
                        break;
                    }
                    
                    // Jogada da IA
                    if (!player1Jogar)
                    {
                        Console.WriteLine($"////////// {player1}, é sua vez (X)!");
                        int[] move = GetPlayerMove();
                        tabuleiro[move[0], move[1]] = 'X';
                    }
                    // Jogada do jogador
                    else if (player2 != "IA")
                    {
                        Console.WriteLine($"////////// {player2}, é sua vez (0)!");
                        int[] move = GetPlayerMove();
                        tabuleiro[move[0], move[1]] = '0';
                        
                    }
                    else
                    {
                        Console.WriteLine($"//////////  {player2} está pensando.....");
                        System.Threading.Thread.Sleep(2500); // Esperar 2 segundo
                        int[] move = GetAIMove();
                        tabuleiro[move[0], move[1]] = '0';
                    }
                    

                    player1Jogar = !player1Jogar; // Trocar a vez dos jogadores
                }

                // Perguntar se quer jogar novamente
                Console.Write("////////// Quer jogar novamente? (s/n): ");
                string resposta = Console.ReadLine().ToLower();
                if (resposta != "s" && resposta != "sim")
                {
                    Jogar = false;
                }
            }
        }

        static void DrawBoard()
        {
            Console.WriteLine("/////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////");
            Console.WriteLine("///////////  J O G O   D A   V E L H A ! ////////////");
            Console.WriteLine("/////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////     1   2   3     /////////////////"); // Colunas
            Console.WriteLine("/////////////////   +---+---+---+   /////////////////");
            Console.WriteLine($"///////////////// 1 | {tabuleiro[0, 0]} | {tabuleiro[0, 1]} | {tabuleiro[0, 2]} |   /////////////////"); // Linha 1
            Console.WriteLine("/////////////////   +---+---+---+   /////////////////");
            Console.WriteLine($"///////////////// 2 | {tabuleiro[1, 0]} | {tabuleiro[1, 1]} | {tabuleiro[1, 2]} |   /////////////////"); // Linha 2
            Console.WriteLine("/////////////////   +---+---+---+   /////////////////");
            Console.WriteLine($"///////////////// 3 | {tabuleiro[2, 0]} | {tabuleiro[2, 1]} | {tabuleiro[2, 2]} |   /////////////////"); // Linha 3
            Console.WriteLine("/////////////////   +---+---+---+   /////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////");
            Console.WriteLine("/X 0 X 0 X 0 X 0 X 0 X 0 X 0 X 0 X 0 X 0 X 0 X 0 X 0/\n");
        }

        static bool CheckTie()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static int[] GetPlayerMove()
        {
            while (true)
            {
                Console.Write("////////// Digite a posição desejada (ex: 1 2): ");
                string[] input = Console.ReadLine().Split(' ');
                int x, y;
                if (input.Length != 2 || !int.TryParse(input[0], out x) || !int.TryParse(input[1], out y))
                {
                    Console.WriteLine("Posição inválida. Tente novamente.");
                    continue;
                }
                x--; // Converter para índice de array (0-2)
                y--;
                if (x < 0 || x > 2 || y < 0 || y > 2)
                {
                    Console.WriteLine("Posição inválida. Tente novamente.");
                    continue;
                }
                if (tabuleiro[x, y] != ' ')
                {
                    Console.WriteLine("Posição já ocupada. Tente novamente.");
                    continue;
                }
                return new int[] { x, y };
            }
        }
        static int[] GetAIMove()
        {
            // Verificar se a IA pode ganhar na próxima jogada
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        tabuleiro[i, j] = '0';
                        if (CheckWin('0'))
                        {
                            tabuleiro[i, j] = ' ';
                            return new int[] { i, j };
                        }
                        tabuleiro[i, j] = ' ';
                    }
                }
            }

            // Verificar se o jogador pode ganhar na próxima jogada e bloqueá-lo
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        tabuleiro[i, j] = 'X';
                        if (CheckWin('X'))
                        {
                            tabuleiro[i, j] = ' ';
                            return new int[] { i, j };
                        }
                        tabuleiro[i, j] = ' ';
                    }
                }
            }

            // Jogar no canto quando estiver vazio
            if (tabuleiro[0, 0] == ' ')
            {
                return new int[] { 0, 0 };
            }

            // Jogar em uma das bordas, se estiver vazio
            int[][] borda = { new int[] { 0, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 }, new int[] { 2, 2 } };
            for (int i = 0; i < borda.Length; i++)
            {
                if (tabuleiro[borda[i][0], borda[i][1]] == ' ')
                {
                    return borda[i];
                }
            }

            // Jogar em qualquer posição vazia restante
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        return new int[] { i, j };
                    }
                }
            }

            // Nunca deve chegar aqui
            throw new Exception("Erro na jogada da IA.");
        }

        static bool CheckWin(char player)
        {
            // Verificar linhas
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == player && tabuleiro[i, 1] == player && tabuleiro[i, 2] == player)
                {
                    return true;
                }
            }
            // Verificar colunas
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[0, j] == player && tabuleiro[1, j] == player && tabuleiro[2, j] == player)
                {
                    return true;
                }
            }

            // Verificar diagonais
            if (tabuleiro[0, 0] == player && tabuleiro[1, 1] == player && tabuleiro[2, 2] == player)
            {
                return true;
            }
            if (tabuleiro[2, 0] == player && tabuleiro[1, 1] == player && tabuleiro[0, 2] == player)
            {
                return true;
            }

            return false;
        }

        public static string Saudacao()
        {
            DateTime agora = DateTime.Now;
            string saudacao = "";

            if (agora.Hour >= 6 && agora.Hour < 12)
            {
                saudacao = "Bom dia!";
            }
            else if (agora.Hour >= 12 && agora.Hour < 18)
            {
                saudacao = "Boa tarde!";
            }
            else
            {
                saudacao = "Boa noite!";
            }

            return saudacao;
        }
    }
}