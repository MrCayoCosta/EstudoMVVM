﻿using Mimica.Armazanemanto;
using Mimica.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;


namespace Mimica.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        public Jogo Jogo { get; set; }
        public Command IniciarCommand { get; set; }

        public InicioViewModel()
        {
            IniciarCommand = new Command(IniciarJogo);
            Jogo = new Jogo();
            Jogo.Grupo1 = new Grupo();
            Jogo.Grupo2 = new Grupo();
        }

        private void IniciarJogo()
        {
            Armazenamento.Jogo = this.Jogo;
            Armazenamento.RodadaAtual = 1;
            App.Current.MainPage = new View.Jogo(Jogo.Grupo1);
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
