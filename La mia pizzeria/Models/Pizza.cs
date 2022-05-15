using System.ComponentModel.DataAnnotations;

namespace La_mia_pizzeria.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Il campo Nome è obbligatorio")]
        [StringLength(50, ErrorMessage ="Il nome non può avere più di 50 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'URL dell'immagine è obbligatorio")]
        [Url(ErrorMessage ="URL inserito non è valido")]
        public string Image { get; set; }

        public Pizza()
        {

        }

        public Pizza ( int id, string name , string description , string image)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }
}
