namespace ChannelMonitor.Api.Validation
{
    public static class Utilities
    {
        public static string Required = "El campo {PropertyName} es requerido";
        public static string MaxLenth = "El campo {PropertyName} debe tener menos de {MaxLength} caracteres";
        public static string FirstLetterMayus = "El campo {PropertyName} debe comenzar con mayúsculas";
        public static string EmailMessage = "El campo {PropertyName} debe ser un email válido";
        public static string NoExistMessage = "El Id no esta relacionado con ninguna entidad existente";
    }
}
