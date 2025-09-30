namespace ImagesApi.Dto
{
    /// <summary>
    /// Объект(изображение) для работы в АПИ
    /// </summary>
    /// <param name="Id">Первичный ключ (для добавления в бд должен быть = 0)</param>
    /// <param name="Name">Имя файла</param>
    /// <param name="Format">Формат изображения ("jpg", "png")</param>
    /// <param name="Data">Содержимое изображения (массив байтов)</param>
    public record ImageDto(
        int Id,
        string Name,
        string Format,
        byte[] Data);
}
