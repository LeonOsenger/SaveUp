using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using RestSharp;
using SaveUp.Models;

namespace SaveUp.ViewModels
{
    public class ListViewModel : ObservableObject
    {
        private ObservableCollection<ProductData> _productList { get; set; }

        private string _totalPriceText { get; set; }
        private ProductData _productSelected;

        public ICommand DeleteOneCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }

        public ObservableCollection<ProductData> platzhalter = new ObservableCollection<ProductData>();

        /// <summary>
        /// Ctor
        /// </summary>
        public ListViewModel()
        {
            _productList = new ObservableCollection<ProductData>();
            try
            {
                _productList = API.GetProductData();
            }
            catch (Exception) { }
            _totalPriceText = BuildTotalPriceText();

            DeleteOneCommand = new RelayCommand(DeleteOne);
            DeleteAllCommand = new RelayCommand(DeleteAll);
        }

        /// <summary>
        /// Logik für Button zum nur das Ausgewählte Produkt zu löschen
        /// </summary>
        public async void DeleteOne()
        {
            if(_productSelected != null)
            {
                bool deleteOne = await App.Current.MainPage.DisplayAlert("Ausgewähltes Löschen", "Wollen sie das Ausgewählte Löschen?", "Delete", "Cancel");
                if(deleteOne) 
                {
                    try
                    {
                        API.DeleteOneProductData(_productSelected.Id);
                    } 
                    catch (Exception) { }
                }
                LoadData();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Produckt Auswählen", "Sie müssen ein Produkt auswählen", "Cancel");
            }
        }

        /// <summary>
        /// Logik für Button zum alle Produkte zu löschen
        /// </summary>
        public async void DeleteAll()
        {
            bool deleteAll = await App.Current.MainPage.DisplayAlert("Alles Löschen", "Wollen sie alles Löschen?", "Delete", "Cancel");
            if(deleteAll) 
            {
                ProductList.Clear();
                try
                {
                    API.DeleteAllProductData();
                }
                catch (Exception) { }

                LoadData();
            }
        }

        /// <summary>
        /// Propertie für die Liste mit den Produkten
        /// </summary>
        public ObservableCollection<ProductData> ProductList
        {
            get => _productList;
            set
            {
                if(_productList != value) 
                { 
                    _productList = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propertie für das Produkt was angeklickt ist bei der Liste mit den Produkten
        /// </summary>
        public ProductData ProductSelected
        {
            get => _productSelected;
            set 
            {
                if(_productSelected != value) 
                {
                    _productSelected = value; 
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propertie für das anzegen des totalen Preises
        /// </summary>
        public string TotalPriceText
        {
            get => _totalPriceText;
            set 
            { 
                if(value != _totalPriceText) 
                {
                    _totalPriceText = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Funktion um daten von API in die Listemit den Produkten zu Laden
        /// </summary>
        public void LoadData()
        {
            try
            {
                ProductList = API.GetProductData();
            }
            catch (Exception) 
            { }
            TotalPriceText = BuildTotalPriceText();
        }

        /// <summary>
        /// Funktion der den Totalen Preis ausrechnet und den auzugebenen Sting zusammenstellt.
        /// </summary>
        /// <returns></returns>
        private string BuildTotalPriceText()
        {
            double totalPrice = 0;

            foreach (var p in _productList) 
            {
                totalPrice += p.price;
            }

            totalPrice = Math.Round(totalPrice, 2);

            return $"{ totalPrice } CHF";
        }
    }
}
