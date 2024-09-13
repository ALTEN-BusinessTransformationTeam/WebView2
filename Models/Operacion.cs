namespace ReactWebView2_Template.Models
{
    public class Operacion
    {
        public int ID { get; set; }
        public string PN { get; set; }
        public string Estado { get; set; }

        public virtual Orden Orden { get; set; }
    }
}
