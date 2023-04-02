using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaveUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaveUp.ViewModels
{
    public class AddViewModel : ObservableObject
    {   
        private string _descriptionEntry { get; set; }
        private double _priceEntry { get; set; }

        public ICommand PostCommand { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public AddViewModel()
        {
            PostCommand = new RelayCommand(PostRequest);
        }

        /// <summary>
        /// Propertie von Description um Produkt hinzuzufügen 
        /// </summary>
        public string DescriptionEntry
        {
            get => _descriptionEntry;
            set
            {
                if(_descriptionEntry != value) 
                {
                    _descriptionEntry = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propertie von preis um Produkt hinzuzufügen 
        /// </summary>
        public double PriceEntry
        {
            get => _priceEntry;
            set
            {
                if(_priceEntry != value) 
                {
                    _priceEntry = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Command für Button der Produkt hinzufügt
        /// </summary>
        public async void PostRequest()
        {
            if(_descriptionEntry != null && _priceEntry != 0)
            {
                try
                {
                    API.PostProductData(new ProductDataPost() { description = _descriptionEntry, price = _priceEntry });
                }
                catch (Exception) { }
                DescriptionEntry = string.Empty;
                PriceEntry = 0;
            }else
            {
                await App.Current.MainPage.DisplayAlert("Richtige eingabe", "Sie müssen einen Richtigen Preis und eine Beschreibung eingeben", "Cancel");
            }
            
        }
    }
}
