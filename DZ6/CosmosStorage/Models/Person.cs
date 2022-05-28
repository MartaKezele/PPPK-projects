using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CosmosStorage.Models
{
    public class Person
    {
        [JsonProperty(propertyName:"id")]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(propertyName:"firstName")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty(propertyName:"lastName")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty(propertyName:"age")]
        public int Age { get; set; }
    }
}