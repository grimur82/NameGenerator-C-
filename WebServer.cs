using System;
using System.IO;
using System.Net;
using System.Text;

public class WebServer{
	private readonly HttpListener listener;
	private readonly Markov markov;
	private readonly string webRoot;

	public WebServer(Markov markov, string webRoot, string prefix = "http://localhost:5000/"){
		this.markov = markov;
		this.webRoot = Path.GetFullPath(webRoot);
		listener = new HttpListener();
		listener.Prefixes.Add(prefix);
	}

	public void Start(){
		listener.Start();
		Console.WriteLine("Web UI running at http://localhost:5000/");
		Console.WriteLine("Press Ctrl+C to stop the server.");

		while(true){
			HttpListenerContext context = listener.GetContext();
			HandleRequest(context);
		}
	}

	private void HandleRequest(HttpListenerContext context){
		string path = context.Request.Url.AbsolutePath;

		if(path.Equals("/api/name", StringComparison.OrdinalIgnoreCase)){
			string name = markov.generateNewName();
			string payload = "{\"name\":\"" + EscapeJson(name) + "\"}";
			byte[] buffer = Encoding.UTF8.GetBytes(payload);
			context.Response.ContentType = "application/json";
			context.Response.OutputStream.Write(buffer, 0, buffer.Length);
			context.Response.Close();
			return;
		}

		if(path == "/"){
			path = "/index.html";
		}

		string requestedPath = Path.GetFullPath(Path.Combine(webRoot, path.TrimStart('/')));
		if(!requestedPath.StartsWith(webRoot, StringComparison.Ordinal)){
			context.Response.StatusCode = 400;
			context.Response.Close();
			return;
		}

		if(!File.Exists(requestedPath)){
			context.Response.StatusCode = 404;
			context.Response.Close();
			return;
		}

		byte[] fileBytes = File.ReadAllBytes(requestedPath);
		context.Response.ContentType = GetContentType(requestedPath);
		context.Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
		context.Response.Close();
	}

	private string GetContentType(string path){
		string extension = Path.GetExtension(path).ToLowerInvariant();
		switch(extension){
			case ".html":
				return "text/html";
			case ".css":
				return "text/css";
			case ".js":
				return "application/javascript";
			default:
				return "application/octet-stream";
		}
	}

	private string EscapeJson(string value){
		return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
	}
}
