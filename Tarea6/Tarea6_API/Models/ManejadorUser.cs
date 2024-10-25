using System.IO;
using System.Text.Json;
public class ManejadorUser
{

    public static ServerResult RegistroIncidentes(RegistroIncidentes incidencia)
    {
        if (!Directory.Exists("incidencias"))
        {
            Directory.CreateDirectory("incidencias");
        }

        var miid = Guid.NewGuid().ToString();

        var archivo = $"incidencias/{miid}.json";

        var json = JsonSerializer.Serialize(incidencia);
        File.WriteAllText(archivo,json);

        return new ServerResult(true,"Incidencia registrada");
    }

    public static ServerResult IniciarSesion (LoginRequest datoslogin)
    {
        var cedula = datoslogin.Cedula;
        var clave = datoslogin.Clave;

        if(cedula.Length != 11)
        {
            return new ServerResult(false,"La cedula debe tener 11 digitos");
        }
        if(clave.Length == 0)
        {
            return new ServerResult(false,"La clave es obligatorio");
        }
        
        var archivo = $"";

        if (File.Exists(archivo))
        {
            return new ServerResult(false,"Usuario no encontrado");
        }

        var json = File.ReadAllText(archivo);

        var usuario = JsonSerializer.Deserialize<RegistroUser>(json);
        
        if (usuario.Clave != clave)
        {
            return new ServerResult(false,"Clave incorrecta");
        }

        return new ServerResult(true,"Secion iniciada",usuario);
    }

    public static ServerResult Registro(RegistroUser usuario)
    {
        List<string>errores = new List<string>();
        if(usuario.Cedula.Length != 11)
        {
            errores.Add("La cedula debe tener 11 digitos");
        }
        if(usuario.Nombre.Length == 0)
        {
            errores.Add("El nombre es obligatorio");
        }
        if(usuario.Apellido.Length == 0)
        {
            errores.Add("El apellido es obligatorio");
        }
        if(usuario.Telefono.Length == 0)
        {
        }
        if(usuario.Correo.Length == 0)
        {
        }
        if(usuario.Clave.Length == 0)
        {
        }
        
        if (errores.Count > 0)
        {
            Console.WriteLine("Errores en el registro");
            foreach (var error in errores)
            {
                Console.WriteLine(error);
            }
            return new ServerResult(false, "errores en el registro",errores);
        }

        if (!Directory.Exists("usuarios"))
        {
            Directory.CreateDirectory("usuarios");
        }

        var archivo = $"usuarios/{usuario.Cedula}.json";
        if (File.Exists(archivo))
        {
            return new ServerResult(false,"El usuario ya existe");
        }

        var json = JsonSerializer.Serialize(usuario);
        File.WriteAllText(archivo,json);
        return new ServerResult(true,"usuario registrado");
    }
}