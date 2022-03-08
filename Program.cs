// Se crea el objeto reader que realizará interacción con el archivo
var reader = new StreamReader(File.OpenRead(@"D:\Programacion\cursos\NET\Curso\Ejercicio2\users.csv"));
// Crea userRegister para almacenar los usuarios registrados
List<Users> userRegister = new List<Users>();
// Se inicializa contador para comprobar si alguna linea falla
var count = 0;
var countProcessed = 0;
try
{
    // Bucle para la lectura de todo el archivo
    while (!reader.EndOfStream)
    {
        //Se añade una iteración 
        count++;
        // Bool para comprobar si el email está repetido
        var emailRepeated = false;
        // Usuario leido
        var actualUser = reader.ReadLine();
        // Se separa el usuario
        string[] userParts = actualUser.Split(',');
        if(userParts.Length > 3)
        {
            throw new Exception("Error: Formato no válido, error en la linea: " + count);
        }

        // Se crea el usuario y se le pasan los datos
        Users newUser = new Users();
        newUser.email = userParts[0];
        newUser.fullname = userParts[1];
        newUser.username = userParts[2];

        // Se comprueba que el email no esté repetido
        for (int i = 0; i < userRegister.Count; i++)
        {
            if (userRegister[i].email == newUser.email)
            {
                throw new Exception("Error: Email :" + newUser.email + " ya se ha registrado con anterioridad");
            }
        }
        if (!emailRepeated)
        {
            userRegister.Add(newUser);
        }
    }
    reader.Close();

    foreach (var item in userRegister)
    {
        Console.WriteLine(string.Format("Email: " + item.email + "Nombre: " + item.fullname + "Username: " + item.username));
    }

} catch (Exception ex)
{ 
    if( ex is System.IO.FileNotFoundException)
    {
        throw new Exception("Archivo no encontrado: " + ex.Message);
    }
    else
    {
        Console.Error.WriteLine(ex.Message);
    }
}


class Users
{
    public string email { get; set; }
    public string fullname { get; set; }
    public string username { get; set; } 
}