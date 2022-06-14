using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Helpers
{
    public class YoutubeSerach
    {
        public static async Task<List<YoutubeVideo>> Run(string query)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDBvrsdShZrBLReWG-CFRqP7uHbOs3kcSw",
                ApplicationName = "ITech"
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query;     // but search terms here
            searchListRequest.MaxResults = 6;   // Number of results

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videos = new List<YoutubeVideo>();

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    videos.Add(new YoutubeVideo
                    {
                        Id = searchResult.Id.VideoId,
                        Title = searchResult.Snippet.Title
                    });
                }
            }
            return videos;

        }
    }
}
