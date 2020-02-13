using agregator_linków.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Viewmodel
{
    public class ViewAddLink
    {
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        [RegularExpression (@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})", ErrorMessage = "Nie prawidłowy wpisany Url")]
        public string url { get; set; }
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        public string title { get; set; }
        public  User user { get; set; }
        public DateTime time { get; set; }
        public int like { get; set; }

    }
}
