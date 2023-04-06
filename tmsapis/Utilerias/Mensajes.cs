using Commons.ViewModels;

namespace Commons.Utilerias
{
    public class Mensajes
    {
        public static MensajesViewModel Error(string texto)
        {

            return new MensajesViewModel()
            {
                mensaje = texto,
                tipo = 1
            };
        }
        public static MensajesViewModel ErroresAtributos(string texto)
        {
            var error = "Error";

            return new MensajesViewModel()
            {
                mensaje = texto + error,
                tipo = 2
            };
        }
        public static MensajesViewModel Exitoso(string texto)
        {

            return new MensajesViewModel()
            {
                mensaje = texto,
                tipo = 3
            };
        }
        public static MensajesViewModel Informativo(string texto)
        {

            return new MensajesViewModel()
            {
                mensaje = texto,
                tipo = 4
            };
        }
        public static MensajesViewModel Parametro(string texto)
        {

            return new MensajesViewModel()
            {
                mensaje = texto,
                tipo = 5
            };
        }
    }
}
