using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agregator_linków.Viewmodel
{
    public class ViewEditLink
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        [RegularExpression(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})", ErrorMessage = "Nie prawidłowy wpisany Url")]
        public string url { get; set; }
        [Required(ErrorMessage = "Pole jest obowiąskowe")]
        public string title { get; set; }

    }
}
