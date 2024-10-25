public class Clima
{
    public static ServerResult ObtenerClima(string coordenadas)
    {
        return new ServerResult(true,"Clima obtenido","Soleado");
    }
}