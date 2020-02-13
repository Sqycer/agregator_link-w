using ComponetRegister.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

namespace ComponetRegisterClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient clinet;
        public ObservableCollection<Register> registers { get; set; }
        public Register register { get; set; }
        private string ApiRout = "api/ComponentRegister";

        public MainWindow()
        {
            registers= new ObservableCollection<Register>();     
        InitializeComponent();
            clinet = new HttpClient()
            {
                BaseAddress = new System.Uri("http://localhost:64920")
            };
            DataContext = this;
        }

    
        private async Task GetRegisters()
        {
            var respons = await clinet.GetAsync(ApiRout);
            var content = await respons.Content.ReadAsAsync<IEnumerable<Register >> ();
            registers.Clear();
            foreach(var elemnt in content)
            {
                registers.Add(elemnt);
            }
           // Listbox.ItemsSource = registers;
        }

        private async void  Get(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetRegisters();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Clear(object sender, RoutedEventArgs e)
        {
            try
            {
                await clinet.DeleteAsync(ApiRout);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
