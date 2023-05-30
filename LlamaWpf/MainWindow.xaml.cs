using LlamaGeneral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace LlamaWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Chat? chat;
        string previousToken = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            repoComboBox.ItemsSource = Models.GetRepositories("D:\\AI\\Models");
            repoComboBox.SelectedIndex = 0;
            repoComboBox_Selected(null, null);
        }

        private void Chat_OnMessageReceived(string token)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if(token == ":" && previousToken == "USER")
                {
                    outputTextBlock.Text = outputTextBlock.Text[..^4];
                    previousToken = token;
                    return;
                }
                outputTextBlock.Text += token;
                previousToken = token;
                outputScrollViewer.ScrollToEnd();
            }));
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (chat == null) throw new NullReferenceException();

            Dispatcher.Invoke(() =>
            {
                var text = inputTextBox.Text;
                Task.Run(() => chat.SendMessage(text));
                inputTextBox.Text = string.Empty;
                outputTextBlock.Text += Environment.NewLine + "Samantha: ";
            });
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                sendButton.IsEnabled = true;
                loadButton.IsEnabled = false;

                chat = new Chat(Models.GetModel(repoComboBox.Text, versionComboBox.Text, "D:\\AI\\Models"), Prompts.Samantha);
                chat.OnTokenReceived += Chat_OnMessageReceived;
            });

        }

        private void repoComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            Task.Run(UpdateVersionComboBox);
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            repoComboBox.ItemsSource = Models.GetRepositories("D:\\AI\\Models");
            repoComboBox.SelectedIndex = 0;
        }

        private void UpdateVersionComboBox()
        {
            Thread.Sleep(10);
            Dispatcher.Invoke(() =>
            {
                versionComboBox.ItemsSource = Models.GetVersions(repoComboBox.Text, "D:\\AI\\Models");
                versionComboBox.SelectedIndex = 0;
            });
        }
    }
}
