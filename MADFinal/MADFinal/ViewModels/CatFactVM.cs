using System;
using System.Windows.Input;

using Xamarin.Forms;
using MADFinal.ViewModels;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static MADFinal.Models.CatFacts;

namespace MADFinal
{
    public class CatFactVM : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<Datum> CatFacts { get; set; }


        public CatFactVM()
        {
            CatFacts = new ObservableCollection<Datum>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            //try
            //{
            //    CatFacts.Clear();
            //    //var cf = await 
            //}
        }


        public ICommand OpenWebCommand { get; }

        public string Title { get; set; }
    }
}