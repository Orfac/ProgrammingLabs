using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DictionaryLib.Models;
using DictionaryLib.Services;

namespace GuiLab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChannelFactory<IDictionaryContract> _factory;
        private IDictionaryContract _channel;

        public MainWindow()
        {
            InitializeComponent();
            _factory = new ChannelFactory<IDictionaryContract>
                (new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/IDictionaryContract")
                );
            _channel = _factory.CreateChannel();
        }

        private void SearchingField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (!String.IsNullOrWhiteSpace(textBox.Text))
                {

                    SearchingFieldResponse.Text = _channel.FindWord(textBox.Text);
                }
            }
        }

        private Word ConvertStringToWord(string unparsedFullWord)
        {
            string[] unparsedMorphemes = unparsedFullWord.Split('-');
            bool isRootFound = false;
            string rootValue = null;
            List<Morpheme> morphemes = new List<Morpheme>();
            var fullWord = new StringBuilder();
            for (int i = 0; i < unparsedMorphemes.Length; i++)
            {
                if (IsRoot(unparsedMorphemes[i]))
                {
                    isRootFound = true;
                    rootValue = unparsedMorphemes[i].Substring(
                        1, unparsedMorphemes[i].Length - 2);
                    morphemes.Add(new Morpheme(EMorphemeType.Root, rootValue));
                    fullWord.Append(rootValue);
                }
                else
                {
                    EMorphemeType type;
                    if (isRootFound)
                    {
                        type = EMorphemeType.Suff;
                    }
                    else
                    {
                        type = EMorphemeType.Pref;
                    }
                    morphemes.Add(new Morpheme(type, unparsedMorphemes[i]));
                    fullWord.Append(unparsedMorphemes[i]);
                }
            }
            return new Word(morphemes, fullWord.ToString(), rootValue);
        }

        private bool IsRoot(string morpheme)
        {
            return morpheme[0] == '[' &&
                morpheme[morpheme.Length - 1] == ']';
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(SearchingField.Text))
            {
                SearchingFieldResponse.Text = _channel.AddWord(
                    ConvertStringToWord(SearchingField.Text)
               );
            }
        }

        private new void LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = "Введите слово...";
            }
        }
        private new void GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = "";
            }
        }

    }
}