namespace AzSmartLibrary.Application.DTOs
{
    public record BookDto(int Id, string Title, string AuthorName, int AuthorId);

    
    public record CreateBookDto(string Title, int AuthorId);

    
    public record UpdateBookDto(int Id, string Title, int AuthorId);
}
