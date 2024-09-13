namespace ReactWebView2_Template.Models
{
    public class Orden
    {
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Site { get; set; }
        public DateTime? FechaIni { get; set; }

        public virtual ICollection<Operacion>? Operaciones { get; set; }
    }
}
