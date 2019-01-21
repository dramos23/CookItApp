namespace CookItApp.Models
{
    public class TipoIngrediente
    {
        public int _IdTipoIngrediente { get; set; }
        public int _Nombre { get; set; }

        public TipoIngrediente()
        {
        }

        public TipoIngrediente(int IdTipoIngrediente, int Nombre)
        {
            _IdTipoIngrediente = IdTipoIngrediente;
            _Nombre = Nombre;
        }
    }
}