﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_JSON;

namespace BS_PokedexManager
{
    public class ListClass : INotifyPropertyChanged
    {
        public ListClass(ObservableCollection<Pokemon> listPokemons)
        {
            OriginalListPokemons = listPokemons;
            ListPokemons = OriginalListPokemons;
        }

        private ObservableCollection<Pokemon> _listPokemons;
        public ObservableCollection<Pokemon> ListPokemons
        {
            get
            {
                return _listPokemons;
            }
            set
            {
                _listPokemons = value;
                NoticeMe("ListPokemons");
            }
        }

        public void NoticeMe(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Pokemon> OriginalListPokemons { get; set; }
        public string ChosenType { get; set; }
        public string ChosenOrder { get; set; }
    }
}
