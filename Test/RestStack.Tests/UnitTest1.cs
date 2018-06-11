using System.Collections.Generic;
using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestStack.Tests
{
	[TestClass]
	public class UnitTest1
	{
		private TestClient _client;

		[TestInitialize]
		public void Initialize()
		{
			_client = new TestClient();
		}

		[TestMethod]
		public void AssertGetAll()
		{
			var posts = _client.GetPosts();

			Assert.AreEqual(posts.StatusCode, HttpStatusCode.OK);
			Assert.IsTrue(posts.Data.Count > 0);
			Assert.IsInstanceOfType(posts, typeof(RestResponse<List<Post>>));
		}

		[TestMethod]
		public void AssertGetSingle()
		{
			var post = _client.GetPost(1);

			Assert.AreEqual(post.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(post.Data.Id, 1);
			Assert.IsInstanceOfType(post, typeof(RestResponse<Post>));
		}

		[TestMethod]
		public void AssertPut()
		{
			var body = "hello, world!";
			var post = _client.GetPost(1);
			post.Data.Body = body;
			var editPost = _client.EditPost(post);

			Assert.AreEqual(editPost.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(editPost.Data.Id, 1);
			Assert.AreEqual(editPost.Data.Body, body);
			Assert.IsInstanceOfType(editPost, typeof(RestResponse<Post>));
		}

		[TestMethod]
		public void AssertPost()
		{
			var post = _client.GetPost(1);
			post.Data.Id = 0;
			var newPost = _client.CreatePost(post);

			Assert.AreEqual(newPost.StatusCode, HttpStatusCode.Created);
			Assert.IsFalse(newPost.Data.Id == 1);
			Assert.IsInstanceOfType(newPost, typeof(RestResponse<Post>));
		}

		[TestMethod]
		public void AssertDelete()
		{
			var post = _client.GetPost(1);
			var deletePost = _client.DeletePost(post);

			Assert.AreEqual(deletePost.StatusCode, HttpStatusCode.OK);
			Assert.IsInstanceOfType(deletePost, typeof(RestResponse));
		}

		[TestCleanup]
		public void Dispose()
		{
			_client?.Dispose();
		}
	}
}
