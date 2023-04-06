namespace Commons.Models.Utilites
{
    public class PaginacionModel
    {
        public int PaginaActual { get; set; }
        public int? TotalRegistros { get; set; }
        public int RegistroXPagina { get; set; }
    }
}
