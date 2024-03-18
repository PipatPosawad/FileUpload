using Domain.Models;
using WebApi.Dtos;

namespace WebApi.Infrastructure
{
    /// <summary>
    /// Contains mapping methods for <see cref="FileModel"/>
    /// </summary>
    public static class FileDtoMapper
    {
        /// <summary>
        /// Converts a <see cref="FileModel"/> to a <see cref="FileDto"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static FileDto Map(FileModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new FileDto
            {
                Id = model.Id,
                Name = model.Name,
                ContentType = model.ContentType,
                SizeInBytes = model.SizeInBytes
            };
        }
    }
}
