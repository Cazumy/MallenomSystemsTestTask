namespace ImagesApi.DAL.DbEntities
{
    /// <summary>
    /// Сущность изображения в БД
    /// </summary>
    /// <param name="id">Первичный ключ</param>
    /// <param name="name">Имя файла</param>
    /// <param name="format">Формат изображения ("jpg", "png")</param>
    /// <param name="data">Содержимое изображения (массив байтов)</param>
    public class Image (int id, string name, string format, byte[] data)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Format { get; set; } = format;
        public byte[] Data { get; set; } = data;
    }
}