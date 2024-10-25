using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class Title
{
    public string rendered { get; set; }
}

public class Excerpt
{
    public string rendered { get; set; }
 }

public class Notice
{
    public Title title { get; set; }
    public Excerpt excerpt { get; set; }
}

class URL
{
    public static async Task<ServerResult> URLconexion()
    {
        try
        {
        var url = "https://remolacha.net/wp-json/wp/v2/posts?search=migraci%C3%B3n&_fields=title,excerpt";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        
        // Deserializar JSON a List<Notice>
        var noticias = JsonSerializer.Deserialize<List<Notice>>(json);
        var final = new List<Dictionary<string,string>>();

        foreach (var noticia in noticias)
        {
            var titulo = noticia.title.rendered;
            var resumen = noticia.excerpt.rendered;
            resumen = Regex.Replace(resumen,"<.*?>",string.Empty);
            var dic = new Dictionary<string,string>();
            dic.Add("titulo",titulo);
            dic.Add("resumen",resumen);
            final.Add(dic);
        }
        return new ServerResult(true,"Noticias cargadas", final);
        }
        catch(Exception ex)
        {
            return new ServerResult(false, ex.Message);
        }
    }
}
