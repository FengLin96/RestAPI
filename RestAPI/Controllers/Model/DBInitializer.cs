
using System.Linq;
using RestAPI.Controllers.Models;

namespace Model
{
    public class DBIntitializer
    {
        public static void Initialize(LibraryContext context)
        {
            //database creeren als nog niet bestaat
            context.Database.EnsureCreated();
            
            if (!context.Planeten.Any())
            {
                var mercurius = new Planet()
                {
                    Uitleg = "Genoemd naar Mercurius, de Romeinse god van de handel en boodschapper der goden."
                };
                context.Planeten.Add(mercurius);

                var Venus = new Planet()
                {
                    Uitleg = "Genoemd naar Venus, de Romeinse godin van de liefde en schoonheid."
                };
                context.Planeten.Add(Venus);

                var Aalst = new Stad()
                {
                    Id = 1,
                    Naam = "Aalst",
                    Provincie = "Oost-Vlaanderen",
                    Inwoners = 82587
                };
                
                context.Steden.Add(Aalst);
                var Aarlen = new Stad()
                {
                    Id = 2,
                    Naam = "Aarlen",
                    Provincie = "Luxemburg",
                    Inwoners = 28520
                };
                context.Steden.Add(Aarlen);
                //Save all the changes to the DB
                context.SaveChanges();
            }
        }
    }
}


                

                