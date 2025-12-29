namespace AzSmartLibrary.Application.DTOs
{
    public record BookDto(int Id, string Title, string AuthorName, int AuthorId);

    // InputModel: Lo que viene del formulario de creación
    public record CreateBookDto(string Title, int AuthorId);

    // InputModel: Para edición
    public record UpdateBookDto(int Id, string Title, int AuthorId);
}
