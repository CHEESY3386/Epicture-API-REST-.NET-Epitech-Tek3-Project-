﻿using Refit;
using System.Threading.Tasks;

namespace Epicture.API.Services
{
    public interface IImageService
    {
        [Get("/3/gallery/search/{sort}/{window}/{page}")]
        Task<string> GetSearchImages(
            [AliasAs("sort")] string sort,
            [AliasAs("window")] string window,
            [AliasAs("page")] string page,
            [Query] string q,
            [Header("Authorization")] string accessToken);

        [Multipart]
        [Post("/3/upload")]
        Task<string> PostImage(
            [AliasAs("image")] StreamPart image,
            [Header("Authorization")] string accesstoken);

        [Delete("/3/image/{imageHash}")]
        Task<string> DeleteImage(
            [AliasAs("imageHash")] string imageHash,
            [Header("Authorization")] string accesstoken);
    }
}
