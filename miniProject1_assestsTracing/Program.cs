using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace ConsoleApp3
{
    internal class Program
    {
        //Method to Quit the Program
        static bool Quit(String str)
        {
            if (str.ToLower().Trim() == "exit")
            {
                return true;
            }
            return false;
        }
        //Method to print/display the Program
        static void print(List<Assests> assests)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            // The items are sorted by Office and then by Purchase Date have currency according to country
            List<Assests> sortedByDates = assests.OrderBy(assest => assest.Office).ThenBy(assest => assest.PurchaseDate).ToList();
            //Using padright to display better for Users
            Console.WriteLine("TYPE".PadRight(10) + "BRAND".PadRight(10) + "MODEL".PadRight(10) + "OFFICE".PadRight(10) + "DATE".PadRight(15) + "PRICE".PadRight(10) + "CURRENCY".PadRight(10) + "LOCAL PRICE".PadRight(10) + "\n");

            foreach (Assests assest in sortedByDates)
            {
                Console.ResetColor();
                var purchaseDate = (DateTime.Now).Subtract(assest.PurchaseDate);
                string priceConverted = "";
                float conversionRate = 10.00f;
                float euroConversion = 0.94f;
                float norwayConversion = 9.56f;
                //Converting the Currency with currency Symbols
                switch (assest.Currency)
                {
                    case "EUR":
                        priceConverted = (assest.Price * euroConversion).ToString("C2", CultureInfo.GetCultureInfo("en-GB"));
                        break;
                    case "SEK":
                        priceConverted = (assest.Price * conversionRate).ToString("C2", CultureInfo.GetCultureInfo("sv-SE"));
                        break;
                    case "NOK":
                        priceConverted = (assest.Price * norwayConversion).ToString("C2", CultureInfo.GetCultureInfo("nb-NO"));
                        break;
                    case "DOLLAR":
                        priceConverted = assest.Price.ToString("C2", CultureInfo.GetCultureInfo("en-US"));
                        break;
                }

                // Items are *RED* if the date is less than 3 months away from 3 years and Yellow* if the date is less than 6 months away from 3 years
                if (purchaseDate.Days > (365 * 3))
                {
                    if (purchaseDate.Days > ((365 * 3) + (31 * 6)))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
                //Printing out all the PRODCUTS in the LISTS
                Console.WriteLine(assest.Type.PadRight(10) + assest.Brand.PadRight(10) + assest.Model.PadRight(10) + assest.Office.PadRight(10) +
                assest.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) + "$" + assest.Price.ToString().PadRight(10) + assest.Currency.PadRight(10) + priceConverted);
                Console.ResetColor();
            }
        }
        static void Main(string[] args)
        {
            // Instantiating variables used throughout code
            Console.WriteLine("This is an Assests Company - Where everthing is saved in a DataBase: 'EXIT' in the Electronic TYPE TO SEE THE LIST");
            //Initilizing the List
            List<Assests> assests = new List<Assests>();
            string type = "";
            string brand = "";
            string model = "";
            string office = "";
            DateTime purchaseDateIs = DateTime.Now;
            string currency = "";
            float price = 0;
            
            // Loop for repeated Questions "Exit" To break the loop
            while (true)
            {
                enterType: Console.Write("Enter the Electronic Type: PHONE OR LAPTOP: ");
                type = Console.ReadLine();
                bool quit = Quit(type);
                //If user type Quit it will break the loop and prints out all the Product
                if (quit)
                {
                    print(assests);
                    break;
                }
                else
                {
                    if (type.ToUpper().Trim() == "PHONE" || type.ToUpper().Trim() == "LAPTOP")
                    {
                        Console.Write("Enter the Brand Type: ");
                        brand = Console.ReadLine();
                        Console.Write("Enter the Model Name: ");
                        model = Console.ReadLine();
                        Console.Write("Enter the Office Location Type: ");
                        office = Console.ReadLine();
                    //DateTime Method and Error Handling
                    enterDateAgain: try
                        {
                            Console.Write("Enter the Purchase Date - yyyy-MM-dd: ");
                            purchaseDateIs = Convert.ToDateTime(Console.ReadLine());

                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("This is a DateTime Exception: Try using yyyy-MM-dd:");
                            Console.ResetColor();
                            goto enterDateAgain;

                        }
                        //Try to catch any exception if there is any ask for the input again
                    enterPriceAgain: try
                        {
                            Console.Write("Enter the Price of the Product in US Dollars: ");
                            price = float.Parse(Console.ReadLine());

                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("This is a format Exception:");
                            Console.ResetColor();
                            goto enterPriceAgain;
                        }
                        while (true)
                        {
                        enterCurrencyAgain: Console.Write("Enter the The Currency of the Country Type. Choose EUR/DOLLAR/SEK/NOK: ");
                            currency = Console.ReadLine().ToUpper();
                            //Asking for Currency Options
                            if (currency == "SEK" || currency == "EUR" || currency == "NOK" || currency == "DOLLAR") 
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("This currency not valid; Please try again!!");
                                goto enterCurrencyAgain;
                            }
                        }

                        // Creating Objects from the Class ASSESTS
                        Assests assest1 = new(type, brand, model, office, purchaseDateIs, price, currency);
                        //Adding everything to the LIST
                        assests.Add(assest1);
                    }
                    else
                        goto enterType;
                }
            }
            Console.ReadLine();
        }
    }
    class Assests
    {
        public Assests(string type, string brand, string model, string office, DateTime purchaseDate, float price, string currency)
        {
            Type = type;
            Brand = brand;
            Model = model;
            Office = office;
            PurchaseDate = purchaseDate;
            Price = price;
            Currency = currency;
        }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public DateTime PurchaseDate { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
    }
}