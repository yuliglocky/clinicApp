using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clinicApp.Models;
using SQLite;
namespace clinicApp.Conexion
{
    public class Database
    {

        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            // Crea la tabla  si no existe
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Admin>().Wait();
            _database.CreateTableAsync<Doctor>().Wait(); // Nueva tabla de doctores
            _database.CreateTableAsync<Cita>().Wait();   // Nueva tabla de citas
            _database.CreateTableAsync<Medicamentos>().Wait(); // Nueva tabla de medicamentos
  

        }


        // Obtener todos los usuarios
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        // Obtener usuario por IDS
        public Task<User> GetUserByIdAsync(int id)
        {
            return _database.Table<User>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        // Guardar o actualizar usuario
        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }


        // Crear usuario
        public Task<int> CreateUserAsync(User user)
        {
            // Asegúrate de que el Id sea 0 para la inserción
            user.Id = 0;
            return _database.InsertAsync(user);
        }

        // Eliminar usuario
        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

      // Crear admin 
        public Task<int> CreateAdminAsync(Admin admin)
        {
            // Asegúrate de que el Id sea 0 para la inserción
            admin.Id = 0;
            return _database.InsertAsync(admin);
        }
        public Task<Admin> GetAdminByUsernameAsync(string username)
        {
            return _database.Table<Admin?>().Where(a => a.Username == username).FirstOrDefaultAsync();
        }


        //Servicios Para doctores
        public Task<List<Doctor>> GetDoctorsAsync()
        {
            // Solo devolver doctores que no estén eliminados
            return _database.Table<Doctor>().Where(d => !d.IsDeleted).ToListAsync();
        }

        public Task<Doctor> GetDoctorByIdAsync(int id)
        {
            // Obtener un doctor que no esté eliminado
            return _database.Table<Doctor>().Where(d => d.Id == id && !d.IsDeleted).FirstOrDefaultAsync();
        }

        // Guardar o actualizar doctor
        public Task<int> SaveDoctorAsync(Doctor doctor)
        {
            if (doctor.Id != 0)
            {
                return _database.UpdateAsync(doctor);
            }
            else
            {
                return _database.InsertAsync(doctor);
            }
        }

        // Eliminar lógicamente doctor
        public Task<int> DeleteDoctorAsync(Doctor doctor)
        {
            doctor.IsDeleted = true; // Actualizar el campo IsDeleted en lugar de eliminar
            return _database.UpdateAsync(doctor);
        }

        // SERVICIO PARA CITAS
        public Task<List<Cita>> GetCitasAsync()
        {
            // Solo devolver citas que no estén eliminadas
            return _database.Table<Cita>().Where(c => !c.IsDeleted).ToListAsync();
        }

        public Task<Cita> GetCitaByIdAsync(int id)
        {
            // Obtener una cita que no esté eliminada
            return _database.Table<Cita>().Where(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
        }

        // Guardar o actualizar cita
        public Task<int> SaveCitaAsync(Cita cita)
        {
            if (cita.Id != 0)
            {
                return _database.UpdateAsync(cita);
            }
            else
            {
                return _database.InsertAsync(cita);
            }
        }

        // Eliminar lógicamente cita
        public Task<int> DeleteCitaAsync(Cita cita)
        {
            cita.IsDeleted = true; // Actualizar el campo IsDeleted en lugar de eliminar
            return _database.UpdateAsync(cita);
        }






        //PARTE DEL PARCIAL
        //Medicamentos 
        public Task<List<Medicamentos>> GetMedicamentosAsync()
        {
            return _database.Table<Medicamentos>().Where(c => !c.IsDeleted).ToListAsync();
        }

        public Task<Medicamentos> GetMedicamentosByIdAsync(int id)
        {
            // Obtener una medicamentos que no esté eliminada
            return _database.Table<Medicamentos>().Where(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
        }

        // Guardar o actualizar medicamentos
        public Task<int> SaveMedicamentosAsync(Medicamentos medicamentos)
        {
            if (medicamentos.Id != 0)
            {
                return _database.UpdateAsync(medicamentos);
            }
            else
            {
                return _database.InsertAsync(medicamentos);
            }
        }
        // Eliminar lógicamente Medicamentos
        public Task<int> DeleteMedicamentosAsync(Medicamentos medicamentos)
        {
            medicamentos.IsDeleted = true; // Actualizar el campo IsDeleted en lugar de eliminar
            return _database.UpdateAsync(medicamentos);
        }

      


    }
}