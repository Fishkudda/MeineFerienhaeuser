using MeineFerienhaeuser.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeineFerienhaeuser.Pages
{
    public class HausModel : PageModel
    {
        private readonly ILogger<HausModel> _logger;

        public List<House> AllHouses { get; set; } // Full list of houses
        private List<House> _houses { get; set; } // Paginated list of houses
  
        public string AltImg { get; set; }

        public int CurrentPage { get; set; } // Current page number
        public int TotalPages { get; set; } // Total number of pages

        public int ItemsPerPage { get; set; } = 6; // Number per Houses per Page

        public List<SelectListItem> HouseCountSelectItems { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "6", Text = "6 Häuser pro Seite" },
            new SelectListItem { Value = "12", Text = "12 Häuser pro Seite" },
            new SelectListItem { Value = "24", Text = "24 Häuser pro Seite" },
            new SelectListItem { Value = "50", Text = "50 Häuser pro Seite" }
        };

        public HausModel(ILogger<HausModel> logger, AppSettings settings)
        {

            _logger = logger;
            DataLoader dataLoader = new DataLoader(settings);
            _houses = dataLoader.Load();
            this.AltImg = settings.DefaultImagePath;

        }


        public List<House> GetHouses()
        {

            return _houses;
        }

         public void OnGet([FromQuery] int itemsPerPage = 6, [FromQuery] int page = 1)
         {

            AllHouses = _houses;
            ItemsPerPage = itemsPerPage > 0 ? itemsPerPage : 6; 
            CurrentPage = page > 0 ? page : 1; 
            TotalPages = (int)Math.Ceiling((double)AllHouses.Count / ItemsPerPage);
            if (CurrentPage > TotalPages) CurrentPage = TotalPages; 
            if (CurrentPage < 1) CurrentPage = 1;
            int skip = (CurrentPage - 1) * ItemsPerPage;
            _houses = AllHouses.Skip(skip).Take(ItemsPerPage).ToList();
         }


    }
}
