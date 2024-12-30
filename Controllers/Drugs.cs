using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace E_Vita.Controllers
{

    public class Drug
    {
        public string ActiveIngredient { get; set; }
        public string Company { get; set; }
        public string Created { get; set; }
        public string Form { get; set; }
        public string Group { get; set; }
        public string Id { get; set; }
        public string new_price { get; set; }
        public string Pharmacology { get; set; }
        public string Route { get; set; }
        public string Tradename { get; set; }
        public string Updated { get; set; }
    }
    public class DrugData
    {
        public List<Drug> Drugs { get; set; }
    }

    public class DrugsController
    {
        // Property to store the loaded drugs
        public DrugData Data { get; private set; }

        // Function to load drugs from the JSON file
        public void LoadDrugs()
        {
            string path = "D:\\NU\\fifth semester\\Clinical\\E-Vita\\Assets\\Json_files\\medications_New_prices_up_to_03-08-2024.json";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The JSON file was not found at the specified path.");
            }

            // Read the JSON file
            string json = File.ReadAllText(path);

            // Deserialize the JSON into a DrugData object
            Data = JsonConvert.DeserializeObject<DrugData>(json);

            if (Data == null || Data.Drugs == null || !Data.Drugs.Any())
            {
                throw new InvalidDataException("No drug data found in the JSON file.");
            }
        }

        // Method to retrieve a drug by tradename
        //public Drug GetDrugByTradename(string tradename)
        //{
        //    if (Data == null || Data.Drugs == null)
        //    {
        //        throw new InvalidOperationException("Drug data is not loaded. Call LoadDrugs() first.");
        //    }

        //    // Search for the drug by tradename
        //    return Data.Drugs.FirstOrDefault(d => d.Tradename.Equals(tradename, StringComparison.OrdinalIgnoreCase));
        //}
    }


}
