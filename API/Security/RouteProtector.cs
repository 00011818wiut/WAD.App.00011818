using System;
namespace API.Security
{
    struct Endpoint
    {
        public readonly string Method;
        public readonly string Path;

        public Endpoint(string method, string path)
        {
            Method = method;
            Path = path;
        }

        public Endpoint(string path)
        {
            Method = "any";
            Path = path;
        }
    }

    public class RouteProtector
	{

        private readonly Endpoint[] ProtectedRoutes = new Endpoint[] {
            new Endpoint("/api/verify"),

            new Endpoint("POST", "/api/products"),
            new Endpoint("PUT", "/api/products"),
            new Endpoint("DELETE", "/api/products"),
            new Endpoint("GET", "/api/products/user"),

            new Endpoint("POST", "/api/categories"),
            new Endpoint("PUT", "/api/categories"),
            new Endpoint("DELETE", "/api/categories"),

        };

        public bool Check(string method, PathString path)
        {

            return Array.FindIndex(
                ProtectedRoutes,
                endpoint => {
                    return path.StartsWithSegments(endpoint.Path) && (endpoint.Method == "any" || endpoint.Method == method);
            }) != -1;
        }
    }
}

