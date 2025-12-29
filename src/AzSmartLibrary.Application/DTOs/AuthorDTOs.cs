namespace AzSmartLibrary.Application.DTOs
{
    // Lo que mostramos en la lista (Salida)
    public record AuthorDto(int Id, string Name);

    // Lo que recibimos del formulario (Entrada)
    public record CreateAuthorDto(string Name);

    // Para edición
    public record UpdateAuthorDto(int Id, string Name);
}
