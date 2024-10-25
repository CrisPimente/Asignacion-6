public class ServerResult
{   
    public bool Exito {get;set;}
    public string Mensaje {get;set;}
    public object Data {get;set;}

    public ServerResult(bool exito, string mensaje, object data)
    {
        Exito = exito;
        Mensaje = mensaje;
        Data = data;
    }

    public ServerResult(bool exito, string mensaje)
    {
        Exito = exito;
        Mensaje = mensaje;
    }

    public ServerResult(bool exito)
    {
        Exito = exito;
    }

    public ServerResult()
    {
        Exito = true;
    }
}