using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace clinicApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public string? cedula { get; set; }

        public string? direccion { get; set; }


        // datos medicos
        public string? medicamentos { get; set; }
        public string? alergias { get; set; }
    }

    public class Admin
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }
    }


    public class Doctor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }

        public string? Turno { get; set; }
        public string? Telefono { get; set; }

        public bool IsDeleted { get; set; } = false; 
    }

    public class Cita
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime FechaCita { get; set; }

        public bool IsDeleted { get; set; } = false; 

    }
    public class Medicamentos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Proveedor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public string? cantidad { get; set; }

    }

}

