internal class Program
{

    private static string FILE_PATH = "FILE_PATH";

    private static string FileName
    {
        get
        {
            string? path = Environment.GetEnvironmentVariable(FILE_PATH);
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception($"{FILE_PATH} environment variable not set.");
            }

            if (!System.IO.Path.Exists(path))
            {
                throw new Exception($"Invalid path {FILE_PATH} = {path}");
            }


            string? fileName = System.IO.Path.Combine(path, "readme.txt");

            return fileName;
        }
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello from API!");

        app.MapGet("/read", (HttpContext context) =>
        {
            string? fileName;
            try { fileName = FileName; }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
            if (!System.IO.File.Exists(fileName))
            {
                return Results.NotFound($"File not found: {fileName}");
            }

            var content = System.IO.File.ReadAllText(fileName);
            return Results.Ok(content);
        });


        app.MapPost("/write", async (HttpContext context) =>
        {
            string? fileName;
            try { fileName = FileName; }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

            using var reader = new StreamReader(context.Request.Body);
            var newText = await reader.ReadToEndAsync();
            System.IO.File.WriteAllText(fileName, newText);

            return Results.Ok("File updated successfully.");
        });

        app.Run("http://+:5230");
    }


}