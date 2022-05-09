using BlazorServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Data
{
    public class VacationsDbInitializer
    {
        public static void Initialize(VacationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any vacations.
            if (context.Vacations.Any())
            {
                //foreach (var vacation in context.Vacations)
                //{
                //    context.Vacations.Remove(vacation);
                //    context.SaveChanges();
                //}                              
                return;   // DB has been seeded
            }

            var vacations = new VacationModel[]
            {
                new VacationModel
                {
                    Title = "Puerto de la Cruz",
                    ImageUrl = "artiom-vallat-H59WxBNSQY8-unsplash.jpg",
                    Price = "359",
                    Rating = "...",
                    PriceUnit = "1 week",
                    Location = "Puerto de la Cruz",
                    Persons = 8,
                    SleepingRooms = 4,
                    Stars = 3
                },
                new VacationModel // done 2
                {
                    Title = "Baia do Sancho, Fernando de Noronha, Brazil", 
                    ImageUrl = "andrew-ruiz-fmz-B9At9iQ-unsplash.jpg",
                    Price = "545", 
                    OriginalPrice = "635",
                    Rating = "7.6",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Fernando de Noronha, Brazil",
                    PriceUnit = "1 week"
                },
                new VacationModel 
                {
                    Title = "Santa Barbara Beach & Golf Resort, Curacao",
                    ImageUrl = "bernard-hermant-FOCO61JQ8EA-unsplash.jpg",
                    Price = "975",                    
                    Rating = "7.5",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Curacao",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Fuerteventura",
                    ImageUrl = "hugues-de-buyer-mimeure-atFTfykyUK0-unsplash.jpg",
                    Price = "949",
                    OriginalPrice = "1099",
                    Rating = "7.8",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Fuerteventura",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Kontokali Bay",
                    ImageUrl = "mariya-georgieva-wwV3ZMLaAmg-unsplash.jpg",
                    Price = "869",
                    OriginalPrice = "1040",
                    Rating = "8.6",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Kontokali Bay",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Nice cottage nearby Salzburg, Austria",
                    ImageUrl = "martin-adams-Kfb21cW3lFk-unsplash.jpg",
                    Price = "499",
                    OriginalPrice = "549",
                    Rating = "7.9",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Salzburg, Austria",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Puerto Calero",
                    ImageUrl = "melissa-walker-horn-jsEftnpVh1U-unsplash.jpg",
                    Price = "799",
                    OriginalPrice = "939",
                    Rating = "8.7",
                    Stars = 4,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Puerto Calero",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Bar Harbor, United States",
                    ImageUrl = "michael-schaffler-9IPc_aZLd80-unsplash.jpg",
                    Price = "649",
                    OriginalPrice = "815",
                    Rating = "8.8",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Bar Harbor, United States",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Luxurious hotel in Taormina, Italy",
                    ImageUrl = "mick-tinbergen-6wlaFgeG7G4-unsplash.jpg",
                    Price = "809",
                    OriginalPrice = "889",
                    Rating = "8.0",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Taormina, Italy",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Radisson Blu Resort and Spa nearby Cesme, Turkey",
                    ImageUrl = "mindaugas-petrutis-fV2dM2WvKvE-unsplash.jpg",
                    Price = "419",                    
                    Rating = "7.9",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Cesme, Turkey",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Bora Bora Four Seasons Hotel",
                    ImageUrl = "mohamed-ahsan-zfwfZHWqeWM-unsplash.jpg",
                    Price = "1149",
                    OriginalPrice = "1249",
                    Rating = "8.8",
                    Stars = 3,
                    Persons = 6,
                    SleepingRooms = 2,
                    Location = "Bora Bora",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Pittoresque cottage in Maldives",
                    ImageUrl = "mohamed-ali-oji_NGmBI5o-unsplash.jpg",
                    Price = "825",
                    OriginalPrice = "915",
                    Rating = "8.4",
                    Stars = 5,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Maldives",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Resort in Dubai",
                    ImageUrl = "nick-fewings-3RM6t46ZOBs-unsplash.jpg",
                    Price = "1450",
                    OriginalPrice = "1625",
                    Rating = "9.1",
                    Stars = 5,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Dubai",
                    PriceUnit = "1 week"
                },
                new VacationModel // done
                {
                    Title = "Fire Island, United States",
                    ImageUrl = "ostap-senyuk-Zv6tZ5oVunE-unsplash.jpg",
                    Price = "759",
                    OriginalPrice = "839",
                    Rating = "8.6",
                    Stars = 4,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Fire Island, United States",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Modern Middle-Eastern resort in Dubai",
                    ImageUrl = "rktkn-ssOtyGE8CyE-unsplash.jpg",
                    Price = "1325",
                    OriginalPrice = "1499",
                    Rating = "8.4",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Dubai",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Romantic hotel in Malfa, Italy",
                    ImageUrl = "seth-weisfeld-hFVZ-oQiuhc-unsplash.jpg",
                    Price = "525",
                    OriginalPrice = "615",
                    Rating = "8.2",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Malfa, Italy",
                    PriceUnit = "1 week"
                },
                new VacationModel
                {
                    Title = "Punta Cana, Dominican Republic",
                    ImageUrl = "shawn-lee-0sSJMTfLXUI-unsplash.jpg",
                    Price = "769",
                    OriginalPrice = "890",
                    Rating = "8.8",
                    Stars = 4,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Punta Cana, Dominican Republic",
                    PriceUnit = "1 week"
                },
                new VacationModel // done
                {
                    Title = "Markane, Norway",
                    ImageUrl = "steinar-engeland-IlOZC5bi5Wg-unsplash.jpg",
                    Price = "625",
                    OriginalPrice = "729",
                    Rating = "5.8",
                    Stars = 2,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Markane, Norway",
                    PriceUnit = "1 week"
                },
                new VacationModel // done
                {
                    Title = "Varadero Beach Cuba",
                    ImageUrl = "suttipong-surak-ejevWP2gZDk-unsplash.jpg",
                    Price = "1139",
                    OriginalPrice = "979",
                    Rating = "8.2",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Cuba",
                    PriceUnit = "1 week"
                },
                new VacationModel // done
                {
                    Title = "Eagle Beach, Aruba",
                    ImageUrl = "toa-heftiba-Ezww9k_Gefs-unsplash.jpg",
                    Price = "599",
                    OriginalPrice = "549",
                    Rating = "7.6",
                    Stars = 3,
                    Persons = 10,
                    SleepingRooms = 5,
                    Location = "Eagle Beach, Aruba",
                    PriceUnit = "1 week"
                }
            };
            
            foreach (var v in vacations)
            {
                context.Vacations.Add(v);
            }
            context.SaveChanges();
        }
    }
}
