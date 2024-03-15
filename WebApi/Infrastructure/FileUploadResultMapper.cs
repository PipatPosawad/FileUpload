using Domain.Models;
using WebApi.Dtos;

namespace WebApi.Infrastructure
{
    /// <summary>
    /// Contains mapping methods for <see cref="PendingTrayUploadingResultModel"/>
    /// </summary>
    public static class FileDtoMapper
    {
        /// <summary>
        /// Converts a <see cref="PendingTrayUploadingResultModel"/> to a <see cref="PendingTrayUploadingResult"/>
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
                Size = model.Size
            };
        }
    }
}
