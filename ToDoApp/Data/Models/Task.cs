namespace ToDoApp.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string? Naslov { get; set; }
        public string? Opis { get; set; }
        public DateTime? DatumUstvarjanja { get; set; }
        public bool? Opravljeno { get; set; }
    }
}
