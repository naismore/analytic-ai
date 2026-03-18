namespace Application.Abstract;

public interface IPdfBuilder<T>
{
    /// <summary>
    /// Генерирует PDF для переданных данных
    /// </summary>
    /// <param name="model">Данные для PDF</param>
    /// <returns>Массив байт PDF</returns>
    byte[] BuildPdf(T model);
}
