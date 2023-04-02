using Newtonsoft.Json;
using RestSharp;
using SaveUp.Models;
using SaveUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp
{
    public static class API
    {
        private static RestClient client = new RestClient("https://10.0.2.2:7193/api/DataManagment"); //https:10.0.2.2:7193/api/DataManagment (Für Emulator) https:Localhost:7193/api/DataManagment (für Window machine)

        private static RestRequest request = new RestRequest();

        /// <summary>
        /// API Get Request um Produkte zu holen
        /// </summary>
        /// <returns>Liste von allen Produkten</returns>
        public static ObservableCollection<ProductData> GetProductData() 
        {
            var GetProducts = client.Get(request);

            return JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(GetProducts.Content);
        } 

        /// <summary>
        /// API Post Request um ein neues Produkt hinzuzufügen
        /// </summary>
        /// <param name="productDataPost"></param>
        public static void PostProductData(ProductDataPost productDataPost) 
        {
            var PostRequest = new RestRequest().AddJsonBody(productDataPost);
            
            client.Post(PostRequest);
        }

        /// <summary>
        /// API Delete Request um alle Produkte zu löschen
        /// </summary>
        public static void DeleteAllProductData() 
        {
            var DeleteAllRequest = new RestRequest();

            client.Delete(DeleteAllRequest);
        }

        /// <summary>
        /// API delete Request um ein Product mit Id zu löschen
        /// </summary>
        /// <param name="Id">Id des Produktes was gelöscht werden los</param>
        public static void DeleteOneProductData(Guid Id)
        {
            RestClient clientDelete = new RestClient("https://10.0.2.2:7193/api/DataManagment/" + Id.ToString());

            var DeleteAllRequest = new RestRequest();

            clientDelete.Delete(DeleteAllRequest);
        }
    }
}
