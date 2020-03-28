using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Mimica.Armazanemanto;
using Mimica.Model;

namespace Mimica.ViewModel
{
    public class JogoViewModel : INotifyPropertyChanged
    {
        public Grupo Grupo { get; set; }
        public string NumeroGrupo { get; set; }
        public string NomeGrupo { get; set; }

        private string _Palavra { get; set; }
        public string Palavra { get { return _Palavra; } set { _Palavra = value; OnPropertyChanged("Palavra"); } }

        private string _TextoContagem { get; set; }
        public string TextoContagem { get { return _TextoContagem; } set { _TextoContagem = value; OnPropertyChanged("TextoContagem"); } }

        private byte _PalavraPontuacao;
        public byte PalavraPontuacao { get { return _PalavraPontuacao; } set { _PalavraPontuacao = value; OnPropertyChanged("PalavraPontuacao"); } }

        private bool _IsVisibleContainerContagem;
        public bool IsVisibleContainerContagem { get { return _IsVisibleContainerContagem; } set { _IsVisibleContainerContagem = value; OnPropertyChanged("IsVisibleContainerContagem"); } }

        private bool _IsVisibleContainerIniciar;
        public bool IsVisibleContainerIniciar { get { return _IsVisibleContainerIniciar; } set { _IsVisibleContainerIniciar = value; OnPropertyChanged("IsVisibleContainerIniciar"); } }

        private bool _IsVisibleBtnMostrar;
        public bool IsVisibleBtnMostrar { get { return _IsVisibleBtnMostrar; } set { _IsVisibleBtnMostrar = value; OnPropertyChanged("IsVisibleBtnMostrar"); } }

        private bool _IsVisibleContainerDecisao;
        public bool IsVisibleContainerDecisao { get { return _IsVisibleContainerDecisao; } set { _IsVisibleContainerDecisao = value; OnPropertyChanged("IsVisibleContainerDecisao"); } }

        public Command MostrarPalavra { get; set; }
        public Command Acertou { get; set; }
        public Command Errou { get; set; }
        public Command Iniciar { get; set; }

        public JogoViewModel(Grupo grupo)
        {
            Grupo = grupo;
            NomeGrupo = grupo.Nome;
            if (grupo == Armazenamento.Jogo.Grupo1)
            {
                NumeroGrupo = "Grupo 1";
            }
            else
            {
                NumeroGrupo = "Grupo 2";
            }


            IsVisibleContainerContagem = false;
            IsVisibleContainerIniciar = false;
            IsVisibleContainerDecisao = false;
            IsVisibleBtnMostrar = true;
            Palavra = "****************";

            MostrarPalavra = new Command(MostrarPalavraAction);
            Acertou = new Command(AcertouAction);
            Errou = new Command(ErrouAction);
            Iniciar = new Command(IniciarAction);
        }

        private void AcertouAction()
        {
            Grupo.Pontuacao += PalavraPontuacao;
            ProximoGrupo();
        }

        private void ProximoGrupo()
        {
            Grupo grupo;
            if (Armazenamento.Jogo.Grupo1 == Grupo)
            {
                grupo = Armazenamento.Jogo.Grupo2;
            }
            else
            {
                grupo = Armazenamento.Jogo.Grupo1;
                Armazenamento.RodadaAtual++;
            }
            if (Armazanemanto.Armazenamento.RodadaAtual > Armazenamento.Jogo.Rodadas)
            {
                App.Current.MainPage = new View.Resultado();
            }
            else
            {
                App.Current.MainPage = new View.Jogo(grupo);
            }

        }

        private void ErrouAction()
        {
            ProximoGrupo();
        }

        private void IniciarAction(object obj)
        {
            IsVisibleContainerIniciar = false;
            IsVisibleContainerContagem = true;
            //fazer um if se a pessoa acertou ou errou
            int tempo = Armazanemanto.Armazenamento.Jogo.TempoPalavra;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TextoContagem = tempo.ToString();
                tempo--;
                if (tempo < 0)
                {
                    TextoContagem = "Tempo esgotado!";
                }
                return true;
            });
        }

        private void MostrarPalavraAction()
        {
            Palavra = "Sentar";
            IsVisibleBtnMostrar = false;
            IsVisibleContainerIniciar = true;
            IsVisibleContainerDecisao = true;

            var NumNivel = Armazanemanto.Armazenamento.Jogo.NivelNumerico;

            if (NumNivel == 0)
            {
                Random r = new Random();
                int nivel = r.Next(0, 2);
                int i = r.Next(0, Armazanemanto.Armazenamento.Palavras[nivel].Length);
                Palavra = Armazanemanto.Armazenamento.Palavras[nivel][i];
                PalavraPontuacao = (byte)((nivel == 0) ? 1 : (nivel == 1) ? 3 : 5);
                //Aleatório

            }
            if (NumNivel == 1)
            {
                Random r = new Random();
                int i = r.Next(0, Armazenamento.Palavras[NumNivel - 1].Length);
                Palavra = Armazenamento.Palavras[NumNivel - 1][i];
                PalavraPontuacao = 1;
                //Fácil
            }
            if (NumNivel == 2)
            {
                Random r = new Random();
                int i = r.Next(0, Armazenamento.Palavras[NumNivel - 1].Length);
                Palavra = Armazenamento.Palavras[NumNivel - 1][i];
                PalavraPontuacao = 1;
                //Médio
            }
            if (NumNivel == 3)
            {
                Random r = new Random();
                int i = r.Next(0, Armazenamento.Palavras[NumNivel - 1].Length);
                Palavra = Armazenamento.Palavras[NumNivel - 1][i];
                PalavraPontuacao = 1;
                //Difícil
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }
}
