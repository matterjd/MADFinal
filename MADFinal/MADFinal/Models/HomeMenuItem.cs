using System;
using System.Collections.Generic;
using System.Text;

namespace MADFinal.Models
{
    public enum MenuItemType
    {
        ToDos,
        CatFacts
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
