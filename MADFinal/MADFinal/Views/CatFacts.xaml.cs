using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MADFinal.Models;
using System.Collections.ObjectModel;
using static MADFinal.Models.CatFacts;
using System.Net.Http;
using Newtonsoft.Json;

namespace MADFinal
{
    public partial class CatFacts : ContentPage
    {
        SensorSpeed speed = SensorSpeed.Game;

        ObservableCollection<Datum> catFacts = new ObservableCollection<Datum>();
        public ObservableCollection<Datum> CatsFacts { get { return catFacts; } }

        public Command LoadRandomCommand { get; set; }


        public CatFacts()
        {
            InitializeComponent();

            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(SensorSpeed.Game);

            LoadRandomCommand = new Command(() => GetRandomCatFacts());

            GetRandomCatFacts();
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CatsFacts.Clear();
                GetRandomCatFacts();
            });
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);

            }
            catch(FeatureNotSupportedException fnsEx)
            {

            }
            catch(Exception ex)
            {

            }
        }

        private async void GetRandomCatFacts()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://catfact.ninja/facts?limit=10");

            var RO = JsonConvert.DeserializeObject<RootObject>(response);

            List<Datum> d = RO.data;

            foreach(var cf in d)
            {
                catFacts.Add(cf);
            }

            ListView.ItemsSource = catFacts;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ListView)sender;
            var catFact = item.SelectedItem;
            DependencyService.Get<ITextToSpeech>().Speak(((MADFinal.Models.CatFacts.Datum)catFact).fact);
        }
    }
}