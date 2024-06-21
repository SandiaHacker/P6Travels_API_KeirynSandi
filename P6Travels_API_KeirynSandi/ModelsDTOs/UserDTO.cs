namespace P6Travels_API_KeirynSandi.ModelsDTOs
{
    //Un DTO (Data Tranfer Object) es un tipo 
    //de versión "recortada" de un modelo particular
    //donde se pretende quitar todas las complejidades
    //generadas en los modelos a partir del ORM (Entity
    //Framework en nuestro caso).

    //El propisito tambien puede ser ocultar la estructura
    //original de los modelos al equipo de desarollo 
    //Front End ya sea porque no es necesaria visualizar
    //el modelo completo o porque no se debe mostrar la
    //estructura original de las tablas a nivel de
    //bases de datos.



    //En este ejemplo vamos a transformar el modelo a idioma español,
    //ya que original esta en inglés e hipoteticamente el equipo de
    //desarrollo solo hablo español

    public class UserDTO
    {

        public int UsuarioID { get; set; }

        public string? Correo { get; set; }

        public string? Nombre { get; set; }

        public string? Telefono { get; set; }

        public string? Contrasennia { get; set;}

        public int RolID { get; set;}

        //hasta acá todas han sido propiedades que estan 
        //en el modelo original, pero adempas se pueden agregar otras
        //pensando en cuando hacemos consultas tipo inner join
        //que combinen datos de varias tables.
        //siempre es mejor tener versiones lo más "planas" de 
        //los modelos.

        public string? RolDescripcion { get; set; }


    }
}
