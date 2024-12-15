/* 07/12/2024:  Nova values     */

using System.ComponentModel.DataAnnotations;

namespace Values
{
    public class Service_t
    {   
        [Required]
        public  string Name   { get; set; }
        public  string Desc   { get; set; }
        public  string Author { get; set; }
        public  bool   State  { get; set; }
    }
}