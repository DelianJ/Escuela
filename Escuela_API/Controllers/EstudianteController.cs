using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Escuela_API.Models;
using EscuelaCRUD.Shared;
using Microsoft.EntityFrameworkCore;

namespace Escuela_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {

        private readonly DbescuelaContext _dbcontext;

        public EstudianteController(DbescuelaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("ListaEstudiantes")]
        public async Task<IActionResult> ListaEstudiantes()
        {
            var responseApi = new ResponseAPI<List<EstudianteDTO>>();
            var listaEstudiantesDto = new List<EstudianteDTO>();

            try
            {
                foreach (var item in await _dbcontext.Estudiantes.ToListAsync())
                {
                    listaEstudiantesDto.Add(new EstudianteDTO
                    {
                        IdEstudiante = item.IdEstudiante,
                        NombreEstudiante = item.NombreEstudiante,
                    });
                }
                responseApi.esValido = true;
                responseApi.valor = listaEstudiantesDto;
            } catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("ListaMaterasEstudiante/{idEstudiante}")]
        public async Task<IActionResult> ListaMaterasEstudiante(int idEstudiante)
        {
            var responseApi = new ResponseAPI<List<MateriasEstudianteDTO>>();
            var listaMateriasEstudianteDto = new List<MateriasEstudianteDTO>();

            try
            {
                foreach (var item in await _dbcontext.MateriasEstudiantes.Where(s => s.IdEstudiante == idEstudiante)
                    .Include(p => p.IdEstudianteNavigation).Include(e => e.IdMateriaNavigation)
                    .ToListAsync())
                {
                    listaMateriasEstudianteDto.Add(new MateriasEstudianteDTO
                    {
                         IdEstudiante = item.IdEstudianteNavigation.IdEstudiante,
                         IdMateria = item.IdMateriaNavigation.IdMateria,
                         Estudiante = new EstudianteDTO
                         {
                            IdEstudiante = item.IdEstudianteNavigation.IdEstudiante,
                            NombreEstudiante = item.IdEstudianteNavigation.NombreEstudiante
                         },
                         Materia = new MateriaDTO
                         {
                            IdMateria = item.IdMateriaNavigation.IdMateria,
                            NomnbreMateria = item.IdMateriaNavigation.NomnbreMateria
                         },
                         Calificacion = item.Calificacion,
                    });
                }
                    responseApi.esValido = true;
                    responseApi.valor = listaMateriasEstudianteDto;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Estudiante/{idMateria}Eliminar/{idEstudiante}")]
        public async Task<IActionResult> Eliminar(MateriasEstudianteDTO materiasEstudianteDTO, int idMateria, int idEstudiante)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {


                var dbMateriaEstudiante = await _dbcontext.MateriasEstudiantes.FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante && e.IdMateria == idMateria);
                if (dbMateriaEstudiante != null)
                {
                    _dbcontext.MateriasEstudiantes.Remove(dbMateriaEstudiante);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.esValido = true;
                }
                else
                {
                    responseApi.esValido = false;
                    responseApi.mensaje = "Estudiante no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(MateriasEstudianteDTO materiasEstudianteDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbMateriaEstudiante = new MateriasEstudiante
                {
                    IdEstudiante = materiasEstudianteDTO.IdEstudiante,
                    IdMateria = materiasEstudianteDTO.IdMateria,
                    Calificacion = 0
                };

                _dbcontext.MateriasEstudiantes.Add(dbMateriaEstudiante);
                await _dbcontext.SaveChangesAsync();

                responseApi.esValido = true;
                responseApi.valor = dbMateriaEstudiante.IdEstudiante;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = "No se pudo guardar";
            }

            return Ok(responseApi);
        }

    }
}
