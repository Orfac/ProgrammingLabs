using DictionaryLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuiLab5
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsSpinningProperty =
                DependencyProperty.Register(
                "IsSpinning", typeof(Boolean),
                typeof(Word)
                );
        public string value;

        public MainWindowViewModel()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
