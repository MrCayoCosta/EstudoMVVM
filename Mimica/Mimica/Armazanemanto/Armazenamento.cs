using System;
using System.Collections.Generic;
using System.Text;
using Mimica.Model;

namespace Mimica.Armazanemanto
{
    public class Armazenamento
    {
        public static Jogo Jogo { get; set; }
        public static short RodadaAtual { get; set; }

        public static string[][] Palavras =
        {
            //Fácil
            new string[]{"Olho", "Língua", "Tênis", "Milho", "Chinelo", "Bola", "Ping-Pong"},

            //Médio
            new string[]{"Marceneiro", "Carpinteiro", "Amarelo", "Limão", "Abelha", "Polícia", "Mundo", "Fogo"},

            //Difícil
            new string[]{"Paralelepípedo", "Lanterna", "Lâmpada", "Bombeiro", "Batman", "Homem de ferro", "Prédio"},
       };
    }
}
