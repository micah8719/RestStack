
using System.Collections.Generic;

namespace RestStack.Tests
{
	public class TestClient : RestClientBase
	{
		public TestClient()
			: base("https://jsonplaceholder.typicode.com")
		{
		}

		public RestResponse<List<Post>> GetPosts()
		{
			return Get("/posts", new JsonSerializer<List<Post>>());
		}

		public RestResponse<Post> GetPost(int id)
		{
			return Get($"/posts/{id}", new JsonSerializer<Post>());
		}

		public RestResponse<Post> CreatePost(Post post)
		{
			return Post("/posts", post, "application/json", new JsonSerializer<Post>());
		}

		public RestResponse<Post> EditPost(Post post)
		{
			return Put($"/posts/{post.Id}", post, "application/json", new JsonSerializer<Post>());
		}

		public RestResponse DeletePost(Post post)
		{
			return Delete($"/posts/{post.Id}");
		}
	}
}
