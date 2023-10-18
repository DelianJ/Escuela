using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Escuela_API.Models;
using EscuelaCRUD.Shared;
using Microsoft.EntityFrameworkCore;

namespace Escuela_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoreController : ControllerBase
    {

        private readonly DbescuelaContext _dbcontext;

        public ProfesoreController(DbescuelaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("ListaProfesore")]
        public async Task<IActionResult> ListaProfesore()
        {
            var responseApi = new ResponseAPI<List<ProfesoreDTO>>();
            var listaProfesoreDto = new List<ProfesoreDTO>();

            try
            {
                foreach (var item in await _dbcontext.Profesores.ToListAsync())
                {
                    listaProfesoreDto.Add(new ProfesoreDTO
                    {
                        IdProfesor = item.IdProfesor,
                        NombreProfesor = item.NombreProfesor,
                    });
                }
                responseApi.esValido = true;
                responseApi.valor = listaProfesoreDto;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("ListaMateriasProfesor/{idProfesor}")]
        public async Task<IActionResult> ListaMateriasProfesor(int idProfesor)
        {
            var responseApi = new ResponseAPI<List<MateriasProfesoreDTO>>();
            var listaMateriasProfesoreDto = new List<MateriasProfesoreDTO>();

            try
            {
                foreach (var item in await _dbcontext.MateriasProfesores.Where(p => p.IdProfesor == idProfesor)
                    .Include(e => e.IdMateriaNavigation).Include(p => p.IdProfesorNavigation).ToListAsync())
                {
                    listaMateriasProfesoreDto.Add(new MateriasProfesoreDTO
                    {
                        IdProfesor = item.IdProfesorNavigation.IdProfesor,
                        IdMateria = item.IdMateriaNavigation.IdMateria,
                        Profesore = new ProfesoreDTO
                        {
                            IdProfesor = item.IdProfesorNavigation.IdProfesor,
                            NombreProfesor = item.IdProfesorNavigation.NombreProfesor
                        },
                        Materia = new MateriaDTO
                        {
                            IdMateria = item.IdMateriaNavigation.IdMateria,
                            NomnbreMateria = item.IdMateriaNavigation.NomnbreMateria
                        },
                    });
                }
                responseApi.esValido = true;
                responseApi.valor = listaMateriasProfesoreDto;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("ListaEstudiantesEnMateras/{idMateria}")]
        public async Task<IActionResult> ListaEstudianteMateras(int idMateria)
        {
            var responseApi = new ResponseAPI<List<EstudianteDTO>>();
            var listaMateriasProfesoreDto = new List<EstudianteDTO>();

            try
            {
                foreach (var item in await _dbcontext.MateriasEstudiantes.Where(p => p.IdMateria == idMateria)
                    .Include(e => e.IdMateriaNavigation).Include(p => p.IdEstudianteNavigation).Distinct().ToListAsync())
                {
                    listaMateriasProfesoreDto.Add(new EstudianteDTO
                    {
                        IdEstudiante = item.IdEstudianteNavigation.IdEstudiante,
                        NombreEstudiante = item.IdEstudianteNavigation.NombreEstudiante,
                        /*Profesore = new ProfesoreDTO
                        {
                            IdProfesor = item.IdProfesorNavigation.IdProfesor,
                            NombreProfesor = item.IdProfesorNavigation.NombreProfesor
                        },
                        Materia = new MateriaDTO
                        {
                            IdMateria = item.IdMateriaNavigation.IdMateria,
                            NomnbreMateria = item.IdMateriaNavigation.NomnbreMateria
                        },*/
                    });
                }
                responseApi.esValido = true;
                responseApi.valor = listaMateriasProfesoreDto;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Calificacion/Materia/{idMateria}/Estudiante/{idEstudiante}")]
        public async Task<IActionResult> CambiarCalificacion(MateriasEstudianteDTO materiasEstudianteDTO, int idMateria, int idEstudiante)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {


                var dbMateriaEstudiante = await _dbcontext.MateriasEstudiantes.FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante && e.IdMateria == idMateria);
                if (dbMateriaEstudiante != null)
                {
                    dbMateriaEstudiante.IdMateria = idMateria;
                    dbMateriaEstudiante.IdEstudiante = idEstudiante;
                    dbMateriaEstudiante.Calificacion = materiasEstudianteDTO.Calificacion;

                    _dbcontext.MateriasEstudiantes.Update(dbMateriaEstudiante);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.esValido = true;
                    responseApi.valor = dbMateriaEstudiante.IdEstudiante;
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

        [HttpGet]
        [Route("ListaEstudiantesAprobados")]
        public async Task<IActionResult> ListaEstudiantesAprobados()
        {
            var responseApi = new ResponseAPI<List<MateriasEstudianteDTO>>();
            var listaEstudantesAprobadosDto = new List<MateriasEstudianteDTO>();

            try
            {
                foreach (var item in await _dbcontext.MateriasEstudiantes.Where(p => p.Calificacion >= 7)
                    .Include(e => e.IdMateriaNavigation).Include(p => p.IdEstudianteNavigation).ToListAsync())
                {
                    listaEstudantesAprobadosDto.Add(new MateriasEstudianteDTO
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
                        Calificacion = item.Calificacion
                    });
                }
                responseApi.esValido = true;
                responseApi.valor = listaEstudantesAprobadosDto;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("ListaEstudiantesReprobados")]
        public async Task<IActionResult> ListaEstudiantesReprobados()
        {
            var responseApi = new ResponseAPI<List<MateriasEstudianteDTO>>();
            var listaEstudantesReprobadosDto = new List<MateriasEstudianteDTO>();

            try
            {
                foreach (var item in await _dbcontext.MateriasEstudiantes.Where(p => p.Calificacion < 7)
                    .Include(e => e.IdMateriaNavigation).Include(p => p.IdEstudianteNavigation).ToListAsync())
                {
                    listaEstudantesReprobadosDto.Add(new MateriasEstudianteDTO
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
                        Calificacion = item.Calificacion
                    });
                }
                responseApi.esValido = true;
                responseApi.valor = listaEstudantesReprobadosDto;
            }
            catch (Exception ex)
            {
                responseApi.esValido = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
